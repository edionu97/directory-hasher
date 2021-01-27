using System.IO;

namespace Directory.Hasher.Utility.ExtensionMethods
{
    public static class PrimitivesExtensions
    {
        /// <summary>
        /// This function creates a stream from a string
        /// The stream must be closed by the caller of the method
        /// </summary>
        /// <param name="string">the string that will be converted</param>
        /// <returns></returns>
        public static Stream AsStream(this string @string)
        {
            //create a memory stream
            var memoryStream = new MemoryStream();
            
            //create a stream writer
            var streamWriter = new StreamWriter(memoryStream);
            streamWriter.Write(@string);
            streamWriter.Flush();

            //return the stream
            memoryStream.Position = 0;
            return memoryStream;
        }
    }
}
