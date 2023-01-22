using BoardGenerator.Conf;
using System.Drawing.Imaging;

namespace BoardGenerator
{
    public partial class BoardGeneratorFrm : Form
    {
        private LogFrm logFrm;
        private bool autoReloadConfig = false;
        private delegate void CrossThreadCallback();
        private Configuration configuration;
        private DateTime statusBackColorSetToError;


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

        public async void SetError(string error)
        {
            this.SetStatus(error, Color.Yellow);

            var now = DateTime.Now;

            this.statusBackColorSetToError = now;

            await Task.Run(async () =>
            {
                await Task.Delay(TimeSpan.FromSeconds(5));

                if (this.statusBackColorSetToError != now)
                {
                    return;
                }

                var callback = new CrossThreadCallback(this.ResetLabelBackground);

                this.Invoke(callback);
            });
        }

        private void ResetLabelBackground()
        {
            this.statusLabel.BackColor = SystemColors.Control;
        }

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

            this.boardEditor.BasePath = this.configuration.BasePath;

            this.SetConfiguration(this.configuration, resetPosition: true);
        }

        private void ReloadMenuItem_Click(object sender, EventArgs e)
        {
            if (this.configuration == null)
            {
                this.SetError("Could not reload, no configuration loaded.");

                return;
            }

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
                var callback = new CrossThreadCallback(this.InnerReloadConfig);

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

            this.boardEditor.Cache = new ImageCache(config.CacheSize);

            this.boardEditor.SetConfiguration(config.Areas, resetPosition);
        }


        private void CreateConfigurationExampleMenuItem_Click(object sender, EventArgs e)
        {
            MenuHelper.CreateConfigurationExample(this);
        }

        private void AutoReloadMenuItem_Click(object sender, EventArgs e)
        {
            this.autoReloadConfig = !this.autoReloadConfig;

            this.autoReloadToolStripMenuItem.Checked = this.autoReloadConfig;
        }

        private void CtrlShortcut(Keys keys)
        {
            if (keys == Keys.R)
            {
            }
        }

        private void SaveMenuItem_Click(object sender, EventArgs e)
        {
            if (this.configuration == null)
            {
                this.SetError("Could not save, no configuration loaded.");

                return;
            }

            string filePath = this.ConfigFilePath;

            if (!File.Exists(filePath))
            {
                this.SaveAs();

                return;
            }

            MenuHelper.SaveConfiguration(this, this.configuration, filePath);
        }

        private void SaveAsMenuItem_Click(object sender, EventArgs e)
        {
            if (this.configuration == null)
            {
                this.SetError("Could not save, no configuration loaded.");

                return;
            }

            this.SaveAs();
        }

        private void SaveAs()
        {
            string title = "Save Configuration File";
            string filter = "JSON Configuration|*.json";
            using (SaveFileDialog sfd = FileHelper.ShowSaveFileDialog(title, filter))
            {
                try
                {
                    if (sfd.FileName != "")
                    {
                        MenuHelper.SaveConfiguration(this,
                            this.configuration, sfd.FileName);
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

        private void GenerateMenuItem_Click(object sender, EventArgs e)
        {
            if (this.configuration == null)
            {
                this.SetError("Could not generate, no configuration loaded.");
                return;
            }

            Generator.Generate(this, this.configuration);

            this.boardEditor.Refresh();
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

        private void BordersMenuItem_Click(object sender, EventArgs e)
        {
            this.bordersToolStripMenuItem.Checked =
                !this.bordersToolStripMenuItem.Checked;

            this.boardEditor.SetDrawBorders(
                this.bordersToolStripMenuItem.Checked);
        }

        private void LabelsMenuItem_Click(object sender, EventArgs e)
        {
            this.areaNamesToolStripMenuItem.Checked =
                !this.areaNamesToolStripMenuItem.Checked;

            this.boardEditor.SetDrawLabels(
                this.areaNamesToolStripMenuItem.Checked);
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

        private void GroupNamesMenuItem_Click(object sender, EventArgs e)
        {
            this.groupNamesToolStripMenuItem.Checked =
                !this.groupNamesToolStripMenuItem.Checked;

            this.boardEditor.SetDrawGroups(
                this.groupNamesToolStripMenuItem.Checked);
        }


        private void WhiteMenuItem_Click(object sender, EventArgs e)
        {
            this.SetOverlayColor(Color.White);
        }

        private void YellowMenuItem_Click(object sender, EventArgs e)
        {
            this.SetOverlayColor(Color.Yellow);
        }

        private void RedMenuItem_Click(object sender, EventArgs e)
        {
            this.SetOverlayColor(Color.Red);
        }

        private void BlueMenuItem_Click(object sender, EventArgs e)
        {
            this.SetOverlayColor(Color.Blue);
        }

        private void BlackMenuItem_Click(object sender, EventArgs e)
        {
            this.SetOverlayColor(Color.Black);
        }

        private void SetOverlayColor(Color color)
        {
            this.whiteToolStripMenuItem.Checked = (color == Color.White);
            this.yellowToolStripMenuItem.Checked = (color == Color.Yellow);
            this.redToolStripMenuItem.Checked = (color == Color.Red);
            this.blueToolStripMenuItem.Checked = (color == Color.Blue);
            this.blackToolStripMenuItem.Checked = (color == Color.Black);

            Brush textColor = null;

            if (color == Color.White)
                textColor = Brushes.White;
            if (color == Color.Yellow)
                textColor = Brushes.Yellow;
            if (color == Color.Red)
                textColor = Brushes.Red;
            if (color == Color.Blue)
                textColor = Brushes.Blue;
            if (color == Color.Black)
                textColor = Brushes.Black;

            this.boardEditor.BorderColor = color;
            this.boardEditor.TextColor = textColor;

            this.boardEditor.Refresh();

            this.SetStatus($"Set Overlay {color}");
        }

        private void ExportMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                Bitmap bitmap = this.boardEditor.Export();

                using (var fsd = FileHelper.ShowSaveFileDialog("Export Board", "PNG|*.png"))
                {
                    if (fsd != null)
                    {
                        if (fsd.FileName != "")
                        {
                            var imageEncoders = ImageCodecInfo.GetImageEncoders();
                            var encoderParameters = new EncoderParameters(1);
                            encoderParameters.Param[0] = new EncoderParameter(Encoder.Quality, 100L);

                            bitmap.Save(fsd.FileName, imageEncoders[4], encoderParameters);

                            this.SetStatus($"The board was exported to: {fsd.FileName}");
                        }
                        else
                        {
                            this.SetStatus("Exporing the board was cancelled");
                        }
                    }
                    else
                    {
                        this.SetStatus("Exporing the board was cancelled");
                    }
                }
            }
            catch (Exception ex)
            {
                this.SetError("An error occurred while exporting the board");
                Logging.Log($"{ex}");
            }
        }
    }
}
