using System.IO;
using System.Xml.Serialization;
using Tundra.Interfaces.Serializer;
using Tundra.Writers;

namespace Tundra.Wrapper
{
    /// <summary>
    /// Xml Serializer Wrapper Class
    /// </summary>
    public class XmlSerializerWrapper : ISerializer
    {
        /// <summary>
        /// Deserializes an object.
        /// </summary>
        /// <typeparam name="T">The type of object to de-serialize.</typeparam>
        /// <param name="data">The string data to use for deserialization.</param>
        /// <returns>
        /// Returns the deserialized object.
        /// </returns>
        public T Deserialize<T>(string data)
             where T : class
        {
            using (Stream stream = new MemoryStream())
            {
                var bytes = System.Text.Encoding.UTF8.GetBytes(data);
                stream.Write(bytes, 0, bytes.Length);
                stream.Position = 0;
                var deserializer = new XmlSerializer(typeof(T));
                return deserializer.Deserialize(stream) as T;
            }
        }

        /// <summary>
        /// Serializes an object to a string.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="instance">The object to serialize.</param>
        /// <returns>
        /// Returns the object serialized as a string.
        /// </returns>
        public string Serialize<T>(T instance)
        {
            using (StringWriter textWriter = new Utf8StringWriter())
            {
                var serializer = new XmlSerializer(typeof(T));
                serializer.Serialize(textWriter, instance);
                return textWriter.ToString();
            }
        }
    }
}