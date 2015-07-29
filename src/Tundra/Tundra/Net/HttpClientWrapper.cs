using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Tundra.Enum;
using Tundra.Extension;
using Tundra.Interfaces.Credentials;
using Tundra.Interfaces.Web;

namespace Tundra.Net
{
    /// <summary>
    /// Http Client Wrapper Class
    /// </summary>
    public class HttpClientWrapper : IHttpClientDataProvider
    {        
        #region HttpClientWrapper - Private Fields

        /// <summary>
        /// The cookie container
        /// </summary>
        private readonly CookieContainer _container;

        /// <summary>
        /// The HTTP client instance
        /// </summary>
        private readonly HttpClient _httpClient;

        #endregion

        #region HttpClientWrapper - Properties

        /// <summary>
        /// Gets or sets the base address of Uniform Resource Identifier (URI) of the Internet resource used when sending requests.
        /// </summary>
        /// <value>
        /// The base address.
        /// </value>
        public Uri BaseAddress
        {
            get
            {
                return this._httpClient.BaseAddress;
            }
            set
            {
                if (this._httpClient.BaseAddress == null)
                {
                    this._httpClient.BaseAddress = value;
                }
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether to enable caching.
        /// </summary>
        /// <value>
        ///   <c>true</c> if caching should be enabled; otherwise, <c>false</c>.
        /// </value>
        public bool EnableCaching { get; private set; }

        #endregion

        #region HttpClientWrapper - CTOR

        /// <summary>
        /// Initializes a new instance of the <see cref="HttpClientWrapper"/> class.
        /// </summary>
        public HttpClientWrapper()
        {
            this._container = new CookieContainer();
            this._httpClient = new HttpClient(new HttpClientHandler()
            {
                CookieContainer = this._container
            });
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="HttpClientWrapper" /> class.
        /// </summary>
        /// <param name="enableCaching">if set to <c>true</c> caching will be enabled; otherwise the default value will be used: <c>false</c>.</param>
        public HttpClientWrapper(bool enableCaching = false)
            : this()
        {
            this.EnableCaching = enableCaching;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="HttpClientWrapper" /> class.
        /// </summary>
        /// <param name="credential">The credential used to connect with the HTTPClient.</param>
        /// <param name="enableCaching">if set to <c>true</c> caching will be enabled; otherwise the default value will be used: <c>false</c>.</param>
        public HttpClientWrapper(INetCredential credential, bool enableCaching = false)
        {
            this.EnableCaching = enableCaching;
            this._container = new CookieContainer();

            this._httpClient = new HttpClient(new HttpClientHandler
            {
                Credentials = new NetworkCredential(credential.UserName, credential.Password, credential.Domain), CookieContainer = this._container
            });
        }

        #endregion

        #region HttpClientWrapper - Protected Methods

        /// <summary>
        /// Sets the headers on the http client.
        /// </summary>
        /// <param name="headersDictionary">The headers dictionary.</param>
        /// <exception cref="ArgumentNullException">headersDictionary</exception>
        protected void SetHeader(IDictionary<string, string> headersDictionary)
        {
            if (headersDictionary == null) throw new ArgumentNullException("headersDictionary");

            this._httpClient.DefaultRequestHeaders.Clear();
            foreach (var item in headersDictionary)
            {
                this._httpClient.DefaultRequestHeaders.Add(item.Key, item.Value);
            }
        }

        #endregion HttpClientWrapper - Protected Methods        

        #region IHttpClientDataProvider Members

        /// <summary>
        /// Downloads a file asynchronous from a given requestUri path and reports progress.
        /// </summary>
        /// <param name="requestUri">The request URI.</param>
        /// <param name="progress">The progress.</param>
        /// <param name="token">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <returns>
        /// returns a collection that contains bytes.
        /// </returns>
        /// <exception cref="Exception"></exception>
        public async Task<IList<byte>> DownloadFileAsync(string requestUri, IProgress<double> progress, CancellationToken token)
        {
            var response = await this._httpClient.GetAsync(requestUri, HttpCompletionOption.ResponseHeadersRead, token);

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception(string.Format("The request returned with HTTP status code {0}", response.StatusCode));
            }

            var total = response.Content.Headers.ContentLength ?? -1L;
            var canReportProgress = total != -1 && progress != null;

            var fileBytes = new List<byte>();

            using (var stream = await response.Content.ReadAsStreamAsync())
            {
                var totalRead = 0L;
                var buffer = new byte[4096];
                var isMoreToRead = true;

                do
                {
                    token.ThrowIfCancellationRequested();

                    var read = await stream.ReadAsync(buffer, 0, buffer.Length, token);

                    if (read == 0)
                    {
                        isMoreToRead = false;
                    }
                    else
                    {
                        var data = new byte[read];
                        buffer.ToList().CopyTo(0, data, 0, read);

                        // write the file to local cached variable
                        fileBytes.AddRange(buffer);

                        totalRead += read;

                        if (canReportProgress)
                        {
                            progress.Report((totalRead*1d)/(total*1d)*100);
                        }
                    }
                } while (isMoreToRead);
            }

            return fileBytes;
        }

        /// <summary>
        /// Sends an HTTP request as an asynchronous operation.
        /// </summary>
        /// <typeparam name="TResult">The type of the result.</typeparam>
        /// <param name="requestUri">The request URI.</param>
        /// <param name="method">The HTTP method.</param>
        /// <param name="outputFormat">The output format.</param>
        /// <param name="token">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <returns></returns>
        public async Task<TResult> SendAsync<TResult>(string requestUri, HttpMethod method, SerializerFormat outputFormat, CancellationToken? token = null) 
            where TResult : class
        {
            // send the HTTP request
            return await this._httpClient.SendAsyncEx<TResult>(requestUri, method, outputFormat, token);
        }

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
        public async Task<TResult> SendAsync<TResult>(string requestUri, HttpMethod method, SerializerFormat outputFormat, IDictionary<string, string> requestHeaders, CancellationToken? token = null)
            where TResult : class
        {
            // first set the headers on the request
            this.SetHeader(requestHeaders);
            // send the HTTP request
            return await this._httpClient.SendAsyncEx<TResult>(requestUri, method, outputFormat, token);
        }

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
        public async Task<TResult> SendAsync<TInput, TResult>(string requestUri, HttpMethod method, TInput inputObject, HttpContentType contentType, SerializerFormat serializerFormat, CancellationToken? token = null) 
            where TResult : class 
            where TInput : class
        {
            // send the HTTP request
            return await this._httpClient.SendAsyncEx<TInput, TResult>(requestUri, method, inputObject, contentType, serializerFormat, token);
        }

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
        public async Task<TResult> SendAsync<TInput, TResult>(string requestUri, HttpMethod method, TInput inputObject, HttpContentType contentType, SerializerFormat serializerFormat, IDictionary<string, string> requestHeaders, CancellationToken? token = null)
            where TResult : class
            where TInput : class
        {
            // first set the headers on the request
            this.SetHeader(requestHeaders);
            // send the HTTP request
            return await this._httpClient.SendAsyncEx<TInput, TResult>(requestUri, method, inputObject, contentType, serializerFormat, token);
        }

        /// <summary>
        /// Sends an HTTP request as an asynchronous operation.
        /// </summary>
        /// <param name="requestUri">The request URI.</param>
        /// <param name="method">The HTTP method.</param>
        /// <param name="token">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <returns>
        /// The task object representing the asynchronous operation.
        /// </returns>
        public async Task<HttpResponseMessage> SendAsync(string requestUri, HttpMethod method, CancellationToken? token = null)
        {
            // send the HTTP request
            return await this._httpClient.SendAsyncEx(requestUri, method, token);
        }

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
        public async Task<HttpResponseMessage> SendAsync(string requestUri, HttpMethod method, IDictionary<string, string> requestHeaders, CancellationToken? token = null)
        {
            // first set the headers on the request
            this.SetHeader(requestHeaders);
            // send the HTTP request
            return await this._httpClient.SendAsyncEx(requestUri, method, token);
        }

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
        public async Task<HttpResponseMessage> SendAsync<TInput>(string requestUri, HttpMethod method, TInput inputObject, HttpContentType contentType, SerializerFormat serializerFormat, CancellationToken? token = null)
             where TInput : class
        {
            // send the HTTP request
            return await this._httpClient.SendAsyncEx(requestUri, method, inputObject, contentType, serializerFormat, token);
        }

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
        public async Task<HttpResponseMessage> SendAsync<TInput>(string requestUri, HttpMethod method, TInput inputObject, HttpContentType contentType, SerializerFormat serializerFormat, IDictionary<string, string> requestHeaders, CancellationToken? token = null)
             where TInput : class
        {
            // first set the headers on the request
            this.SetHeader(requestHeaders);
            // send the HTTP request
            return await this._httpClient.SendAsyncEx(requestUri, method, inputObject, contentType, serializerFormat, token);
        }

        #endregion
    }
}