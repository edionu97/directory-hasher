using System.IO;
using System.Threading.Tasks;
using Directory.Hasher.Utility.Hashing;

namespace Directory.Hasher.Checksum.DirectoryHasher.Hasher
{
    public interface IDirectoryHasher
    {
        /// <summary>
        /// This method it is used to get the hash of a directory
        /// </summary>
        /// <param name="baseDirectory">the base directory</param>
        /// <returns>returns the generic information about the base directory</returns>
        public Task<GenericItem> GetDirectoryHashAsync(DirectoryInfo baseDirectory);
    }
}
