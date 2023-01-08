namespace BoardGenerator
{
    using BoardGenerator.Conf;
    using Newtonsoft.Json;
    using System.Text;

    internal static class Config
    {
        public static bool TryLoadConfiguration(out Configuration config)
        {
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

                        string fileContent;
                        using (var reader = new StreamReader(fileStream))
                        {
                            fileContent = reader.ReadToEnd();
                        }

                        config = JsonConvert.DeserializeObject<Configuration>(fileContent);

                        Logging.Log($"Loaded configuration file: {filePath}");

                        return true;
                    }
                }
            }
            catch (Exception ex)
            {
                Logging.Log("An error occurred while loading configuration");
                Logging.Log($"{ex}");
            }

            config = null;
            return false;
        }

        public static bool SaveConfiguration(Configuration config)
        {
            try
            {
                var sfd = new SaveFileDialog();
                sfd.Filter = "JSON Configuration|*.json";
                sfd.Title = "Save a Configuration File";
                sfd.ShowDialog();

                if (sfd.FileName != "")
                {
                    var fs = (System.IO.FileStream)sfd.OpenFile();

                    var settings = new JsonSerializerSettings
                    {
                        Formatting = Formatting.Indented,
                        NullValueHandling = NullValueHandling.Ignore,
                    };

                    string json = JsonConvert.SerializeObject(config, settings);

                    byte[] bytes = Encoding.UTF8.GetBytes(json);

                    fs.Write(bytes, 0, bytes.Length);

                    fs.Close();

                    Logging.Log($"Configuration saved to: {sfd.FileName}");

                    return true;
                }
            }
            catch (Exception ex)
            {
                Logging.Log("An error occurred while saving configuration");
                Logging.Log($"{ex}");
                return false;
            }

            Logging.Log("Saving configuration was cancelled");

            return false;
        }

        public static Rectangle GetBounds(Configuration config)
        {
            _ = config ??
                throw new ArgumentNullException(nameof(config));

            int left = 0;
            int top = 0;
            int right = 0;
            int bottom = 0;

            if (config.Areas != null)
            {
                foreach (Area area in config.Areas)
                {
                    if(area.X < left)
                    {
                        left = area.X;
                    }
                    if(area.Y < top)
                    {
                        top = area.Y;
                    }

                    int areaRight = area.X + area.Width;
                    if(areaRight > right)
                    {
                        right = areaRight;
                    }

                    int areaBottom = area.Y + area.Height;
                    if(areaBottom > bottom)
                    {
                        bottom = areaBottom;
                    }
                }
            }

            int width = right - left;
            int height = bottom - top;

            var result = new Rectangle(left, top, width, height);

            return result;
        }

        public static Configuration CreateExample()
        {
            var area1 = new Area
            {
                Name = "Area1",
                X = 0,
                Y = 0,
                Z = 0,
                Width = 100,
                Height = 100
            };

            var area2 = new Area
            {
                Name = "Area2",
                X = 0,
                Y = 100,
                Z = 0,
                Width = 100,
                Height = 100,
                Folder = "Area Two"
            };

            var area3 = new Area
            {
                Name = "Area3",
                X = 100,
                Y = 0,
                Z = 0,
                Width = 100,
                Height = 100,
                Group = "Right"
            };

            var area4 = new Area
            {
                Name = "Area4",
                X = 100,
                Y = 100,
                Z = 0,
                Width = 100,
                Height = 100,
                Group = "Right",
                Exclusive = true
            };

            var config = new Configuration
            {
                Areas = new[] { area1, area2, area3, area4 }
            };

            Logging.Log("Example configuration created");

            return config;
        }
    }
}
