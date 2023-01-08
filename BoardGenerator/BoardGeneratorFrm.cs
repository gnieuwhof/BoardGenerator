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
            Logging.EnsureEmptyLine();

            string timestamp = DateTime.Now.ToString("HH:mm:ss");

            this.toolStripStatusLabel.Text = $"[{timestamp}] {status}";
        }

        private void QuitMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void LoadConfigurationMenuItem_Click(object sender, EventArgs e)
        {
            Logging.EnsureEmptyLine();

            try
            {
                using (var openFileDialog = new OpenFileDialog())
                {
                    openFileDialog.Filter = "JSON Configuration (*.json)|*.json";
                    openFileDialog.RestoreDirectory = true;

                    if (openFileDialog.ShowDialog() == DialogResult.OK)
                    {
                        string filePath = openFileDialog.FileName;

                        Stream fileStream = openFileDialog.OpenFile();

                        string fileContent = FileHelper.ReadFromStream(fileStream);

                        var config = Config.Deserialize(fileContent);

                        Logging.Log($"Loaded configuration file: {filePath}");

                        this.SetStatus("Configuration loaded");

                        var bounds = Config.GetBounds(config);
                    }
                }
            }
            catch (Exception ex)
            {
                string error = "An error occurred while loading configuration";
                this.SetStatus(error);
                Logging.Log(error);
                Logging.Log($"{ex}");

                MessageBox.Show("Loading configuration failed.",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void CreateConfigurationExampleMenuItem_Click(object sender, EventArgs e)
        {
            Logging.EnsureEmptyLine();

            var example = Config.CreateExample();

            try
            {
                var sfd = new SaveFileDialog();
                sfd.Filter = "JSON Configuration|*.json";
                sfd.Title = "Save Configuration File";
                sfd.ShowDialog();

                if (sfd.FileName != "")
                {
                    var fs = (System.IO.FileStream)sfd.OpenFile();

                    string json = Config.Serialize(example);

                    FileHelper.WriteToStream(fs, json);

                    fs.Close();

                    Logging.Log($"Configuration saved to: {sfd.FileName}");

                    this.SetStatus("Example configuration saved");
                }
                else
                {
                    this.SetStatus("Creating example configuration was cancelled");
                }
            }
            catch (Exception ex)
            {
                string error = "An error occurred while saving configuration";
                this.SetStatus(error);
                Logging.Log(error);
                Logging.Log($"{ex}");
            }
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
