namespace BoardGenerator
{
    using System;
    using System.Collections.Generic;

    public class ImageCache
    {
        private readonly object padlock = new object();
        private readonly Dictionary<string, Image> itemCache =
            new Dictionary<string, Image>();
        private readonly Dictionary<string, DateTime> accessTimes =
            new Dictionary<string, DateTime>();


        public ImageCache(int? itemLimit)
        {
            if(itemLimit == null)
            {
                itemLimit = 50;
            }

            if (itemLimit < 1)
            {
                throw new ArgumentException(
                    "Item limit must be at least one.", nameof(itemLimit));
            }

            this.ItemLimit = itemLimit.Value;
        }


        public int ItemLimit { get; }


        public bool AddOrUpdate(Image image, string path)
        {
            if (image == null)
            {
                return false;
            }

            lock (this.padlock)
            {
                bool exists = this.itemCache.ContainsKey(path);

                if (!exists)
                {
                    if (this.itemCache.Count == this.ItemLimit)
                    {
                        var leastRecent = this.accessTimes.Keys
                            .OrderBy(x => x)
                            .FirstOrDefault();

                        this.RemoveIfExists(leastRecent);
                    }

                    this.accessTimes[path] = DateTime.MinValue;
                }

                this.itemCache[path] = image;

                return !exists;
            }
        }


        private Image RemoveIfExists(string path)
        {
            Image result = null;

            lock (this.padlock)
            {
                if (this.itemCache.TryGetValue(path, out result))
                {
                    this.itemCache.Remove(path);
                    this.accessTimes.Remove(path);
                }
            }

            return result;
        }

        public Image Get(string path)
        {
            Image result = null;

            lock (this.padlock)
            {
                if (this.itemCache.TryGetValue(path, out result))
                {
                    this.accessTimes[path] = DateTime.UtcNow;
                }
            }

            return result;
        }


        public int Count()
        {
            lock (this.padlock)
            {
                return this.itemCache.Count;
            }
        }

        public void Clear()
        {
            lock (this.padlock)
            {
                this.itemCache.Clear();
                this.accessTimes.Clear();
            }
        }
    }
}
