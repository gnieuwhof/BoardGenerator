namespace BoardGenerator
{
    using BoardGenerator.Conf;
    using System;

    internal static class MenuHelper
    {
        public static Configuration LoadConfiguration(BoardGeneratorFrm frm)
        {
            _ = frm ?? throw new ArgumentNullException(nameof(frm));

            Logging.EnsureEmptyLine();


            Configuration result = null;

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

                        result = InnerLoadConfig(frm, fileStream, filePath);

                        frm.ConfigFilePath = filePath;

                        frm.Config.WatchFile(filePath);
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

            return result;
        }

        public static Configuration LoadConfiguration(
            BoardGeneratorFrm frm, string filePath)
        {
            _ = frm ?? throw new ArgumentNullException(nameof(frm));

            Logging.EnsureEmptyLine();


            Configuration result = null;

            try
            {
                Stream fileStream = File.Open(filePath, FileMode.Open);

                result = InnerLoadConfig(frm, fileStream, filePath);
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

            return result;
        }

        private static Configuration InnerLoadConfig(
            BoardGeneratorFrm frm, Stream fileStream, string filePath)
        {
            string fileContent = FileHelper.ReadFromStream(fileStream);

            Configuration result = frm.Config.Deserialize(fileContent);

            Logging.Log($"Loaded configuration file: {filePath}");

            frm.SetStatus("Configuration loaded");

            return result;
        }


        public static void CreateConfigurationExample(BoardGeneratorFrm frm)
        {
            _ = frm ?? throw new ArgumentNullException(nameof(frm));

            Logging.EnsureEmptyLine();

            var example = frm.Config.CreateExample();

            try
            {
                var sfd = new SaveFileDialog();
                sfd.Filter = "JSON Configuration|*.json";
                sfd.Title = "Save Configuration File";
                sfd.ShowDialog();

                if (sfd.FileName != "")
                {
                    var fs = (System.IO.FileStream)sfd.OpenFile();

                    string json = frm.Config.Serialize(example);

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
