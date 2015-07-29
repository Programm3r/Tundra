using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Tundra.Enum;
using Tundra.Helper;

namespace Tundra.Extension
{
    /// <summary>
    /// Http Client Extension Class
    /// </summary>
    public static class HttpClientExtensions
    {
        /// <summary>
        /// Sets the content type header.
        /// </summary>
        /// <param name="httpClient">The HTTP client.</param>
        /// <param name="serializerFormat">The serializer format.</param>
        /// <returns>
        /// an <see cref="System.Net.Http.HttpClient" /> instance
        /// </returns>
        public static HttpClient SetContentTypeHeader(this HttpClient httpClient, SerializerFormat serializerFormat)
        {
            switch (serializerFormat)
            {
                case SerializerFormat.JSON:
                    if (!httpClient.DefaultRequestHeaders.Accept.Any(a => a.MediaType.Equals(ConvertHelper.JsonContentType)))
                    {
                        httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(ConvertHelper.JsonContentType));
                    }
                    break;
                case SerializerFormat.XML:
                    if (!httpClient.DefaultRequestHeaders.Accept.Any(a => a.MediaType.Equals(ConvertHelper.XmlContentType)))
                    {
                        httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(ConvertHelper.XmlContentType));
                    }
                    break;
            }

            return httpClient;
        }

        /// <summary>
        /// Sets the request payload.
        /// </summary>
        /// <typeparam name="TInput">The type of the input.</typeparam>
        /// <param name="httpClient">The HTTP client.</param>
        /// <param name="requestUri">The request URI.</param>
        /// <param name="method">The method.</param>
        /// <param name="inputObject">The input object.</param>
        /// <param name="contentType">Type of the content.</param>
        /// <param name="serializerFormat">The serializer format.</param>
        /// <returns></returns>
        /// <exception cref="ArgumentException">In order to use a FormUrlEncoded content type, the input object must be of type: IEnumerable{KeyValuePair{string, string}};inputObject</exception>
        /// <exception cref="ArgumentOutOfRangeException">contentType</exception>
        public static HttpRequestMessage SetRequestPayload<TInput>(this HttpClient httpClient, string requestUri, HttpMethod method, TInput inputObject, HttpContentType contentType, SerializerFormat serializerFormat) where TInput : class
        {
            HttpContent httpContent;

            switch (contentType)
            {
                case HttpContentType.FormUrlEncoded:
                    var nameValueCollection = inputObject as IEnumerable<KeyValuePair<string, string>>;
                    if (nameValueCollection == null)
                    {
                        throw new ArgumentException("In order to use a FormUrlEncoded content type, the input object must be of type: IEnumerable<KeyValuePair<string, string>>", "inputObject");
                    }
                    httpContent = new FormUrlEncodedContent(nameValueCollection);
                    break;
                case HttpContentType.String:
                    httpContent = new StringContent(SerializerHelper.SerializeObject(inputObject, serializerFormat), Encoding.UTF8, ConvertHelper.ToMediaType(serializerFormat));
                    break;
                default:
                    throw new ArgumentOutOfRangeException("contentType");
            }

            return new HttpRequestMessage(method, new Uri(requestUri))
            {
                Content = httpContent
            };
        }

        /// <summary>
        /// Sends an HTTP request as an asynchronous operation.
        /// </summary>
        /// <param name="httpClient">The HTTP client.</param>
        /// <param name="requestUri">The request URI.</param>
        /// <param name="method">The method.</param>
        /// <param name="token">The token.</param>
        /// <returns></returns>
        public static async Task<HttpResponseMessage> SendAsyncEx(this HttpClient httpClient, string requestUri, HttpMethod method, CancellationToken? token = null)
        {
            var httpRequestMessage = new HttpRequestMessage(new HttpMethod(method.ToString()), new Uri(requestUri));
            if (!token.HasValue)
            {
                return await httpClient.SendAsync(httpRequestMessage);
            }
            token.Value.Register(httpClient.CancelPendingRequests);
            return await httpClient.SendAsync(httpRequestMessage, token.Value);
        }

        /// <summary>
        /// Sends an HTTP request as an asynchronous operation.
        /// </summary>
        /// <typeparam name="TInput">The type of the input.</typeparam>
        /// <param name="httpClient">The HTTP client.</param>
        /// <param name="requestUri">The request URI.</param>
        /// <param name="method">The method.</param>
        /// <param name="inputObject">The input object.</param>
        /// <param name="contentType">Type of the content.</param>
        /// <param name="serializerFormat">The serializer format.</param>
        /// <param name="token">The token.</param>
        /// <returns></returns>
        public static async Task<HttpResponseMessage> SendAsyncEx<TInput>(this HttpClient httpClient, string requestUri, HttpMethod method, TInput inputObject, HttpContentType contentType, SerializerFormat serializerFormat, CancellationToken? token = null) where TInput : class
        {
            // get the payload body
            var payload = httpClient.SetContentTypeHeader(serializerFormat).SetRequestPayload(requestUri, method, inputObject, contentType, serializerFormat);
            if (!token.HasValue)
            {
                // send the request to the specified uri
                return await httpClient.SendAsync(payload);
            }
            // register the cancellation token
            token.Value.Register(httpClient.CancelPendingRequests);
            // send the request to the specified uri
            return await httpClient.SendAsync(payload, token.Value);
        }

        /// <summary>
        /// Sends an HTTP request as an asynchronous operation.
        /// </summary>
        /// <typeparam name="TResult">The type of the result.</typeparam>
        /// <param name="httpClient">The HTTP client.</param>
        /// <param name="requestUri">The request URI.</param>
        /// <param name="method">The method.</param>
        /// <param name="serializerFormat">The output format.</param>
        /// <param name="token">The token.</param>
        /// <returns></returns>
        public static async Task<TResult> SendAsyncEx<TResult>(this HttpClient httpClient, string requestUri, HttpMethod method, SerializerFormat serializerFormat, CancellationToken? token = null) where TResult : class
        {
            var httpRequestMessage = new HttpRequestMessage(new HttpMethod(method.ToString()), new Uri(requestUri));
            if (!token.HasValue)
            {
                return await httpClient.SendAsync(httpRequestMessage).ProcessResponse<TResult>(serializerFormat);
            }
            token.Value.Register(httpClient.CancelPendingRequests);
            return await httpClient.SendAsync(httpRequestMessage, token.Value).ProcessResponse<TResult>(serializerFormat);
        }

        /// <summary>
        /// Sends an HTTP request as an asynchronous operation.
        /// </summary>
        /// <param name="httpClient">The HTTP client.</param>
        /// <param name="requestUri">The request URI.</param>
        /// <param name="method">The method.</param>
        /// <param name="serializerFormat">The serializer format.</param>
        /// <param name="token">The token.</param>
        /// <returns></returns>
        public static async Task<HttpResponseMessage> SendAsyncEx(this HttpClient httpClient, string requestUri, HttpMethod method, SerializerFormat serializerFormat, CancellationToken? token = null)
        {
            var httpRequestMessage = new HttpRequestMessage(new HttpMethod(method.ToString()), new Uri(requestUri));
            if (!token.HasValue)
            {
                return await httpClient.SendAsync(httpRequestMessage);
            }
            token.Value.Register(httpClient.CancelPendingRequests);
            return await httpClient.SendAsync(httpRequestMessage, token.Value);
        }

        /// <summary>
        /// Sends an HTTP request as an asynchronous operation.
        /// </summary>
        /// <typeparam name="TResult">The type of the result.</typeparam>
        /// <typeparam name="TInput">The type of the input.</typeparam>
        /// <param name="httpClient">The HTTP client.</param>
        /// <param name="requestUri">The request URI.</param>
        /// <param name="method">The method.</param>
        /// <param name="inputObject">The input object.</param>
        /// <param name="contentType">Type of the content.</param>
        /// <param name="serializerFormat">The serializer format.</param>
        /// <param name="token">The token.</param>
        /// <returns></returns>
        public static async Task<TResult> SendAsyncEx<TInput, TResult>(this HttpClient httpClient, string requestUri, HttpMethod method, TInput inputObject, HttpContentType contentType, SerializerFormat serializerFormat, CancellationToken? token = null) where TInput : class where TResult : class
        {
            // get the payload body
            var payload = httpClient.SetContentTypeHeader(serializerFormat).SetRequestPayload(requestUri, method, inputObject, contentType, serializerFormat);
            if (!token.HasValue)
            {
                // send the request to the specified uri
                return await httpClient.SendAsync(payload).ProcessResponse<TResult>(serializerFormat);
            }
            // register the cancellation token
            token.Value.Register(httpClient.CancelPendingRequests);
            // send the request to the specified uri
            return await httpClient.SendAsync(payload, token.Value).ProcessResponse<TResult>(serializerFormat);
        }
    }
}