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

        public Point Position => new Point(
            this.position.X - this.offset.X, this.position.Y - this.offset.Y);

        public Action Dragged { get; set; }
        public Action ZoomChanged { get; set; }



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
                    // The user scrolled up.
                    ++this.Zoom;
                }
                else
                {
                    // The user scrolled down.
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

            this.areas = areas;
            this.bounds = Config.GetBounds(areas);

            this.Refresh();
        }


        private void BoardEditor_Paint(object sender, PaintEventArgs e)
        {
            if (this.areas?.Any() != true)
            {
                return;
            }

            foreach (Area area in this.areas)
            {
                Graphics g = e.Graphics;

                this.DrawArea(g, area);
            }
        }

        private void DrawArea(Graphics g, Area area)
        {
            float scale = this.ScaleFactor;

            int areaX = (int)(area.X * scale);
            int areaY = (int)(area.Y * scale);

            int boundsWidth = (int)(this.bounds.Width * scale);
            int boundsHeight = (int)(this.bounds.Width * scale);

            int x = this.Position.X - (boundsWidth / 2) + areaX - this.bounds.Left;
            int y = this.Position.Y - (boundsHeight / 2) + areaY - this.bounds.Top;

            var areaPosition = new Point(x, y);

            int width = (int)(area.Width * scale);
            int height = (int)(area.Height * scale);

            var areaSize = new Size(width, height);


            var pen = new Pen(Color.Yellow, 3);

            var rect = new Rectangle(areaPosition, areaSize);

            g.DrawRectangle(pen, rect);
        }


        private void BoardEditor_MouseDown(object sender, MouseEventArgs e)
        {
            this.mouseDown = true;

            this.mouseDownLocation = e.Location;
        }

        private void BoardEditor_MouseMove(object sender, MouseEventArgs e)
        {
            if (!this.mouseDown)
            {
                return;
            }

            var location = e.Location;

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
        }

        private void BoardEditor_KeyDown(object sender, KeyEventArgs e)
        {
            this.ctrlDown = e.Control;
        }

        private void BoardEditor_KeyUp(object sender, KeyEventArgs e)
        {
            this.ctrlDown = e.Control;
        }
    }
}
