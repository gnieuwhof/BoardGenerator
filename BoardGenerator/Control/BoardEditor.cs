namespace BoardGenerator.Control
{
    using BoardGenerator.Conf;
    using System.Windows.Forms;

    public partial class BoardEditor : UserControl
    {
        private bool mouseDown = false;
        private Point mouseDownLocation;
        private Point position;
        private Point offset;

        private Area[] areas;
        private Rectangle bounds;
        private bool ctrlDown = false;

        private bool drawBorders = true;
        private bool drawLabels = true;


        public BoardEditor()
        {
            InitializeComponent();

            this.MouseWheel += BoardEditor_MouseWheel;
            this.LostFocus += BoardEditor_LostFocus;
        }


        public int Zoom { get; set; }

        public float ScaleFactor
        {
            get
            {
                var abs = Math.Abs(this.Zoom);
                var factor = (float)Math.Pow(1.25, abs);

                if (this.Zoom < 0)
                {
                    return 1 / factor;
                }

                return factor;
            }
        }

        public Point Position => this.GetOffset(this.offset, this.position);

        public Action Dragged { get; set; }
        public Action ZoomChanged { get; set; }
        public Action<Keys> CtrlShortcut { get; set; }


        public void SetDrawBorders(bool drawBorders)
        {
            this.drawBorders = drawBorders;

            this.Refresh();
        }

        public void SetDrawLabels(bool drawLabels)
        {
            this.drawLabels = drawLabels;

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
                this.position = new Point(this.Size.Width / 2, this.Height / 2);
                this.Dragged?.Invoke();
            }

            this.areas = areas
                .OrderBy(a => a.Z)
                .ToArray();
            this.bounds = Config.GetBounds(areas);

            this.Refresh();
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

                this.DrawArea(g, area);
            }

            this.ResumeLayout();
        }

        private void DrawArea(Graphics g, Area area)
        {
            Rectangle areaRect = GetAreaRectangle(area);

            int x = areaRect.X;
            int y = areaRect.Y;
            int width = areaRect.Width;
            int height = areaRect.Height;

            var areaPosition = new Point(x, y);

            var areaSize = new Size(width, height);

            if (!this.mouseDown && !string.IsNullOrWhiteSpace(area.File))
            {
                if (!File.Exists(area.File))
                {
                    Logging.Log($"File {area.File} for area {area.Name} does not exist");
                }

                if (area.Img == null)
                {
                    area.SetImage(area.File, Image.FromFile(area.File));
                }

                g.DrawImage(area.Img, x, y, width, height);
            }

            if (this.drawBorders)
            {
                Color color = (area.Locked == true)
                    ? Color.LightGray
                    : Color.Yellow;

                var pen = new Pen(color, 3);

                var rect = new Rectangle(areaPosition, areaSize);

                g.DrawRectangle(pen, rect);
            }

            if (this.drawLabels)
            {
                var font = new Font("Consolas", 15);

                Brush brush = (area.Locked == true)
                    ? Brushes.LightGray
                    : Brushes.Yellow;

                g.DrawString(area.Name, font, brush, new Point(x + 5, y + 5));
            }
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
            if (e.Button == MouseButtons.Right)
            {
                var position = new Point(e.X, e.Y);

                Area area = GetArea(position);

                if (area != null)
                {
                    var ctxMenu = new ContextMenu(this, area);
                    ctxMenu.Show(position);
                }

                return;
            }

            this.mouseDown = true;

            this.mouseDownLocation = e.Location;
        }

        private Area GetArea(Point position)
        {
            foreach (Area area in this.areas)
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
    }
}
