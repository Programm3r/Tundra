using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Tundra.Enum;
using Tundra.Helper;

namespace Tundra.Extension
{
    /// <summary>
    /// HTTP Response Extension Method Class
    /// </summary>
    public static class HttpResponseExtensions
    {
        /// <summary>
        /// Gets the string response asynchronous.
        /// </summary>
        /// <param name="httpResponseMessage">The HTTP response message.</param>
        /// <returns></returns>
        public static async Task<string> GetStringResponseAsync(this HttpResponseMessage httpResponseMessage)
        {
            return await httpResponseMessage.Content.ReadAsStringAsync();
        }

        /// <summary>
        /// Gets the string response asynchronous.
        /// </summary>
        /// <param name="httpResponseMessage">The HTTP response message.</param>
        /// <returns></returns>
        public static async Task<string> GetStringResponseAsync(this Task<HttpResponseMessage> httpResponseMessage)
        {
            var responseMessage = await httpResponseMessage;
            return await responseMessage.GetStringResponseAsync();
        }

        /// <summary>
        /// Processes the response.
        /// </summary>
        /// <typeparam name="TResult">The type of the result.</typeparam>
        /// <param name="responseMessage">The response message.</param>
        /// <param name="outputFormat">The output format.</param>
        /// <returns></returns>
        public static async Task<TResult> ProcessResponse<TResult>(this HttpResponseMessage responseMessage, SerializerFormat outputFormat)
            where TResult : class
        {
            if (responseMessage == null)
            {
                return default(TResult);
            }

            var result = await responseMessage.Content.ReadAsStringAsync();
            if (responseMessage.IsSuccessStatusCode && responseMessage.StatusCode == HttpStatusCode.OK && string.IsNullOrEmpty(result))
            {
                return default(TResult);
            }
            return SerializerHelper.DeserializeData<TResult>(outputFormat, result);
        }

        /// <summary>
        /// Processes the response.
        /// </summary>
        /// <typeparam name="TResult">The type of the result.</typeparam>
        /// <param name="responseMessage">The response message.</param>
        /// <param name="outputFormat">The output format.</param>
        /// <returns></returns>
        public static async Task<TResult> ProcessResponse<TResult>(this Task<HttpResponseMessage> responseMessage, SerializerFormat outputFormat) 
            where TResult : class
        {
            var response = await responseMessage;
            return await response.ProcessResponse<TResult>(outputFormat);
        }
    }
}
