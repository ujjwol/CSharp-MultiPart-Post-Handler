using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CSharpMultiPartPostHandler.RestClient
{



    public class CSharpMultiPartPostHandlerClientBase : IDisposable
    {
        private readonly object disposeLock = new object();
        private bool disposed;

        private readonly HttpClient client;

        public CSharpMultiPartPostHandlerClientBase()
        {
            this.client = new HttpClient();
        }

        /// Asynchronously invokes a method on the API, and performs a projection on the
        /// //Tumblr# implementation
        /// response before returning the result.
        public async Task<TResult> CallApiMethodAsync<TResponse, TResult>(ApiMethod method, Func<TResponse, TResult> projection, CancellationToken cancellationToken, IEnumerable<JsonConverter> converters = null)
            where TResult : class
            where TResponse : class
        {
            if (disposed)
                throw new ObjectDisposedException("PreCapricaClient");

            if (method == null)
                throw new ArgumentNullException("method");

            if (projection == null)
                throw new ArgumentNullException("projection");

            var response = await CallApiMethodAsync<TResponse>(method, cancellationToken, converters).ConfigureAwait(false);
            return projection(response);
        }


        /// Asynchronously invokes a method on the API without expecting a response.
        public Task CallApiMethodNoResultAsync(ApiMethod method, CancellationToken cancellationToken)
        {
            if (disposed)
                throw new ObjectDisposedException("PreCapricaClient");

            if (method == null)
                throw new ArgumentNullException("method");

            return CallApiMethodAsync<object>(method, cancellationToken);
        }


        /// Asynchronously invokes a method on the API.
        public async Task<TResult> CallApiMethodAsync<TResult>(ApiMethod method, CancellationToken cancellationToken, IEnumerable<JsonConverter> converters = null)
            where TResult : class
        {
            if (disposed)
                throw new ObjectDisposedException("PreCapricaClient");

            if (method == null)
                throw new ArgumentNullException("method");

            //build the api call URL
            StringBuilder apiRequestUrl = new StringBuilder(method.Url);
            if (method.HttpMethod == HttpMethod.Get && method.Parameters.Count > 0)
            {
                //we are in a HTTP GET: add the request parameters to the query string
                apiRequestUrl.Append("?");
                apiRequestUrl.Append(method.Parameters.ToFormUrlEncoded());
            }

            using (var request = new HttpRequestMessage(method.HttpMethod, apiRequestUrl.ToString()))
            {

                if (method.Parameters.Any(c => c is BinaryMethodParameter))
                {
                    //if there is binary content we submit a multipart form request
                    var content = new MultipartFormDataContent();
                    foreach (var p in method.Parameters)
                        content.Add(p.AsHttpContent());

                    request.Content = content;
                }
                else
                {
                    //otherwise just a form url encoded request
                    var content = new FormUrlEncodedContent(method.Parameters.Select(c => new KeyValuePair<string, string>(c.Name, ((StringMethodParameter)c).Value)));
                    request.Content = content;
                }

                using (var response = await client.SendAsync(request, cancellationToken).ConfigureAwait(false))
                {
                    var stream = await response.Content.ReadAsStreamAsync().ConfigureAwait(false);
                    using (var reader = new JsonTextReader(new StreamReader(stream)))
                    {
                        var serializer = CreateSerializer(converters);
                        if (response.IsSuccessStatusCode)
                        {
                            return serializer.Deserialize<PreCapricaRawResponse<TResult>>(reader).Response;
                        }
                        else
                        {
                            var errorResponse = (response.StatusCode == System.Net.HttpStatusCode.Unauthorized) ? new string[0] : serializer.Deserialize<PreCapricaRawResponse<string[]>>(reader).Response;
                            throw new CSharpMultiPartPostHandlerException(response.StatusCode, response.ReasonPhrase, errorResponse);
                        }
                    }
                }
            }
        }


        private JsonSerializer CreateSerializer(IEnumerable<JsonConverter> converters)
        {
            JsonSerializer serializer = new JsonSerializer();
            if (converters != null)
            {
                foreach (JsonConverter converter in converters)
                    serializer.Converters.Add(converter);
            }

            return serializer;
        }


        #region IDisposable Implementation

        /// <summary>
        /// Disposes of the object and the internal HttpClient instance.
        /// </summary>
        public void Dispose()
        {
            if (!disposed)
            {
                lock (disposeLock)
                {
                    if (!disposed)
                    {
                        disposed = true;
                        client.Dispose();
                        Dispose(true);
                        GC.SuppressFinalize(this);
                    }
                }
            }
        }

        /// <summary>
        /// Subclasses can override this method to provide custom
        /// disposing logic.
        /// </summary>
        /// <param name="disposing">
        /// <b>true</b> if managed resources have to be disposed; otherwise <b>false</b>.
        /// </param>
        protected virtual void Dispose(bool disposing)
        {
        }

        #endregion
    }

}

