namespace BoardGenerator.Control
{
    using BoardGenerator.Conf;
    using System;

    internal class ContextMenu
    {
        private readonly BoardEditor brd;
        private readonly Area area;
        private readonly IEnumerable<Area> allAreas;
        private readonly ContextMenuStrip menu = new ContextMenuStrip();


        public ContextMenu(BoardEditor brd,
            Area area, IEnumerable<Area> allAreas)
        {
            this.brd = brd;
            this.area = area;
            this.allAreas = allAreas;
        }


        public void Show(Point position)
        {
            this.AddLabel($"Name: {area.Name}");

            if (!string.IsNullOrWhiteSpace(area.Group))
            {
                this.AddLabel($"Group: {area.Group}");
            }

            this.AddSeparator();

            string lockedLabel = (area.Locked == true)
                ? "Unlock Area"
                : "Lock Area";

            this.AddItem(lockedLabel, this.ToggleLocked);

            this.AddItem("Toggle locked (CTRL + left mouse)", this.ToggleLocked);

            if (!string.IsNullOrWhiteSpace(this.area.Group))
            {
                var groupAreas = this.allAreas
                    .Where(a => a.Group == this.area.Group);

                if (groupAreas.Any(g => g.Locked != true))
                {
                    this.AddItem("Lock Group Areas",
                        () => this.SetGroupLocked(true, groupAreas));
                }

                if (groupAreas.Any(g => g.Locked == true))
                {
                    this.AddItem("Unlock Group Areas",
                        () => this.SetGroupLocked(false, groupAreas));
                }
            }

            this.AddSeparator();

            this.AddItem("Select Image File", this.SelectImage);

            menu.Show(this.brd, position);
        }

        private void AddLabel(string text)
        {
            var label = new ToolStripLabel(text);

            this.menu.Items.Add(label);
        }

        private void AddSeparator()
        {
            this.menu.Items.Add(new ToolStripSeparator());
        }

        private void AddItem(string text, Action action)
        {
            var eventHandler = new EventHandler(
                (object o, EventArgs e) => action?.Invoke());

            this.menu.Items.Add(text, null, eventHandler);
        }

        private void ToggleLocked()
        {
            this.area.Locked = (this.area.Locked == true)
                ? null
                : true;

            this.brd.Refresh();
        }

        private void SetGroupLocked(bool? locked,
            IEnumerable<Area> groupAreas)
        {
            foreach (Area area in groupAreas)
            {
                area.Locked = (locked == true) ? true : null;
            }

            this.brd.Refresh();
        }

        private void SelectImage()
        {
            string title = "Select Image File";

            string filter =
                "All Image Files|*.png;*.jpg;*.jpeg;*.bmp|" +
                "PNG|*.png" +
                "JPEG|*.jpg;*.jpeg" +
                "BMP|*.bmp";

            using (var openFileDialog = FileHelper.ShowOpenFileDialog(title, filter))
            {
                string filePath = openFileDialog.FileName;

                if (filePath != "")
                {
                    this.area.File = filePath;
                    this.area.Locked = true;

                    this.brd.Refresh();
                }
            }
        }
    }
}
