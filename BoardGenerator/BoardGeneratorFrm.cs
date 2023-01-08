using BoardGenerator.Conf;

namespace BoardGenerator
{
    public partial class BoardGeneratorFrm : Form
    {
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
            string timestamp = DateTime.Now.ToString("HH:mm:ss");

            this.toolStripStatusLabel.Text = $"[{timestamp}] {status}";
        }

        private void loadToolStripMenuItem_Click(object sender, EventArgs e)
        {
            bool success = Config.TryLoadConfiguration(out Configuration config);

            if(success)
            {
                this.SetStatus("Configuration loaded");
                var _ = config;
            }
            else
            {
                this.SetStatus("Configuration was not loaded");
            }
        }

        private void createExampleToolStripMenuItem_Click(object sender, EventArgs e)
        {
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
    }
}
