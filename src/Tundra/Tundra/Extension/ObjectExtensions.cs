using Newtonsoft.Json;

namespace Tundra.Extension
{
    /// <summary>
    /// Object Extension Class
    /// </summary>
    public static class ObjectExtensions
    {
        /// <summary>
        /// To the json string.
        /// </summary>
        /// <param name="inputObject">The input object.</param>
        /// <returns></returns>
        public static string ToJsonString(this object inputObject)
        {
            return JsonConvert.SerializeObject(inputObject);
        }
    }
}