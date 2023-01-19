namespace BoardGenerator
{
    using System.Text;

    internal static class FileHelper
    {
        public static bool IsFileInUse(string filePath)
        {
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

            Logging.LogWithEmptyLine(
                $"Read {fileContent.Length} characters from file stream");

            return fileContent;
        }

        public static void WriteToStream(
            FileStream fileStream, string content)
        {
            _ = fileStream ??
                throw new ArgumentNullException(nameof(fileStream));

            _ = content ??
                throw new ArgumentNullException(nameof(content));


            byte[] bytes = Encoding.UTF8.GetBytes(content);

            fileStream.Write(bytes, 0, bytes.Length);

            Logging.LogWithEmptyLine(
                $"Written {content.Length} characters to file stream");
        }

        public static string GetBasePath(string basePath)
        {
            if (basePath == null)
            {
                basePath = Path.GetDirectoryName(Application.ExecutablePath);
            }

            return basePath;
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

    }
}
