namespace BoardGenerator.Control
{
    using BoardGenerator.Conf;
    using System;

    internal class ContextMenu
    {
        private readonly BoardEditor brd;
        private readonly Area area;
        private readonly ContextMenuStrip menu = new ContextMenuStrip();


        public ContextMenu(BoardEditor brd, Area area)
        {
            this.brd = brd;
            this.area = area;
        }


        public void Show(Point position)
        {
            this.AddLabel($"Name: {area.Name}");

            if (!string.IsNullOrWhiteSpace(area.Group))
            {
                this.AddLabel($"Group: {area.Group}");
            }

            string lockedLabel = (area.Locked == true)
                ? "Unlock Area"
                : "Lock Area";

            this.AddItem(lockedLabel, this.ToggleLocked);

            menu.Show(this.brd, position);
        }

        private void AddLabel(string text)
        {
            var label = new ToolStripLabel(text);

            this.menu.Items.Add(label);
        }

        private void AddItem(string text, Action<object, EventArgs> action)
        {
            var eventHandler = new EventHandler(action);

            this.menu.Items.Add(text, null, eventHandler);
        }

        private void ToggleLocked(object sender, EventArgs e)
        {
            this.area.Locked = (this.area.Locked == true)
                ? null
                : true;

            this.brd.Refresh();
        }
    }
}
