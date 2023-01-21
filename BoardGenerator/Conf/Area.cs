using Newtonsoft.Json;

namespace BoardGenerator.Conf
{
    public class Area
    {
        public string Name { get; set; }


        public int X { get; set; }

        public int Y { get; set; }

        public int Z { get; set; }


        public int Width { get; set; }

        public int Height { get; set; }


        public string Folder { get; set; }

        private string file;
        public string File
        {
            get => this.file;
            set
            {
                this.file = value;
                if (this.imageFile != value)
                {
                    this.imageFile = null;
                    this.img = null;
                }
            }
        }


        public string Group { get; set; }

        public bool? Exclusive { get; set; }


        public bool? Locked { get; set; }

        // ====

        private string imageFile;
        private Image img;

        [JsonIgnore]
        public Image Img => this.img;

        public void SetImage(string imageFile, Image img)
        {
            this.imageFile = imageFile;
            this.img = img;
        }
    }
}
