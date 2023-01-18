using BoardGenerator.Conf;

namespace BoardGenerator
{
    public partial class BoardGeneratorFrm : Form
    {
        private LogFrm logFrm;
        private bool autoReloadConfig = false;
        private delegate void ReloadCallback();
        private Configuration configuration;


        public BoardGeneratorFrm()
        {
            InitializeComponent();

            this.Config = new Config(this);

            this.boardEditor.Dragged = this.BoardDragged;
            this.boardEditor.ZoomChanged = this.Zoomed;
            this.boardEditor.CtrlShortcut = this.CtrlShortcut;
        }


        public Config Config { get; }

        public string ConfigFilePath { get; set; }


        private void BoardGeneratorFrm_Load(object sender, EventArgs e)
        {
            this.SetStatus("Program started");
        }

        public void SetError(string error) =>
            this.SetStatus(error, Color.Yellow);

        public void SetStatus(string status) =>
            this.SetStatus(status, SystemColors.Control);

        public void SetStatus(string status, Color color)
        {
            string timestamp = DateTime.Now.ToString("HH:mm:ss");

            this.statusLabel.BackColor = color;
            this.statusLabel.Text = $"[{timestamp}] {status}";

            Logging.Log($"Status updated to: {status}");
            Logging.EnsureEmptyLine();
        }

        private void QuitMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void LoadConfigurationMenuItem_Click(object sender, EventArgs e)
        {
            this.configuration = MenuHelper.LoadConfiguration(this);

            this.SetConfiguration(this.configuration, resetPosition: true);
        }

        private void ReloadMenuItem_Click(object sender, EventArgs e)
        {
            this.InnerReloadConfig();
        }

        private void BoardDragged()
        {
            int x = this.boardEditor.Position.X;
            int y = this.boardEditor.Position.Y;
            this.positionLabel.Text = $"X: {x}, Y: {y}";
        }

        private void Zoomed()
        {
            this.zoomLabel.Text = $"Zoom: {this.boardEditor.Zoom}";
        }

        public void ConfigFileChanged()
        {
            if (!this.autoReloadConfig)
            {
                return;
            }

            if (this.boardEditor.InvokeRequired)
            {
                var callback = new ReloadCallback(this.InnerReloadConfig);
                this.Invoke(callback);
            }
            else
            {
                this.InnerReloadConfig();
            }
        }

        private void InnerReloadConfig()
        {
            if (this.ConfigFilePath == null)
            {
                return;
            }

            int counter = 1;
            int delayMs = 1;

            do
            {
                Thread.Sleep(delayMs);

                ++counter;

                if (counter > 5)
                {
                    // Loading the file will probably throw, too bad.
                    break;
                }

                delayMs *= 2;
            }
            while (FileHelper.IsFileInUse(this.ConfigFilePath));

            this.configuration = MenuHelper.LoadConfiguration(
                this, this.ConfigFilePath);

            this.SetConfiguration(this.configuration, resetPosition: false);

            this.SetStatus("Configuration reloaded");
        }

        private void SetConfiguration(Configuration config, bool resetPosition)
        {
            if (config == null)
            {
                return;
            }

            this.boardEditor.SetConfiguration(config.Areas, resetPosition);
        }


        private void CreateConfigurationExampleMenuItem_Click(object sender, EventArgs e)
        {
            MenuHelper.CreateConfigurationExample(this);
        }

        private void OpenLogMenuItem_Click(object sender, EventArgs e)
        {
            if ((logFrm == null) || (logFrm.IsDisposed))
            {
                logFrm = new LogFrm();

                Logging.SetLogFrm(logFrm);
            }

            logFrm.Show();
            logFrm.BringToFront();
        }

        private void AutoReloadMenuItem_Click(object sender, EventArgs e)
        {
            this.autoReloadConfig = !this.autoReloadConfig;

            this.autoReloadToolStripMenuItem.Checked = this.autoReloadConfig;
        }

        private void bordersMenuItem_Click(object sender, EventArgs e)
        {
            this.bordersToolStripMenuItem.Checked =
                !this.bordersToolStripMenuItem.Checked;

            this.boardEditor.SetDrawBorders(
                this.bordersToolStripMenuItem.Checked);
        }

        private void labelsMenuItem_Click(object sender, EventArgs e)
        {
            this.labelsToolStripMenuItem.Checked =
                !this.labelsToolStripMenuItem.Checked;

            this.boardEditor.SetDrawLabels(
                this.labelsToolStripMenuItem.Checked);
        }

        private void GenerateMenuItem_Click(object sender, EventArgs e)
        {
            this.GenerateBoard();
        }

        private void GenerateBoard()
        {
            if (this.configuration == null)
            {
                this.SetError("Could not generate, no configuration loaded.");
                return;
            }

            Generator.Generate(this, this.configuration);

            this.boardEditor.Refresh();

            this.SetStatus("Board generated");
        }

        private void CtrlShortcut(Keys keys)
        {
            if (keys == Keys.R)
            {
            }
        }

        private void LockAllAreasMenuItem_Click(object sender, EventArgs e)
        {
            MenuHelper.LockAllAreas(this, this.configuration);

            this.boardEditor.Refresh();
        }

        private void UnlockAllAreasMenuItem_Click(object sender, EventArgs e)
        {
            MenuHelper.UnlockAllAreas(this, this.configuration);

            this.boardEditor.Refresh();
        }

        private void SaveMenuItem_Click(object sender, EventArgs e)
        {
            string filePath = this.ConfigFilePath;

            if (!File.Exists(filePath))
            {
                this.SaveAs();

                return;
            }

            using (var fs = new FileStream(filePath,
                FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.None))
            {
                MenuHelper.SaveConfiguration(this, this.configuration, fs, filePath);
            }
        }

        private void SaveAsMenuItem_Click(object sender, EventArgs e)
        {
            this.SaveAs();
        }

        private void SaveAs()
        {
            using (SaveFileDialog sfd = MenuHelper.ShowSaveFileDialog())
            {
                try
                {
                    if (sfd.FileName != "")
                    {
                        using (var fs = (System.IO.FileStream)sfd.OpenFile())
                        {
                            MenuHelper.SaveConfiguration(this,
                                this.configuration, fs, sfd.FileName);
                        }
                    }
                    else
                    {
                        this.SetStatus("Saving configuration was cancelled");
                    }
                }
                catch (Exception ex)
                {
                    this.SetError("An error occurred while saving configuration");
                    Logging.Log($"{ex}");
                }
            }
        }
    }
}
