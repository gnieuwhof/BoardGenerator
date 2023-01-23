namespace BoardGenerator.Control
{
    using BoardGenerator.Conf;
    using System.Drawing.Drawing2D;
    using System.Drawing.Imaging;
    using System.Windows.Forms;

    public partial class BoardEditor : UserControl
    {
        private const int BORDER_WIDTH = 4;


        private bool mouseDown = false;
        private Point mouseDownLocation;
        private Point position;
        private Point offset;

        private Area[] areas;
        private Rectangle bounds;
        private bool ctrlDown = false;

        private bool drawBorders = true;
        private bool drawAreaNames = true;
        private bool drawGroupNames = true;


        public BoardEditor()
        {
            InitializeComponent();

            this.MouseWheel += BoardEditor_MouseWheel;
            this.LostFocus += BoardEditor_LostFocus;
        }


        public int Zoom { get; set; }

        public ImageCache Cache { get; set; }

        public Color BorderColor { get; set; } = Color.Yellow;
        public Brush TextColor { get; set; } = Brushes.Yellow;

        public float ScaleFactor => GetScaleFactor(this.Zoom);

        private static float GetScaleFactor(int zoom)
        {
            var abs = Math.Abs(zoom);
            var factor = (float)Math.Pow(1.25, abs);

            if (zoom < 0)
            {
                return 1 / factor;
            }

            return factor;
        }

        public Point Position => this.GetOffset(this.offset, this.position);

        public Action Dragged { get; set; }
        public Action ZoomChanged { get; set; }
        public Action<Keys> CtrlShortcut { get; set; }

        public string BasePath { get; set; }


        public void SetDrawBorders(bool drawBorders)
        {
            this.drawBorders = drawBorders;

            this.Refresh();
        }

        public void SetDrawLabels(bool drawLabels)
        {
            this.drawAreaNames = drawLabels;

            this.Refresh();
        }

        public void SetDrawGroups(bool drawGroups)
        {
            this.drawGroupNames = drawGroups;

            this.Refresh();
        }

        private Point GetOffset(Point offset, Point position) =>
            new Point(position.X - offset.X, position.Y - offset.Y);

        private void BoardEditor_LostFocus(object sender, EventArgs e)
        {
            this.ctrlDown = false;
        }

        private void BoardEditor_MouseWheel(object sender, MouseEventArgs e)
        {
            if (this.ctrlDown)
            {
                if (e.Delta > 0)
                {
                    ++this.Zoom;
                }
                else
                {
                    --this.Zoom;
                }

                this.Refresh();

                this.ZoomChanged?.Invoke();
            }
        }

        public void SetConfiguration(Area[] areas, bool resetPosition)
        {
            if (resetPosition)
            {
                this.Zoom = 1;

                int direction = 1;

                var areaRect = Config.GetBounds(areas, log: false);

                int areaWidth = areaRect.Width;
                int areaHeight = areaRect.Height;

                if (!Fits(areaWidth, areaHeight))
                {
                    direction = -1;
                }

                int zoom = 0;

                while (true)
                {
                    if (direction == 1)
                    {
                        ++zoom;
                    }
                    else
                    {
                        --zoom;
                    }

                    float scaleFactor = GetScaleFactor(zoom);

                    areaWidth = (int)(areaRect.Width * scaleFactor);
                    areaHeight = (int)(areaRect.Height * scaleFactor);

                    bool fits = Fits(areaWidth, areaHeight);

                    if (((direction == 1) && !fits) ||
                        ((direction == -1) && fits))
                    {
                        if (direction == -1)
                        {
                            this.Zoom = zoom;
                        }

                        this.ZoomChanged?.Invoke();

                        break;
                    }

                    this.Zoom = zoom;
                }

                this.position = new Point(this.Width / 2, this.Height / 2);
                this.Dragged?.Invoke();

                bool Fits(int w, int h)
                {
                    return ((this.Width - 10 > w) &&
                        (this.Height - 10 > h));
                }
            }

            this.areas = areas
                .OrderBy(a => a.Z)
                .ToArray();

            this.bounds = Config.GetBounds(areas);

            this.Refresh();
        }

        // Prevent flickering (ish).
        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams cp = base.CreateParams;
                cp.ExStyle = cp.ExStyle | 0x2000000;
                return cp;
            }
        }

        private void BoardEditor_Paint(object sender, PaintEventArgs e)
        {
            if (this.areas?.Any() != true)
            {
                return;
            }

            this.SuspendLayout();

            foreach (Area area in this.areas)
            {
                Graphics g = e.Graphics;

                bool drawn = this.DrawArea(g, area, export: false);

                if (!drawn)
                {
                    // THE FUCK!!
                }
            }

            this.ResumeLayout(false);
        }


        private string GetPath(string file)
        {
            if (!file.Contains(':'))
            {
                file = Path.Combine(this.BasePath, file);
            }

            return file;
        }

        private bool DrawArea(Graphics g, Area area, bool export, bool overlay = true)
        {
            bool result = true;

            Rectangle areaRect = export
                ? new Rectangle(area.X - this.bounds.Left,
                    area.Y - this.bounds.Top, area.Width, area.Height)
                : GetAreaRectangle(area);

            int x = areaRect.X;
            int y = areaRect.Y;
            int width = areaRect.Width;
            int height = areaRect.Height;

            if (area.File != null)
            {
                string file = this.GetPath(area.File);

                if (File.Exists(file))
                {
                    var areaPosition = new Point(x, y);

                    var areaSize = new Size(width, height);

                    if (export || !this.mouseDown)
                    {
                        var img = FileHelper.GetImage(this.Cache, file);

                        float scale = export ? 1 : this.ScaleFactor;

                        int scaledImgWidth = (int)(img.Width * scale);
                        int scaledImgHeight = (int)(img.Height * scale);

                        g.DrawImage(img, x, y, scaledImgWidth, scaledImgHeight);
                    }
                }
                else
                {
                    Logging.Log($"File: {file} does not exist");

                    result = false;
                }
            }

            if (!overlay)
            {
                return result;
            }

            if (this.drawBorders || this.mouseDown || (area.Locked == true))
            {
                int halfWidth = BORDER_WIDTH / 2;

                var borderPosition = new Point(x + halfWidth, y + halfWidth);

                var borderSize = new Size(
                    width - BORDER_WIDTH, height - BORDER_WIDTH);

                Color color = (area.Locked == true)
                    ? Color.LightGray
                    : this.BorderColor;

                var pen = new Pen(color, BORDER_WIDTH);

                var rect = new Rectangle(borderPosition, borderSize);

                g.DrawRectangle(pen, rect);
            }

            var font = new Font("Consolas", 15);

            Brush brush = (area.Locked == true)
                ? Brushes.LightGray
                : this.TextColor;

            Brush backColor = BackColorBrush(brush);

            if (this.drawAreaNames)
            {
                this.DrawText(g, new Point(x + 5, y + 5),
                    area.Name, font, brush, backColor);

                if (this.drawGroupNames)
                {
                    NestedDrawGroupNames($"{area.Group}", new Point(x + 5, y + 25));
                }
            }
            else if (this.drawGroupNames)
            {
                NestedDrawGroupNames($"{area.Group}", new Point(x + 5, y + 25));
            }

            void NestedDrawGroupNames(string name, Point position)
            {
                this.DrawText(g, position, name, font, brush, backColor);
            }

            return result;
        }

        private static Brush BackColorBrush(Brush brush)
        {
            if (brush == Brushes.Black)
                return Brushes.White;

            if (brush == Brushes.Blue)
                return Brushes.White;

            return Brushes.Black;
        }

        private void DrawText(Graphics g, Point point, string text,
            Font font, Brush textColor, Brush backColor)
        {
            Size sizeOfText = TextRenderer.MeasureText(text, font);

            var rect = new Rectangle(point, sizeOfText);

            g.FillRectangle(backColor, rect);

            g.DrawString(text, font, textColor, point);
        }

        private Rectangle GetAreaRectangle(Area area)
        {
            float scale = this.ScaleFactor;

            int areaX = (int)(area.X * scale);
            int areaY = (int)(area.Y * scale);

            int boundsWidth = (int)(this.bounds.Width * scale);
            int boundsHeight = (int)(this.bounds.Height * scale);

            int x = this.Position.X - (boundsWidth / 2) + areaX - this.bounds.X;
            int y = this.Position.Y - (boundsHeight / 2) + areaY - this.bounds.Y;

            int width = (int)(area.Width * scale);
            int height = (int)(area.Height * scale);

            var rectangle = new Rectangle(x, y, width, height);

            return rectangle;
        }


        private void BoardEditor_MouseDown(object sender, MouseEventArgs e)
        {
            var position = new Point(e.X, e.Y);

            Area area = GetArea(position);

            if (e.Button == MouseButtons.Right)
            {
                if (area != null)
                {
                    var ctxMenu = new ContextMenu(this, area, this.areas);

                    ctxMenu.Show(position);
                }

                return;
            }
            else if (this.ctrlDown && (area != null))
            {
                area.Locked = (area.Locked == true) ? null : true;

                this.Refresh();
            }

            this.mouseDown = true;

            this.mouseDownLocation = e.Location;
        }

        private Area GetArea(Point position)
        {
            foreach (Area area in this.areas.Reverse())
            {
                Rectangle rect = GetAreaRectangle(area);

                if (rect.Contains(position))
                {
                    return area;
                }
            }

            return null;
        }

        private void BoardEditor_MouseMove(object sender, MouseEventArgs e)
        {
            if (!this.mouseDown ||
                (this.bounds.Width == 0) ||
                (this.bounds.Height == 0)
                )
            {
                return;
            }

            Point location = e.Location;

            this.offset = new Point(
                this.mouseDownLocation.X - location.X,
                this.mouseDownLocation.Y - location.Y
                );

            this.Refresh();

            this.Dragged?.Invoke();
        }

        private void BoardEditor_MouseUp(object sender, MouseEventArgs e)
        {
            this.mouseDown = false;

            this.position = this.Position;

            this.offset = new Point();

            this.Refresh();
        }

        private void BoardEditor_KeyDown(object sender, KeyEventArgs e)
        {
            this.ctrlDown = e.Control;

            if (this.ctrlDown)
            {
                this.CtrlShortcut?.Invoke(e.KeyCode);
            }
        }

        private void BoardEditor_KeyUp(object sender, KeyEventArgs e)
        {
            this.ctrlDown = e.Control;
        }

        public Bitmap Export()
        {
            int width = this.bounds.Width;
            int height = this.bounds.Height;

            var bitmap = new Bitmap(width, height, PixelFormat.Format32bppArgb);

            var g = Graphics.FromImage(bitmap);

            g.SmoothingMode = SmoothingMode.HighQuality;
            g.InterpolationMode = InterpolationMode.HighQualityBilinear;
            g.CompositingQuality = CompositingQuality.HighQuality;
            g.PixelOffsetMode = PixelOffsetMode.HighQuality;

            foreach (Area area in this.areas)
            {
                bool drawn = this.DrawArea(g, area, export: true, overlay: false);

                if (!drawn)
                {
                    // THE FUCK!!
                }
            }

            return bitmap;
        }
    }
}
