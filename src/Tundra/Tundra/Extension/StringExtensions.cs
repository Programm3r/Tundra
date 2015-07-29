using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace Tundra.Extension
{
    /// <summary>
    /// String Extensions Class
    /// </summary>
    public static class StringExtensions
    {
        /// <summary>
        /// Splits the into parts.
        /// </summary>
        /// <param name="input">The input.</param>
        /// <param name="partLength">Length of the part.</param>
        /// <returns></returns>
        public static List<string> SplitIntoParts(this string input, int partLength)
        {
            var result = new List<string>();
            var partIndex = 0;
            var length = input.Length;
            while (length > 0)
            {
                var tempPartLength = length >= partLength ? partLength : length;
                var part = input.Substring(partIndex * partLength, tempPartLength);
                result.Add(part);
                partIndex++;
                length -= partLength;
            }
            return result;
        }
    }
}
