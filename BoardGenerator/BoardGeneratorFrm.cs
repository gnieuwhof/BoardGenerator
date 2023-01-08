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


        private void BoardGeneratorFrm_Load(object sender, EventArgs e)
        {
            this.SetStatus("Program started");
        }

        private void SetStatus(string status)
        {
            Logging.Log($"Status updated to: {status}");
            Logging.Empty();

            string timestamp = DateTime.Now.ToString("HH:mm:ss");

            this.toolStripStatusLabel.Text = $"[{timestamp}] {status}";
        }

        private void loadToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Logging.Empty();

            bool success = Config.TryLoadConfiguration(out Configuration config);

            if (success)
            {
                this.SetStatus("Configuration loaded");

                var bounds = Config.GetBounds(config);
                Logging.Log($"Bounds: L:{bounds.X} T:{bounds.Y} R:{bounds.Right} B:{bounds.Bottom}");
            }
            else
            {
                this.SetStatus("Configuration was not loaded");
            }
        }

        private void createExampleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Logging.Empty();

            var example = Config.CreateExample();

            bool success = Config.SaveConfiguration(example);

            if (success)
            {
                this.SetStatus("Example configuration saved");
            }
            else
            {
                this.SetStatus("Creating example configuration was cancelled");
            }
        }

        private void logToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if ((logFrm == null) || (logFrm.IsDisposed))
            {
                logFrm = new LogFrm();

                Logging.SetLogFrm(logFrm);
            }

            logFrm.Show();
        }
    }
}
