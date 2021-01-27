using System.Collections.Generic;

namespace Directory.Hasher.Checksum.Item
{
    public class GenericItem
    {
        public string Name { get; set; }
        public string Hash { get; set; }
        public bool IsDirectory { get; set; }
        public IEnumerable<GenericItem> Items { get; set; }
    }
}
