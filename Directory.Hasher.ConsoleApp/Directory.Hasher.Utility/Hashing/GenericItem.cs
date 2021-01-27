using System.Collections.Generic;

namespace Directory.Hasher.Utility.Hashing
{
    public class GenericItem
    {
        public string Name { get; set; }
        public string Hash { get; set; }
        public bool IsDirectory { get; set; }
        public IEnumerable<GenericItem> Items { get; set; }
    }
}
