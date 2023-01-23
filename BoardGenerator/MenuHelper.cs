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
                string title = "Select Configuration File";

                string filter = "JSON Configuration (*.json)|*.json";

                using (var openFileDialog = FileHelper.ShowOpenFileDialog(title, filter))
                {
                    string filePath = openFileDialog.FileName;

                    if (filePath != "")
                    {
                        Stream fileStream = openFileDialog.OpenFile();

                        result = InnerLoadConfig(frm, fileStream, filePath);

                        string basePath = string.IsNullOrWhiteSpace(result.BasePath)
                            ? Path.GetDirectoryName(filePath)
                            : result.BasePath;

                        frm.BasePath = basePath;

                        frm.ConfigFilePath = filePath;

                        frm.Config.WatchFile(filePath);
                    }
                    else
                    {
                        frm.SetStatus("Loading configuration was cancelled");
                    }
                }
            }
            catch (Exception ex)
            {
                frm.SetError("An error occurred while loading configuration");
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
                frm.SetError("An error occurred while loading configuration");
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


            string title = "Save Configuration File";

            string filter = "JSON Configuration|*.json";

            using (SaveFileDialog sfd = FileHelper.ShowSaveFileDialog(title, filter))
            {
                if (sfd.FileName != "")
                {
                    SaveConfiguration(frm, example, sfd.FileName);
                }
                else
                {
                    frm.SetStatus("Creating example configuration was cancelled");
                }
            }
        }

        public static void SaveConfiguration(BoardGeneratorFrm frm,
            Configuration config, string filePath)
        {
            if (config == null)
            {
                frm.SetError("Could not save, no configuration loaded.");

                return;
            }

            try
            {
                string json = frm.Config.Serialize(config);

                FileHelper.SaveFile(filePath, json);

                frm.SetStatus($"Configuration saved to: {filePath}");
            }
            catch (Exception ex)
            {
                frm.SetError("An error occurred while saving configuration");
                Logging.Log($"{ex}");
            }
        }

        public static void LockAllAreas(
            BoardGeneratorFrm frm, Configuration config)
        {
            if (config == null)
            {
                frm.SetError("Could not lock areas, no configuration loaded.");
                return;
            }

            if (!config.Areas?.Any() == true)
            {
                frm.SetError("Could not lock areas, no areas in configuration.");
                return;
            }

            foreach (Area area in config.Areas)
            {
                area.Locked = true;
            }
        }

        public static void UnlockAllAreas(
            BoardGeneratorFrm frm, Configuration config)
        {
            if (config == null)
            {
                frm.SetError("Could not unlock areas, no configuration loaded.");
                return;
            }

            if (!config.Areas?.Any() == true)
            {
                frm.SetError("Could not unlock areas, no areas in configuration.");
                return;
            }

            foreach (Area area in config.Areas)
            {
                area.Locked = null;
            }
        }
    }
}
