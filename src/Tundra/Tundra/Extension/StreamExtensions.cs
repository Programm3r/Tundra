using System;
using System.IO;

namespace Tundra.Extension
{
    /// <summary>
    /// Stream Extensions Class
    /// </summary>
    public static class StreamExtensions
    {
        /// <summary>
        /// To the byte array.
        /// </summary>
        /// <param name="stream">The stream.</param>
        /// <returns></returns>
        public static byte[] ToByteArray(this Stream stream)
        {
            stream.Position = 0;
            var buffer = new byte[stream.Length];
            for (var totalBytesCopied = 0; totalBytesCopied < stream.Length;)
            {
                totalBytesCopied += stream.Read(buffer, totalBytesCopied, Convert.ToInt32(stream.Length) - totalBytesCopied);
            }
            return buffer;
        }
    }
}
