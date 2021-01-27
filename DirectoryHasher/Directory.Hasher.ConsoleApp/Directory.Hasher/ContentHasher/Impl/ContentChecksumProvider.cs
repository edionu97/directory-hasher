using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Directory.Hasher.Utility.ExtensionMethods;

namespace Directory.Hasher.Checksum.ContentHasher.Impl
{
    public class ChecksumContentProvider : IContentHasher
    {
        public Task<string> GetChecksumOfFileContentAsync(FileInfo fileInfo)
        {
            //check if the file exists
            if (!fileInfo.Exists)
            {
                throw new Exception($"The file {fileInfo.Name} does not exist");
            }

            //read the file by chunks (1MB size)
            using var bufferedStream =
                new BufferedStream(fileInfo.OpenRead(), 1 * 1024 * 1024);

            //return the computed checksum
            return ComputeChecksumAsync(bufferedStream);
        }

        public Task<string> GetChecksumOfStringContentAsync(string @string)
        {
            //check if the string is eligible
            if (string.IsNullOrEmpty(@string))
            {
                throw new Exception("The string must not be null or empty");
            }

            //get the checksum of the string
            return ComputeChecksumAsync(@string.AsStream());
        }

        /// <summary>
        /// Helper method for computing the sha256 checksum over a stream of data
        /// </summary>
        /// <param name="stream">the stream containing the data</param>
        /// <returns>the checksum</returns>
        private static Task<string> ComputeChecksumAsync(Stream stream)
        {
            //create the instance of the sha algorithm
            using var sha256 = new SHA256Managed();

            //compute the checksum and return it
            var checksumBuilder = new StringBuilder();
            foreach (var @byte in sha256.ComputeHash(stream))
            {
                checksumBuilder.Append(@byte.ToString("x2"));
            }
            return Task.FromResult(checksumBuilder.ToString());
        }
    }
}
