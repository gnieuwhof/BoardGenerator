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
            catch(IOException)
            {
                Logging.Log("X ");
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
    }
}
