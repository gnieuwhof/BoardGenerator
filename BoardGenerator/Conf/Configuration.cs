namespace BoardGenerator.Conf
{
    public class Configuration
    {
        public string BasePath { get; set; }

        public int? CacheSize { get; set; }

        public Area[] Areas { get; set; }
    }
}
