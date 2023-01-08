namespace BoardGenerator
{
    using System;

    internal static class MenuHelper
    {
        public static void LoadConfiguration(BoardGeneratorFrm frm)
        {
            _ = frm ?? throw new ArgumentNullException(nameof(frm));

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

                        frm.SetStatus("Configuration loaded");

                        var bounds = Config.GetBounds(config);
                    }
                }
            }
            catch (Exception ex)
            {
                string error = "An error occurred while loading configuration";
                frm.SetStatus(error);
                Logging.Log(error);
                Logging.Log($"{ex}");

                MessageBox.Show("Loading configuration failed.",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public static void CreateConfigurationExample(BoardGeneratorFrm frm)
        {
            _ = frm ?? throw new ArgumentNullException(nameof(frm));

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

                    frm.SetStatus("Example configuration saved");
                }
                else
                {
                    frm.SetStatus("Creating example configuration was cancelled");
                }
            }
            catch (Exception ex)
            {
                string error = "An error occurred while saving configuration";
                frm.SetStatus(error);
                Logging.Log(error);
                Logging.Log($"{ex}");
            }
        }
    }
}
