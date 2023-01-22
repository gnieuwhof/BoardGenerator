namespace BoardGenerator
{
    internal static class FileHelper
    {
        public static bool IsFileInUse(string filePath)
        {
            _ = filePath ??
                throw new ArgumentNullException(nameof(filePath));

            var file = new FileInfo(filePath);

            try
            {
                using (FileStream stream = file.Open(
                    FileMode.Open,
                    FileAccess.Read,
                    FileShare.None)
                    )
                {
                    stream.Close();
                }
            }
            catch (IOException)
            {
                return true;
            }

            return false;
        }

        public static string ReadFromStream(Stream stream)
        {
            _ = stream ??
                throw new ArgumentNullException(nameof(stream));


            string fileContent;

            using (var reader = new StreamReader(stream))
            {
                fileContent = reader.ReadToEnd();
            }

            Logging.LogLine(
                $"Read {fileContent.Length} characters from file stream");

            return fileContent;
        }

        public static void SaveFile(string filePath, string content)
        {
            _ = filePath ??
                throw new ArgumentNullException(nameof(filePath));

            _ = content ??
                throw new ArgumentNullException(nameof(content));

            File.WriteAllText(filePath, content);
        }

        public static OpenFileDialog ShowOpenFileDialog(
            string title, string filter, bool restoreDirectory = true)
        {
            var openFileDialog = new OpenFileDialog();

            // e.g. "Select Configuration File"
            openFileDialog.Title = title;

            // e.g. "JSON Configuration (*.json)|*.json"
            openFileDialog.Filter = filter;

            openFileDialog.RestoreDirectory = restoreDirectory;

            openFileDialog.ShowDialog();

            return openFileDialog;
        }

        public static SaveFileDialog ShowSaveFileDialog(
            string title, string filter)
        {
            var saveFileDialog = new SaveFileDialog();

            // e.g. "Save Configuration File"
            saveFileDialog.Title = title;

            // e.g. "JSON Configuration|*.json"
            saveFileDialog.Filter = filter;

            saveFileDialog.ShowDialog();

            return saveFileDialog;
        }

        public static Image GetImage(ImageCache cache, string path)
        {
            _ = cache ??
                throw new ArgumentNullException(nameof(cache));

            _ = path ??
                throw new ArgumentNullException(nameof(path));

            Image result = cache.Get(path);

            if (result == null)
            {
                result = Image.FromFile(path);

                cache.AddOrUpdate(result, path);
            }

            return result;
        }
    }
}
