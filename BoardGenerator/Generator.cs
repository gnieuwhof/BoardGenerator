namespace BoardGenerator
{
    using BoardGenerator.Conf;
    using System.Globalization;

    internal static class Generator
    {
        private const int DEFAULT_WEIGHT = 100;

        private static readonly Random random =
            new Random(Guid.NewGuid().GetHashCode());

        private static readonly string[] IMAGE_EXTENSIONS =
            new[] { "png", "jpg", "jpeg", "bmp" };


        public static void Generate(BoardGeneratorFrm frm, Configuration config)
        {
            Logging.EnsureEmptyLine();
            Logging.Log("Regenerating board");

            string basePath = FileHelper.GetBasePath(config.BasePath);

            Logging.Log("Getting Area paths");
            Logging.Log($"Base path: {basePath}");

            if (!Directory.Exists(basePath))
            {
                Logging.Log($"The base folder {basePath} does not exists");
                frm.SetError("Regenerating board failed (base path does not exist)");
                return;
            }

            Dictionary<Area, string> areaPaths =
                GetAreaFolders(basePath, config.Areas);

            var grouped = config.Areas.GroupBy(a => a.Group);
            foreach (var group in grouped)
            {
                var ordered = group
                    .OrderByDescending(g => g.Exclusive)
                    .ThenByDescending(g => g.Locked);

                var excludeList = new List<string>();
                foreach (Area area in ordered)
                {
                    string path = areaPaths[area];

                    if (area.Locked == true)
                    {
                        if ((area.Exclusive == true) &&
                            File.Exists(area.File))
                        {
                            excludeList.Add(area.File);
                        }

                        continue;
                    }

                    Logging.Log($"Area {area.Name} path: {path}");

                    if (!Directory.Exists(path))
                    {
                        Logging.Log($"Area folder {path} for area {area} does not exists");
                        frm.SetError("Regenerating board failed (area path does not exist)");
                        return;
                    }

                    Dictionary<string, int> imagesAndWeights = GetImages(path);

                    string imageFile = RandomImage(frm, area, imagesAndWeights, excludeList);

                    if (area.Exclusive == true)
                    {
                        excludeList.Add(imageFile);
                    }

                    area.File = imageFile;
                }
            }
        }

        private static Dictionary<Area, string> GetAreaFolders(
            string basePath, IEnumerable<Area> areas)
        {
            var areaFolders = new Dictionary<Area, string>();

            foreach (Area area in areas)
            {
                string folder = $"{area.Folder}";

                if (folder.Contains(':'))
                {
                    // Absolute path
                    areaFolders.Add(area, folder);
                    continue;
                }

                if (string.IsNullOrWhiteSpace(folder))
                {
                    folder = $"{area.Group}";
                }

                if (string.IsNullOrWhiteSpace(folder))
                {
                    folder = area.Name;
                }

                folder = Path.Combine(basePath, folder);
                areaFolders.Add(area, folder);
            }

            return areaFolders;
        }

        private static Dictionary<string, int> GetImages(string path)
        {
            var result = new Dictionary<string, int>();

            var searchOption = SearchOption.TopDirectoryOnly;

            foreach (string ext in IMAGE_EXTENSIONS)
            {
                string[] imageFiles = Directory.GetFiles(
                    path, $"*.{ext}", searchOption);

                foreach (string filePath in imageFiles)
                {
                    string fileName = Path.GetFileName(filePath);

                    int weight = GetWeight(fileName);

                    result.Add(filePath, weight);
                }
            }

            return result;
        }

        private static int GetWeight(string fileName)
        {
            if (!fileName.StartsWith("__"))
            {
                return DEFAULT_WEIGHT;
            }

            int start = "__".Length;
            int end = fileName.IndexOf("__", 2);

            if (end <= start)
            {
                return DEFAULT_WEIGHT;
            }

            int length = (end - start);

            string weight = fileName.Substring(start, length);

            if (double.TryParse(weight, NumberStyles.Float,
                CultureInfo.InvariantCulture, out double parsed))
            {
                if (parsed > 0)
                {
                    return (int)(parsed * DEFAULT_WEIGHT * 2);
                }
            }

            Logging.EnsureEmptyLine();
            Logging.LogLine($"Could not parse weight ({weight}) of file '{fileName}'");

            return DEFAULT_WEIGHT;
        }

        private static string RandomImage(
            BoardGeneratorFrm frm,
            Area area,
            Dictionary<string, int> imagesAndWeights,
            IEnumerable<string> excludeList
            )
        {
            string result = null;

            if (excludeList?.Any() == true)
            {
                imagesAndWeights = imagesAndWeights
                    .ToDictionary(e => e.Key, e => e.Value);

                foreach (string exclude in excludeList)
                {
                    bool removed = imagesAndWeights.Remove(exclude);
                }

                if (!imagesAndWeights.Any())
                {
                    Logging.Log($"Image list exhausted for area {area.Name}");
                    frm.SetError($"Could not get an exclusive image for area {area.Name}");
                    return null;
                }
            }

            var values = imagesAndWeights.Values;

            int total = values.Sum();

            if (values.Any(v => v != DEFAULT_WEIGHT))
            {
                Logging.EnsureEmptyLine();
                foreach (var kv in imagesAndWeights)
                {
                    string file = kv.Key;
                    int weight = kv.Value;

                    var percentage = Math.Round(((float)weight / total * 100), 2);

                    Logging.Log($"File: {file} weight: {weight} ({percentage}%)");
                }
                Logging.Log();
            }

            int val = random.Next(total);
            double runningTotal = 0;

            foreach (var kv in imagesAndWeights)
            {
                result = kv.Key;
                runningTotal += kv.Value;

                if (val < runningTotal)
                {
                    break;
                }
            }

            return result;
        }
    }
}
