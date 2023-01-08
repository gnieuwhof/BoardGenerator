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

            sb.AppendLine(message);

            if (logFrm != null)
            {
                logFrm.Update(sb.ToString());
            }
        }

        public static void Empty()
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
