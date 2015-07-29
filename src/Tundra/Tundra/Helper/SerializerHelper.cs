using System;
using System.IO;
using System.Xml.Serialization;
using Newtonsoft.Json;
using Tundra.Enum;
using Tundra.Writers;

namespace Tundra.Helper
{
    /// <summary>
    /// Serializer Helper Class
    /// </summary>
    public static class SerializerHelper
    {
        #region SerializerHelper - Private Static Methods

        /// <summary>
        /// Deserializes the data.
        /// </summary>
        /// <typeparam name="TResult">The type of the result.</typeparam>
        /// <param name="format">The format.</param>
        /// <param name="xml">The XML.</param>
        /// <returns>
        /// a deserialized instance of <typeparam name="TResult" />
        /// </returns>
        /// <exception cref="ArgumentNullException">xml</exception>
        public static TResult DeserializeData<TResult>(SerializerFormat format, string xml) where TResult : class
        {
            if (xml == null) throw new ArgumentNullException("xml");

            switch (format)
            {
                default:
                case SerializerFormat.JSON:
                    return JsonConvert.DeserializeObject<TResult>(xml);
                case SerializerFormat.XML:
                    using (Stream stream = new MemoryStream())
                    {
                        var data = System.Text.Encoding.UTF8.GetBytes(xml);
                        stream.Write(data, 0, data.Length);
                        stream.Position = 0;
                        var deserializer = new XmlSerializer(typeof (TResult));
                        return deserializer.Deserialize(stream) as TResult;
                    }
            }
        }

        /// <summary>
        /// Serializes the object.
        /// </summary>
        /// <typeparam name="TInput">The type of the input.</typeparam>
        /// <param name="inputObject">The input object.</param>
        /// <param name="format">The format.</param>
        /// <returns>
        /// a serialized string
        /// </returns>
        /// <exception cref="ArgumentNullException">inputObject</exception>
        public static string SerializeObject<TInput>(TInput inputObject, SerializerFormat format) where TInput : class
        {
            if (inputObject == null) throw new ArgumentNullException("inputObject");

            switch (format)
            {
                default:
                case SerializerFormat.JSON:
                    return JsonConvert.SerializeObject(inputObject);
                case SerializerFormat.XML:
                    using (StringWriter textWriter = new Utf8StringWriter())
                    {
                        var serializer = new XmlSerializer(typeof (TInput));
                        serializer.Serialize(textWriter, inputObject);
                        return textWriter.ToString();
                    }
                case SerializerFormat.None:
                    return inputObject.ToString();
            }
        }

        #endregion
    }
}