namespace BoardGenerator.Control
{
    using System.Windows.Forms;

    public partial class BoardEditor : UserControl
    {
        private bool mouseDown = false;
        private Point mouseDownLocation;
        private Point position;
        private Point offset;


        public BoardEditor()
        {
            InitializeComponent();
        }


        public Point Position => new Point(
            this.position.X - this.offset.X, this.position.Y - this.offset.Y);


        private void BoardEditor_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;

            var pen = new Pen(Color.Yellow, 3);

            var rect = new Rectangle(this.Position, new Size(200, 200));

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
        }
    }
}
