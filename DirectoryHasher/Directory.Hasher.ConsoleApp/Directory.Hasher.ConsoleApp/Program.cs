using System;
using System.Globalization;
using System.IO;
using System.Threading.Tasks;
using Directory.Hasher.Checksum.ContentHasher.Impl;

namespace Directory.Hasher.ConsoleApp
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var hasher = new ChecksumContentProvider();

            //get the hash value
            var hash = 
                await hasher
                    .GetChecksumOfFileContentAsync(
                        new FileInfo(@"C:\Users\Eduard\Desktop\RDSOS-merged.pdf"));

        }
    }
}
