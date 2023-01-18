﻿namespace BoardGenerator
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

            using (SaveFileDialog sfd = ShowSaveFileDialog())
            {
                if (sfd.FileName != "")
                {
                    using (var fs = (System.IO.FileStream)sfd.OpenFile())
                    {
                        SaveConfiguration(frm, example, fs, sfd.FileName);
                    }

                    frm.SetStatus("Example configuration saved");
                }
                else
                {
                    frm.SetStatus("Creating example configuration was cancelled");
                }
            }
        }

        public static SaveFileDialog ShowSaveFileDialog()
        {
            var sfd = new SaveFileDialog();

            sfd.Filter = "JSON Configuration|*.json";
            sfd.Title = "Save Configuration File";

            sfd.ShowDialog();

            return sfd;
        }

        public static void SaveConfiguration(BoardGeneratorFrm frm,
            Configuration config, FileStream fs, string filePath)
        {
            if (config == null)
            {
                frm.SetError("Could not save, no configuration loaded.");
                return;
            }

            try
            {
                string json = frm.Config.Serialize(config);

                FileHelper.WriteToStream(fs, json);

                fs.Close();

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
