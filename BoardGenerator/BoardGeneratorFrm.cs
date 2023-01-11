using BoardGenerator.Conf;

namespace BoardGenerator
{
    public partial class BoardGeneratorFrm : Form
    {
        private LogFrm logFrm;


        public BoardGeneratorFrm()
        {
            InitializeComponent();
        }


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

            this.SetConfiguration(config);
        }

        private void ReloadMenuItem_Click(object sender, EventArgs e)
        {
            if (this.ConfigFilePath == null)
            {
                return;
            }

            Configuration config = MenuHelper.LoadConfiguration(
                this, this.ConfigFilePath);

            this.SetConfiguration(config);
        }

        private void SetConfiguration(Configuration config)
        {
            if (config == null)
            {
                return;
            }

            this.boardEditor.SetConfiguration(config.Areas);
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
    }
}
