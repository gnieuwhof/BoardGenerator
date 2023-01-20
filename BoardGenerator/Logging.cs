namespace BoardGenerator
{
    using System.Text;

    internal static class Logging
    {
        private static readonly StringBuilder sb = new StringBuilder();

        private static LogFrm logFrm;
        private static string last;


        public static void Log(string message = "")
        {
            _ = message ??
                throw new ArgumentNullException(nameof(message));

            last = message;

            if (message != "")
            {
                string timestamp = DateTime.Now.ToString("HH:mm:ss");

                if (message.Contains("\n"))
                {
                    sb.AppendLine($"[{timestamp}]");
                }
                else
                {
                    message = $"[{timestamp}] {message}";
                }
            }

            sb.AppendLine(message);

            if (logFrm != null)
            {
                logFrm.Update(sb.ToString());
            }
        }

        public static void LogLine(string message = "")
        {
            Log(message);
            Log();
        }

        public static void EnsureEmptyLine()
        {
            if (last != "")
            {
                Log("");
            }
        }

        public static void SetLogFrm(LogFrm logFrm)
        {
            Logging.logFrm = logFrm ??
                throw new ArgumentNullException(nameof(logFrm));

            logFrm.Update(sb.ToString());
        }
    }
}
