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
            this.menuStrip = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.quitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.configurationToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.loadToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.reloadToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.autoReloadToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.createExampleToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.windowToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.logToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.boardEditor = new BoardGenerator.Control.BoardEditor();
            this.zoomLabel = new System.Windows.Forms.ToolStripStatusLabel();
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
            // menuStrip
            // 
            this.menuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.configurationToolStripMenuItem,
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
            this.quitToolStripMenuItem.Size = new System.Drawing.Size(93, 22);
            this.quitToolStripMenuItem.Text = "&Exit";
            this.quitToolStripMenuItem.Click += new System.EventHandler(this.QuitMenuItem_Click);
            // 
            // configurationToolStripMenuItem
            // 
            this.configurationToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.loadToolStripMenuItem,
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
            this.loadToolStripMenuItem.Size = new System.Drawing.Size(156, 22);
            this.loadToolStripMenuItem.Text = "&Load";
            this.loadToolStripMenuItem.Click += new System.EventHandler(this.LoadConfigurationMenuItem_Click);
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
            this.boardEditor.Dock = System.Windows.Forms.DockStyle.Fill;
            this.boardEditor.Location = new System.Drawing.Point(0, 24);
            this.boardEditor.Name = "boardEditor";
            this.boardEditor.Size = new System.Drawing.Size(800, 402);
            this.boardEditor.TabIndex = 2;
            this.boardEditor.Zoom = 0;
            // 
            // zoomLabel
            // 
            this.zoomLabel.BorderSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.Left;
            this.zoomLabel.BorderStyle = System.Windows.Forms.Border3DStyle.Etched;
            this.zoomLabel.Name = "zoomLabel";
            this.zoomLabel.Size = new System.Drawing.Size(55, 19);
            this.zoomLabel.Text = "Zoom: 0";
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
    }
}