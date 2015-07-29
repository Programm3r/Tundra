using System;

namespace Tundra.Interfaces.Serializer
{
    /// <summary>
    /// An interface for serializing and desterilizing objects.
    /// </summary>
    public interface ISerializer
    {
        /// <summary>
        /// Deserializes an object.
        /// </summary>
        /// <typeparam name="T">The type of object to de-serialize.</typeparam>
        /// <param name="data">The string data to use for deserialization.</param>
        /// <returns>Returns the deserialized object.</returns>
        T Deserialize<T>(string data) where T : class;

        /// <summary>
        /// Serializes an object to a string.
        /// </summary>
        /// <param name="instance">The object to serialize.</param>
        /// <returns>Returns the object serialized as a string.</returns>
        string Serialize<T>(T instance);
    }
}