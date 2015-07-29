using Newtonsoft.Json;
using Tundra.Interfaces.Serializer;

namespace Tundra.Wrapper
{
    /// <summary>
    /// Json Serializer Wrapper Class
    /// </summary>
    public class JsonSerializerWrapper : ISerializer
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
            return JsonConvert.DeserializeObject<T>(data);
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
            return JsonConvert.SerializeObject(instance);
        }
    }
}