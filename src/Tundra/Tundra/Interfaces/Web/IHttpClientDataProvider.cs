using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Tundra.Enum;

namespace Tundra.Interfaces.Web
{
    /// <summary>
    /// Http Client Data Provider Interface
    /// </summary>
    public interface IHttpClientDataProvider
    {
        /// <summary>
        /// Gets or sets the base address of Uniform Resource Identifier (URI) of the Internet resource used when sending requests.
        /// </summary>
        /// <value>
        /// The base address.
        /// </value>
        Uri BaseAddress { get; set; }

        /// <summary>
        /// Downloads a file asynchronous from a given requestUri path and reports progress.
        /// </summary>
        /// <param name="requestUri">The request URI.</param>
        /// <param name="progress">The progress.</param>
        /// <param name="token">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <returns>
        /// returns a collection that contains bytes.
        /// </returns>
        Task<IList<byte>> DownloadFileAsync(string requestUri, IProgress<double> progress, CancellationToken token);

        /// <summary>
        /// Sends an HTTP request as an asynchronous operation.
        /// </summary>
        /// <typeparam name="TResult">The type of the result.</typeparam>
        /// <param name="requestUri">The request URI.</param>
        /// <param name="method">The HTTP method.</param>
        /// <param name="outputFormat">The output format.</param>
        /// <param name="token">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <returns></returns>
        Task<TResult> SendAsync<TResult>(string requestUri, HttpMethod method, SerializerFormat outputFormat, CancellationToken? token = null) where TResult : class;

        /// <summary>
        /// Sends an HTTP request as an asynchronous operation.
        /// </summary>
        /// <typeparam name="TResult">The type of the result.</typeparam>
        /// <param name="requestUri">The request URI.</param>
        /// <param name="method">The HTTP method.</param>
        /// <param name="outputFormat">The output format.</param>
        /// <param name="requestHeaders">The request headers.</param>
        /// <param name="token">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <returns></returns>
        Task<TResult> SendAsync<TResult>(string requestUri, HttpMethod method, SerializerFormat outputFormat, IDictionary<string, string> requestHeaders, CancellationToken? token = null) where TResult : class;

        /// <summary>
        /// Sends an HTTP request as an asynchronous operation.
        /// </summary>
        /// <typeparam name="TInput">The type of the input.</typeparam>
        /// <typeparam name="TResult">The type of the result.</typeparam>
        /// <param name="requestUri">The request URI.</param>
        /// <param name="method">The HTTP method.</param>
        /// <param name="inputObject">The input object.</param>
        /// <param name="contentType">Type of the content.</param>
        /// <param name="serializerFormat">The serializer format.</param>
        /// <param name="token">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <returns></returns>
        Task<TResult> SendAsync<TInput, TResult>(string requestUri, HttpMethod method, TInput inputObject, HttpContentType contentType, SerializerFormat serializerFormat, CancellationToken? token = null) where TInput : class where TResult : class;

        /// <summary>
        /// Sends an HTTP request as an asynchronous operation.
        /// </summary>
        /// <typeparam name="TInput">The type of the input.</typeparam>
        /// <typeparam name="TResult">The type of the result.</typeparam>
        /// <param name="requestUri">The request URI.</param>
        /// <param name="method">The HTTP method.</param>
        /// <param name="inputObject">The input object.</param>
        /// <param name="contentType">Type of the content.</param>
        /// <param name="serializerFormat">The serializer format.</param>
        /// <param name="requestHeaders">The request headers.</param>
        /// <param name="token">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <returns></returns>
        Task<TResult> SendAsync<TInput, TResult>(string requestUri, HttpMethod method, TInput inputObject, HttpContentType contentType, SerializerFormat serializerFormat, IDictionary<string, string> requestHeaders, CancellationToken? token = null) where TInput : class where TResult : class;

        /// <summary>
        /// Sends an HTTP request as an asynchronous operation.
        /// </summary>
        /// <param name="requestUri">The request URI.</param>
        /// <param name="method">The HTTP method.</param>
        /// <param name="token">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <returns>
        /// The task object representing the asynchronous operation.
        /// </returns>
        Task<HttpResponseMessage> SendAsync(string requestUri, HttpMethod method, CancellationToken? token = null);

        /// <summary>
        /// Sends an HTTP request as an asynchronous operation.
        /// </summary>
        /// <param name="requestUri">The request URI.</param>
        /// <param name="method">The HTTP method.</param>
        /// <param name="requestHeaders">The request headers.</param>
        /// <param name="token">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <returns>
        /// The task object representing the asynchronous operation.
        /// </returns>
        Task<HttpResponseMessage> SendAsync(string requestUri, HttpMethod method, IDictionary<string, string> requestHeaders, CancellationToken? token = null);

        /// <summary>
        /// Sends an HTTP request as an asynchronous operation.
        /// </summary>
        /// <typeparam name="TInput">The type of the input.</typeparam>
        /// <param name="requestUri">The request URI.</param>
        /// <param name="method">The HTTP method.</param>
        /// <param name="inputObject">The input object.</param>
        /// <param name="contentType">Type of the content.</param>
        /// <param name="serializerFormat">The serializer format.</param>
        /// <param name="token">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <returns>
        /// The task object representing the asynchronous operation.
        /// </returns>
        Task<HttpResponseMessage> SendAsync<TInput>(string requestUri, HttpMethod method, TInput inputObject, HttpContentType contentType, SerializerFormat serializerFormat, CancellationToken? token = null) where TInput : class;

        /// <summary>
        /// Sends an HTTP request as an asynchronous operation.
        /// </summary>
        /// <typeparam name="TInput">The type of the input.</typeparam>
        /// <param name="requestUri">The request URI.</param>
        /// <param name="method">The HTTP method.</param>
        /// <param name="inputObject">The input object.</param>
        /// <param name="contentType">Type of the content.</param>
        /// <param name="serializerFormat">The serializer format.</param>
        /// <param name="requestHeaders">The request headers.</param>
        /// <param name="token">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <returns>
        /// The task object representing the asynchronous operation.
        /// </returns>
        Task<HttpResponseMessage> SendAsync<TInput>(string requestUri, HttpMethod method, TInput inputObject, HttpContentType contentType, SerializerFormat serializerFormat, IDictionary<string, string> requestHeaders, CancellationToken? token = null) where TInput : class;
    }
}