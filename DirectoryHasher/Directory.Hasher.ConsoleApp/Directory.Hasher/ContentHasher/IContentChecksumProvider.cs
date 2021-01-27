using System.IO;
using System.Threading.Tasks;

namespace Directory.Hasher.Checksum.ContentHasher
{
    public interface IContentHasher
    {
        /// <summary>
        /// This method computes for a file, the checksum of its content
        /// </summary>
        /// <param name="fileInfo">the file info</param>
        /// <returns>a string representing the checksum of the file</returns>
        public Task<string> GetChecksumOfFileContentAsync(FileInfo fileInfo);

        /// <summary>
        /// This method gets the checksum of a string
        /// </summary>
        /// <param name="string">the string that will be computed</param>
        /// <returns>the checksum</returns>
        public Task<string> GetChecksumOfStringContentAsync(string @string);
    }
}
