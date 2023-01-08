namespace BoardGenerator
{
    using System.Text;

    internal static class Logging
    {
        private static readonly StringBuilder sb = new StringBuilder();


        public static void Log(string message)
        {
            sb.AppendLine(message);
        }
    }
}
