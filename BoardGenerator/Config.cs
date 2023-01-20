namespace BoardGenerator
{
    using BoardGenerator.Conf;
    using Newtonsoft.Json;

    public class Config
    {
        private readonly BoardGeneratorFrm frm;
        private readonly FileSystemWatcher watcher;


        public Config(BoardGeneratorFrm frm)
        {
            this.frm = frm ??
                throw new ArgumentNullException(nameof(frm));

            watcher = new FileSystemWatcher();

            watcher.NotifyFilter =
                //NotifyFilters.LastAccess |
                NotifyFilters.LastWrite
                //| NotifyFilters.FileName | NotifyFilters.DirectoryName
                ;

            watcher.Changed += new FileSystemEventHandler(OnChanged);
            //watcher.Created += new FileSystemEventHandler(OnChanged);
            //watcher.Deleted += new FileSystemEventHandler(OnChanged);
            //watcher.Renamed += new RenamedEventHandler(OnRenamed);
        }


        public Configuration Deserialize(string json)
        {
            Configuration config = JsonConvert
                .DeserializeObject<Configuration>(json);

            return config;
        }

        public string Serialize(Configuration config)
        {
            var settings = new JsonSerializerSettings
            {
                Formatting = Formatting.Indented,
                NullValueHandling = NullValueHandling.Ignore,
            };

            string json = JsonConvert.SerializeObject(config, settings);

            return json;
        }

        public Rectangle GetBounds(Configuration config)
        {
            _ = config ??
                throw new ArgumentNullException(nameof(config));

            Rectangle result = GetBounds(config.Areas);

            return result;
        }

        public static Rectangle GetBounds(Area[] areas)
        {
            Logging.EnsureEmptyLine();
            Logging.Log("Getting Bounds");

            int left = 0;
            int top = 0;
            int right = 0;
            int bottom = 0;

            if (areas != null)
            {
                foreach (Area area in areas)
                {
                    if (area.X < left)
                    {
                        left = area.X;
                    }
                    if (area.Y < top)
                    {
                        top = area.Y;
                    }

                    int areaRight = area.X + area.Width;
                    if (areaRight > right)
                    {
                        right = areaRight;
                    }

                    int areaBottom = area.Y + area.Height;
                    if (areaBottom > bottom)
                    {
                        bottom = areaBottom;
                    }
                }
            }
            else
            {
                Logging.LogLine("WARN: No areas found in config");
            }

            int width = right - left;
            int height = bottom - top;

            Logging.LogLine(
                $"Bounds (left:{left}, top:{top}, right:{right}, bottom:{bottom})");

            var result = new Rectangle(left, top, width, height);

            return result;
        }

        public Configuration CreateExample()
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

            Logging.LogLine("Example configuration created");

            return config;
        }

        public void WatchFile(string filePath)
        {
            string directory = Path.GetDirectoryName(filePath);
            string fileName = Path.GetFileName(filePath);

            watcher.Path = directory;

            watcher.Filter = fileName;

            watcher.EnableRaisingEvents = true;
        }

        private void OnChanged(object source, FileSystemEventArgs e)
        {
            this.frm.ConfigFileChanged();
        }
    }
}
