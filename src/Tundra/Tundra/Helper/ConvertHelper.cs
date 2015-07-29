using System;
using Tundra.Enum;

namespace Tundra.Helper
{
    /// <summary>
    /// Convert Helper Class
    /// </summary>
    public static class ConvertHelper
    {
        /// <summary>
        /// The json content type
        /// </summary>
        internal const string JsonContentType = "application/json";

        /// <summary>
        /// The XML content type
        /// </summary>
        internal const string XmlContentType = "application/xml";

        /// <summary>
        /// Converts the content type to a media type.
        /// </summary>
        /// <param name="serializerFormat">The serializer format.</param>
        /// <returns></returns>
        /// <exception cref="ArgumentOutOfRangeException">serializerFormat</exception>
        public static string ToMediaType(SerializerFormat serializerFormat)
        {
            switch (serializerFormat)
            {
                case SerializerFormat.JSON:
                    return JsonContentType;
                case SerializerFormat.XML:
                    return XmlContentType;
                default:
                    throw new ArgumentOutOfRangeException("serializerFormat");
            }
        }
    }
}