using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace PilotAppLib.Http
{
    /// <summary>
    /// Interface for simply sending HTTP GET/POST requests
    /// </summary>
    public class SimpleHttp : IDisposable
    {
        private readonly HttpClientHandler _clientHandler;
        private readonly HttpClient _httpClient;


        /// <summary>
        /// Represents the HTTP response data
        /// </summary>
        public class HttpResponse
        {
            private readonly HttpContent _httpContent;

            internal HttpResponse(HttpContent content)
            {
                _httpContent = content;
            }

            /// <summary>
            /// Get response data as string
            /// </summary>
            public string AsString()
            {
                var task = _httpContent.ReadAsStringAsync();
                return task.Result;
            }

            /// <summary>
            /// Get response data as raw bytes
            /// </summary>
            public byte[] AsBytes()
            {
                var task = _httpContent.ReadAsByteArrayAsync();
                return task.Result;
            }
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="SimpleHttp" /> class
        /// </summary>
        public SimpleHttp()
        {
            _clientHandler = new HttpClientHandler {
                AutomaticDecompression = System.Net.DecompressionMethods.All
            };

            _httpClient = new HttpClient(_clientHandler);
            AddDefaultRequestHeaders();
        }

        /// <summary>
        /// Releases the unmanaged resources and disposes of the managed resources used by the <see cref="SimpleHttp" />
        /// </summary>
        public void Dispose()
        {
            _httpClient.Dispose();
            _clientHandler.Dispose();
        }


        /// <summary>Send a GET request to the specified URL</summary>
        /// <param name="url">The URL the request is sent to</param>
        /// <param name="acceptHeader">The <b>Accept</b> header value for the request</param>
        /// <returns>A <see cref="HttpResponse" /> object</returns>
        /// <exception cref="System.Net.Http.HttpRequestException">The request failed due to an underlying issue such as network connectivity, DNS failure, server certificate validation or timeout</exception>
        public HttpResponse Get(string url, string acceptHeader = "*/*")
            => SendRequest(HttpMethod.Get, url, acceptHeader);

        /// <summary>Send a POST request to the specified URL</summary>
        /// <param name="url">The URL the request is sent to</param>
        /// <param name="acceptHeader">The <b>Accept</b> header value for the request</param>
        /// <returns>A <see cref="HttpResponse" /> object</returns>
        /// <exception cref="System.Net.Http.HttpRequestException">The request failed due to an underlying issue such as network connectivity, DNS failure, server certificate validation or timeout</exception>
        public HttpResponse Post(string url, string acceptHeader = "*/*")
            => SendRequest(HttpMethod.Post, url, acceptHeader);


        private void AddDefaultRequestHeaders()
        {
            var productInfo = new ProductInfoHeaderValue("SimpleHttp", "1.0.0");
            _httpClient.DefaultRequestHeaders.UserAgent.Add(productInfo);
        }

        private HttpResponse SendRequest(HttpMethod method, Url url, string acceptHeader)
        {
            using (var request = new HttpRequestMessage(method, url.ToString()))
            {
                request.Headers.Add("Accept", acceptHeader);

                var task = _httpClient.SendAsync(request);

                var response = WaitForTask(task);
                var content = ProcessAndReadResponse(response);

                return content;
            }
        }
        
        private HttpResponseMessage WaitForTask(Task<HttpResponseMessage> responseTask)
        {
            try
            {
                return responseTask.Result;
            }
            catch (AggregateException ex)
            {
                throw ex.InnerException;
            }
        }

        private HttpResponse ProcessAndReadResponse(HttpResponseMessage response)
        {
            response.EnsureSuccessStatusCode();
            return new HttpResponse(response.Content);
        }
    }
}
