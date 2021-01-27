using System;
using System.IO;
using System.Threading.Tasks;
using Directory.Hasher.Checksum.BasicByteBuffer.Impl;
using Directory.Hasher.Checksum.DirectoryHasher.Hasher.Impl;

namespace Directory.Hasher.ConsoleApp
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            const string path = @"";

            var hasher = new DirectoryHasher(new BasicByteBasicByteContentHasher());

            var basicGenericInfo =
                (await hasher.GetDirectoryHashAsync(new DirectoryInfo(path)));

            Console.WriteLine(basicGenericInfo.Hash);
        }
    }
}
