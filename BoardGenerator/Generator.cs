namespace BoardGenerator
{
    using BoardGenerator.Conf;

    internal static class Generator
    {
        private static readonly string[] IMAGE_EXTENSIONS =
            new[] { "png", "jpg", "jpeg", "bmp" };


        public static void Regenerate(BoardGeneratorFrm frm, Configuration config)
        {
            Logging.EnsureEmptyLine();
            Logging.Log("Regenerating board");

            string basePath = FileHelper.GetBasePath(config.BasePath);

            Logging.Log("Getting Area paths");
            Logging.Log($"Base path: {basePath}");

            if (!Directory.Exists(basePath))
            {
                Logging.Log($"The base folder {basePath} does not exists");
                frm.SetStatus("Regenerating board failed (base path does not exist)");
                return;
            }

            Dictionary<Area, string> areaPaths =
                GetAreaFolders(basePath, config.Areas);

            var grouped = config.Areas.GroupBy(a => a.Group);
            foreach (var group in grouped)
            {
                var ordered = group.OrderByDescending(g => g.Exclusive);
                var excludeList = new List<string>();
                foreach (Area area in ordered)
                {
                    string path = areaPaths[area];

                    if (area.Locked == true)
                    {
                        continue;
                    }

                    Logging.Log($"Area {area.Name} path: {path}");

                    if (!Directory.Exists(path))
                    {
                        Logging.Log($"Area folder {path} for area {area} does not exists");
                        frm.SetStatus("Regenerating board failed (area path does not exist)");
                        return;
                    }

                    Dictionary<string, int> imageAndRatio = GetImages(path);

                    string imageFile = RandomImage(frm, area, imageAndRatio, excludeList);

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

                    int ratio = GetRatio(fileName);

                    result.Add(filePath, ratio);
                }
            }

            return result;
        }

        private static int GetRatio(string fileName)
        {
            if (!fileName.StartsWith("__"))
            {
                return 1;
            }

            int start = "__".Length;
            int end = fileName.IndexOf("__", 2);

            if (end <= start)
            {
                return 1;
            }

            int length = (end - start);

            string ratio = fileName.Substring(start, length);

            if (int.TryParse(ratio, out int result))
            {
                if (result > 0)
                {
                    return result;
                }
            }

            return 1;
        }

        private static string RandomImage(
            BoardGeneratorFrm frm,
            Area area,
            Dictionary<string, int> imageAndRatio,
            IEnumerable<string> excludeList
            )
        {
            string result = null;

            if (excludeList?.Any() == true)
            {
                imageAndRatio = imageAndRatio
                    .ToDictionary(e => e.Key, e => e.Value);

                if (imageAndRatio.Count <= 1)
                {
                    Logging.Log($"Image list exhausted for area {area.Name}");
                    frm.SetStatus($"Could not get an exclusive image for area {area.Name}");
                    return null;
                }

                foreach (string exclude in excludeList)
                {
                    imageAndRatio.Remove(exclude);
                }
            }

            int total = imageAndRatio.Values.Sum();

            int hash = Guid.NewGuid().GetHashCode();

            var random = new Random(hash);

            int val = random.Next(total);

            int runningTotal = 0;

            foreach (var kv in imageAndRatio)
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
