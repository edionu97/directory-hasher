using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Directory.Hasher.Checksum.BasicByteBuffer;
using Directory.Hasher.Utility.Hashing;

namespace Directory.Hasher.Checksum.DirectoryHasher.Hasher.Impl
{
    public class DirectoryHasher : IDirectoryHasher
    {
        private readonly IBasicByteContentHasher _basicByteContentHasher;

        public DirectoryHasher(IBasicByteContentHasher basicByteContentHasher)
        {
            _basicByteContentHasher = basicByteContentHasher;
        }

        public async Task<GenericItem> GetDirectoryHashAsync(DirectoryInfo baseDirectory)
        {
            //this method will always return one single result
            await foreach (var item in GetItems(baseDirectory))
            {
                return item;
            }

            return null;
        }

        private async IAsyncEnumerable<GenericItem> GetItems(DirectoryInfo fromDirectory)
        {
            //if the directory is null or does not exist then an error will be thrown
            if (fromDirectory?.Exists != true)
            {
                throw new Exception($"The directory {fromDirectory?.Name} does not exist");
            }

            //iterate the directory file
            var fileItems = new List<GenericItem>();
            foreach (var fileInfo in fromDirectory.GetFiles())
            {
                fileItems.Add(new GenericItem
                {
                    IsDirectory = false,
                    Name = fileInfo.Name,
                    Hash = await _basicByteContentHasher.GetChecksumOfFileContentAsync(fileInfo)
                });
            }

            //get the folder tasks
            var folderTasks = fromDirectory.GetDirectories()
                .Select(directoryInfo => Task.Run<IList<GenericItem>>(
                    async () =>
                    {
                        var items = new List<GenericItem>();

                        //iterate through the result of other directory
                        await foreach (var item in GetItems(directoryInfo))
                        {
                            items.Add(item);
                        }

                        //return the items
                        return items;
                    }))
                .ToList();

            //add other information into the list
            foreach (var folderTask in folderTasks)
            {
                fileItems.AddRange(await folderTask);
            }

            //aggregate the content
            var content = fileItems.Aggregate(
                string.Empty,
                (s, item) => s + $"({fromDirectory.Name}->{item.Name}) with checksum: {item.Hash}\n");

            //return the items
            yield return new GenericItem
            {
                IsDirectory = true,
                Name = fromDirectory.Name,
                Items = fileItems,
                Hash = await _basicByteContentHasher.GetChecksumOfStringContentAsync(content)
            }; ;
        }
    }
}
