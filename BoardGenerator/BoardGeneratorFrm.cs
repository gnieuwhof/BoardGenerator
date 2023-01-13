using BoardGenerator.Conf;

namespace BoardGenerator
{
    public partial class BoardGeneratorFrm : Form
    {
        private LogFrm logFrm;
        private bool autoReloadConfig = false;
        private delegate void ReloadCallback();


        public BoardGeneratorFrm()
        {
            InitializeComponent();

            this.Config = new Config(this);
        }


        public Config Config { get; }

        public string ConfigFilePath { get; set; }


        private void BoardGeneratorFrm_Load(object sender, EventArgs e)
        {
            this.SetStatus("Program started");
        }

        public void SetStatus(string status)
        {
            string timestamp = DateTime.Now.ToString("HH:mm:ss");

            this.toolStripStatusLabel.Text = $"[{timestamp}] {status}";

            Logging.Log($"Status updated to: {status}");
            Logging.EnsureEmptyLine();
        }

        private void QuitMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void LoadConfigurationMenuItem_Click(object sender, EventArgs e)
        {
            Configuration config = MenuHelper.LoadConfiguration(this);

            this.SetConfiguration(config, resetPosition: true);
        }

        private void ReloadMenuItem_Click(object sender, EventArgs e)
        {
            this.InnerReloadConfig();
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

            Configuration config = MenuHelper.LoadConfiguration(
                this, this.ConfigFilePath);

            this.SetConfiguration(config, resetPosition: false);
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
    }
}
