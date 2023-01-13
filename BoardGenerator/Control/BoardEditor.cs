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


        public BoardEditor()
        {
            InitializeComponent();
        }


        private Point Position => new Point(
            this.position.X - this.offset.X, this.position.Y - this.offset.Y);


        public void SetConfiguration(Area[] areas, bool resetPosition)
        {
            if (resetPosition)
            {
                this.position = new Point(this.Size.Width / 2, this.Height / 2);
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
            int x = this.Position.X - (this.bounds.Width / 2) + area.X - this.bounds.Left;
            int y = this.Position.Y - (this.bounds.Height / 2) + area.Y - this.bounds.Top;

            var areaPosition = new Point(x, y);

            var areaSize = new Size(area.Width, area.Height);


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
        }

        private void BoardEditor_MouseUp(object sender, MouseEventArgs e)
        {
            this.mouseDown = false;

            this.position = this.Position;

            this.offset = new Point();
        }
    }
}
