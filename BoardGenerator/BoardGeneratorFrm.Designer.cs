namespace BoardGenerator
{
    partial class BoardGeneratorFrm
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.statusStrip = new System.Windows.Forms.StatusStrip();
            this.statusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.positionLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.zoomLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.menuStrip = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.quitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.configurationToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.loadToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveAsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.reloadToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.autoReloadToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.createExampleToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.boardToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.generateToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.lockAllAreasToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.unlockAllAreasToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.viewToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.areaNamesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.bordersToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.groupNamesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
            this.whiteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.yellowToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.redToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.blueToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.blackToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.windowToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.logToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.boardEditor = new BoardGenerator.Control.BoardEditor();
            this.statusStrip.SuspendLayout();
            this.menuStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // statusStrip
            // 
            this.statusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.statusLabel,
            this.positionLabel,
            this.zoomLabel});
            this.statusStrip.Location = new System.Drawing.Point(0, 426);
            this.statusStrip.Name = "statusStrip";
            this.statusStrip.Size = new System.Drawing.Size(800, 24);
            this.statusStrip.TabIndex = 0;
            this.statusStrip.Text = "statusStrip1";
            // 
            // statusLabel
            // 
            this.statusLabel.Name = "statusLabel";
            this.statusLabel.Size = new System.Drawing.Size(675, 19);
            this.statusLabel.Spring = true;
            this.statusLabel.Text = "Status";
            this.statusLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // positionLabel
            // 
            this.positionLabel.BorderSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.Left;
            this.positionLabel.BorderStyle = System.Windows.Forms.Border3DStyle.Etched;
            this.positionLabel.Name = "positionLabel";
            this.positionLabel.Size = new System.Drawing.Size(55, 19);
            this.positionLabel.Text = "X: 0, Y: 0";
            // 
            // zoomLabel
            // 
            this.zoomLabel.BorderSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.Left;
            this.zoomLabel.BorderStyle = System.Windows.Forms.Border3DStyle.Etched;
            this.zoomLabel.Name = "zoomLabel";
            this.zoomLabel.Size = new System.Drawing.Size(55, 19);
            this.zoomLabel.Text = "Zoom: 0";
            // 
            // menuStrip
            // 
            this.menuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.configurationToolStripMenuItem,
            this.boardToolStripMenuItem,
            this.viewToolStripMenuItem,
            this.windowToolStripMenuItem});
            this.menuStrip.Location = new System.Drawing.Point(0, 0);
            this.menuStrip.Name = "menuStrip";
            this.menuStrip.Size = new System.Drawing.Size(800, 24);
            this.menuStrip.TabIndex = 1;
            this.menuStrip.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.quitToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "&File";
            // 
            // quitToolStripMenuItem
            // 
            this.quitToolStripMenuItem.Name = "quitToolStripMenuItem";
            this.quitToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.F4)));
            this.quitToolStripMenuItem.Size = new System.Drawing.Size(135, 22);
            this.quitToolStripMenuItem.Text = "&Exit";
            this.quitToolStripMenuItem.Click += new System.EventHandler(this.QuitMenuItem_Click);
            // 
            // configurationToolStripMenuItem
            // 
            this.configurationToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.loadToolStripMenuItem,
            this.saveToolStripMenuItem,
            this.saveAsToolStripMenuItem,
            this.reloadToolStripMenuItem,
            this.autoReloadToolStripMenuItem,
            this.createExampleToolStripMenuItem});
            this.configurationToolStripMenuItem.Name = "configurationToolStripMenuItem";
            this.configurationToolStripMenuItem.Size = new System.Drawing.Size(93, 20);
            this.configurationToolStripMenuItem.Text = "&Configuration";
            // 
            // loadToolStripMenuItem
            // 
            this.loadToolStripMenuItem.Name = "loadToolStripMenuItem";
            this.loadToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.O)));
            this.loadToolStripMenuItem.Size = new System.Drawing.Size(156, 22);
            this.loadToolStripMenuItem.Text = "&Load";
            this.loadToolStripMenuItem.Click += new System.EventHandler(this.LoadConfigurationMenuItem_Click);
            // 
            // saveToolStripMenuItem
            // 
            this.saveToolStripMenuItem.Name = "saveToolStripMenuItem";
            this.saveToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.S)));
            this.saveToolStripMenuItem.Size = new System.Drawing.Size(156, 22);
            this.saveToolStripMenuItem.Text = "&Save";
            this.saveToolStripMenuItem.Click += new System.EventHandler(this.SaveMenuItem_Click);
            // 
            // saveAsToolStripMenuItem
            // 
            this.saveAsToolStripMenuItem.Name = "saveAsToolStripMenuItem";
            this.saveAsToolStripMenuItem.Size = new System.Drawing.Size(156, 22);
            this.saveAsToolStripMenuItem.Text = "Save As";
            this.saveAsToolStripMenuItem.Click += new System.EventHandler(this.SaveAsMenuItem_Click);
            // 
            // reloadToolStripMenuItem
            // 
            this.reloadToolStripMenuItem.Name = "reloadToolStripMenuItem";
            this.reloadToolStripMenuItem.Size = new System.Drawing.Size(156, 22);
            this.reloadToolStripMenuItem.Text = "&Reload";
            this.reloadToolStripMenuItem.Click += new System.EventHandler(this.ReloadMenuItem_Click);
            // 
            // autoReloadToolStripMenuItem
            // 
            this.autoReloadToolStripMenuItem.Name = "autoReloadToolStripMenuItem";
            this.autoReloadToolStripMenuItem.Size = new System.Drawing.Size(156, 22);
            this.autoReloadToolStripMenuItem.Text = "&Auto Reload";
            this.autoReloadToolStripMenuItem.Click += new System.EventHandler(this.AutoReloadMenuItem_Click);
            // 
            // createExampleToolStripMenuItem
            // 
            this.createExampleToolStripMenuItem.Name = "createExampleToolStripMenuItem";
            this.createExampleToolStripMenuItem.Size = new System.Drawing.Size(156, 22);
            this.createExampleToolStripMenuItem.Text = "Create &Example";
            this.createExampleToolStripMenuItem.Click += new System.EventHandler(this.CreateConfigurationExampleMenuItem_Click);
            // 
            // boardToolStripMenuItem
            // 
            this.boardToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.generateToolStripMenuItem,
            this.lockAllAreasToolStripMenuItem,
            this.unlockAllAreasToolStripMenuItem});
            this.boardToolStripMenuItem.Name = "boardToolStripMenuItem";
            this.boardToolStripMenuItem.Size = new System.Drawing.Size(50, 20);
            this.boardToolStripMenuItem.Text = "&Board";
            // 
            // generateToolStripMenuItem
            // 
            this.generateToolStripMenuItem.Name = "generateToolStripMenuItem";
            this.generateToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.G)));
            this.generateToolStripMenuItem.Size = new System.Drawing.Size(202, 22);
            this.generateToolStripMenuItem.Text = "&Generate";
            this.generateToolStripMenuItem.Click += new System.EventHandler(this.GenerateMenuItem_Click);
            // 
            // lockAllAreasToolStripMenuItem
            // 
            this.lockAllAreasToolStripMenuItem.Name = "lockAllAreasToolStripMenuItem";
            this.lockAllAreasToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.L)));
            this.lockAllAreasToolStripMenuItem.Size = new System.Drawing.Size(202, 22);
            this.lockAllAreasToolStripMenuItem.Text = "&Lock All Areas";
            this.lockAllAreasToolStripMenuItem.Click += new System.EventHandler(this.LockAllAreasMenuItem_Click);
            // 
            // unlockAllAreasToolStripMenuItem
            // 
            this.unlockAllAreasToolStripMenuItem.Name = "unlockAllAreasToolStripMenuItem";
            this.unlockAllAreasToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.U)));
            this.unlockAllAreasToolStripMenuItem.Size = new System.Drawing.Size(202, 22);
            this.unlockAllAreasToolStripMenuItem.Text = "&Unlock All Areas";
            this.unlockAllAreasToolStripMenuItem.Click += new System.EventHandler(this.UnlockAllAreasMenuItem_Click);
            // 
            // viewToolStripMenuItem
            // 
            this.viewToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.areaNamesToolStripMenuItem,
            this.bordersToolStripMenuItem,
            this.groupNamesToolStripMenuItem,
            this.toolStripMenuItem1,
            this.whiteToolStripMenuItem,
            this.yellowToolStripMenuItem,
            this.redToolStripMenuItem,
            this.blueToolStripMenuItem,
            this.blackToolStripMenuItem});
            this.viewToolStripMenuItem.Name = "viewToolStripMenuItem";
            this.viewToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.O)));
            this.viewToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
            this.viewToolStripMenuItem.Text = "&View";
            // 
            // areaNamesToolStripMenuItem
            // 
            this.areaNamesToolStripMenuItem.Checked = true;
            this.areaNamesToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.areaNamesToolStripMenuItem.Name = "areaNamesToolStripMenuItem";
            this.areaNamesToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.N)));
            this.areaNamesToolStripMenuItem.Size = new System.Drawing.Size(189, 22);
            this.areaNamesToolStripMenuItem.Text = "Area &Names";
            this.areaNamesToolStripMenuItem.Click += new System.EventHandler(this.LabelsMenuItem_Click);
            // 
            // bordersToolStripMenuItem
            // 
            this.bordersToolStripMenuItem.Checked = true;
            this.bordersToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.bordersToolStripMenuItem.Name = "bordersToolStripMenuItem";
            this.bordersToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.B)));
            this.bordersToolStripMenuItem.Size = new System.Drawing.Size(189, 22);
            this.bordersToolStripMenuItem.Text = "Area &Borders";
            this.bordersToolStripMenuItem.Click += new System.EventHandler(this.BordersMenuItem_Click);
            // 
            // groupNamesToolStripMenuItem
            // 
            this.groupNamesToolStripMenuItem.Checked = true;
            this.groupNamesToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.groupNamesToolStripMenuItem.Name = "groupNamesToolStripMenuItem";
            this.groupNamesToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.U)));
            this.groupNamesToolStripMenuItem.Size = new System.Drawing.Size(189, 22);
            this.groupNamesToolStripMenuItem.Text = "Gro&up Names";
            this.groupNamesToolStripMenuItem.Click += new System.EventHandler(this.GroupNamesMenuItem_Click);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(186, 6);
            // 
            // whiteToolStripMenuItem
            // 
            this.whiteToolStripMenuItem.Name = "whiteToolStripMenuItem";
            this.whiteToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.W)));
            this.whiteToolStripMenuItem.Size = new System.Drawing.Size(189, 22);
            this.whiteToolStripMenuItem.Text = "&White";
            this.whiteToolStripMenuItem.Click += new System.EventHandler(this.WhiteMenuItem_Click);
            // 
            // yellowToolStripMenuItem
            // 
            this.yellowToolStripMenuItem.Checked = true;
            this.yellowToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.yellowToolStripMenuItem.Name = "yellowToolStripMenuItem";
            this.yellowToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Y)));
            this.yellowToolStripMenuItem.Size = new System.Drawing.Size(189, 22);
            this.yellowToolStripMenuItem.Text = "&Yellow";
            this.yellowToolStripMenuItem.Click += new System.EventHandler(this.YellowMenuItem_Click);
            // 
            // redToolStripMenuItem
            // 
            this.redToolStripMenuItem.Name = "redToolStripMenuItem";
            this.redToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.R)));
            this.redToolStripMenuItem.Size = new System.Drawing.Size(189, 22);
            this.redToolStripMenuItem.Text = "&Red";
            this.redToolStripMenuItem.Click += new System.EventHandler(this.RedMenuItem_Click);
            // 
            // blueToolStripMenuItem
            // 
            this.blueToolStripMenuItem.Name = "blueToolStripMenuItem";
            this.blueToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.E)));
            this.blueToolStripMenuItem.Size = new System.Drawing.Size(189, 22);
            this.blueToolStripMenuItem.Text = "Blu&e";
            this.blueToolStripMenuItem.Click += new System.EventHandler(this.BlueMenuItem_Click);
            // 
            // blackToolStripMenuItem
            // 
            this.blackToolStripMenuItem.Name = "blackToolStripMenuItem";
            this.blackToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.K)));
            this.blackToolStripMenuItem.Size = new System.Drawing.Size(189, 22);
            this.blackToolStripMenuItem.Text = "Blac&k";
            this.blackToolStripMenuItem.Click += new System.EventHandler(this.BlackMenuItem_Click);
            // 
            // windowToolStripMenuItem
            // 
            this.windowToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.logToolStripMenuItem});
            this.windowToolStripMenuItem.Name = "windowToolStripMenuItem";
            this.windowToolStripMenuItem.Size = new System.Drawing.Size(63, 20);
            this.windowToolStripMenuItem.Text = "&Window";
            // 
            // logToolStripMenuItem
            // 
            this.logToolStripMenuItem.Name = "logToolStripMenuItem";
            this.logToolStripMenuItem.Size = new System.Drawing.Size(94, 22);
            this.logToolStripMenuItem.Text = "&Log";
            this.logToolStripMenuItem.Click += new System.EventHandler(this.OpenLogMenuItem_Click);
            // 
            // boardEditor
            // 
            this.boardEditor.BackColor = System.Drawing.Color.Gray;
            this.boardEditor.BorderColor = System.Drawing.Color.Yellow;
            this.boardEditor.CtrlShortcut = null;
            this.boardEditor.Dock = System.Windows.Forms.DockStyle.Fill;
            this.boardEditor.Dragged = null;
            this.boardEditor.Location = new System.Drawing.Point(0, 24);
            this.boardEditor.Name = "boardEditor";
            this.boardEditor.Size = new System.Drawing.Size(800, 402);
            this.boardEditor.TabIndex = 2;
            this.boardEditor.Zoom = 0;
            this.boardEditor.ZoomChanged = null;
            // 
            // BoardGeneratorFrm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.boardEditor);
            this.Controls.Add(this.statusStrip);
            this.Controls.Add(this.menuStrip);
            this.MainMenuStrip = this.menuStrip;
            this.Name = "BoardGeneratorFrm";
            this.Text = "Board Generator";
            this.Load += new System.EventHandler(this.BoardGeneratorFrm_Load);
            this.statusStrip.ResumeLayout(false);
            this.statusStrip.PerformLayout();
            this.menuStrip.ResumeLayout(false);
            this.menuStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private StatusStrip statusStrip;
        private MenuStrip menuStrip;
        private ToolStripMenuItem fileToolStripMenuItem;
        private ToolStripMenuItem quitToolStripMenuItem;
        private ToolStripMenuItem windowToolStripMenuItem;
        private ToolStripStatusLabel statusLabel;
        private ToolStripMenuItem configurationToolStripMenuItem;
        private ToolStripMenuItem createExampleToolStripMenuItem;
        private ToolStripMenuItem loadToolStripMenuItem;
        private ToolStripMenuItem logToolStripMenuItem;
        private Control.BoardEditor boardEditor;
        private ToolStripMenuItem reloadToolStripMenuItem;
        private ToolStripMenuItem autoReloadToolStripMenuItem;
        private ToolStripStatusLabel positionLabel;
        private ToolStripStatusLabel zoomLabel;
        private ToolStripMenuItem viewToolStripMenuItem;
        private ToolStripMenuItem areaNamesToolStripMenuItem;
        private ToolStripMenuItem bordersToolStripMenuItem;
        private ToolStripMenuItem boardToolStripMenuItem;
        private ToolStripMenuItem generateToolStripMenuItem;
        private ToolStripMenuItem lockAllAreasToolStripMenuItem;
        private ToolStripMenuItem unlockAllAreasToolStripMenuItem;
        private ToolStripMenuItem saveToolStripMenuItem;
        private ToolStripMenuItem saveAsToolStripMenuItem;
        private ToolStripMenuItem groupNamesToolStripMenuItem;
        private ToolStripSeparator toolStripMenuItem1;
        private ToolStripMenuItem whiteToolStripMenuItem;
        private ToolStripMenuItem yellowToolStripMenuItem;
        private ToolStripMenuItem redToolStripMenuItem;
        private ToolStripMenuItem blueToolStripMenuItem;
        private ToolStripMenuItem blackToolStripMenuItem;
    }
}