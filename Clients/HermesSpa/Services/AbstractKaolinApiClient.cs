using System;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Net.Http.Headers;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace HermesSpa.Services
{
    public abstract partial class AbstractKaolinApiClient
    {
        protected abstract string BaseUrl { get; }
        protected virtual bool CamelCaseSerialization => false;

        private readonly HttpClient _client;
        private static readonly JsonSerializer _json;
        private readonly ILogger _logger;

        static AbstractKaolinApiClient()
        {
            _json = new JsonSerializer();
        }

        public AbstractKaolinApiClient(ILogger logger)
        {
            _logger = logger;
            _client = new HttpClient
            {
                BaseAddress = new Uri(BaseUrl.EndsWith("/") ? BaseUrl : BaseUrl + "/")
            };
            if (CamelCaseSerialization)
            {
                _json.ContractResolver = new Newtonsoft.Json.Serialization.DefaultContractResolver
                {
                    NamingStrategy = new Newtonsoft.Json.Serialization.CamelCaseNamingStrategy()
                };
            }
        }

        public async Task<T> ReadAs<T>(HttpContent content)
        {
            using (var stream = await content.ReadAsStreamAsync())
            {
                using (var reader = new StreamReader(stream))
                {
                    return _json.Deserialize<T>(new JsonTextReader(reader));
                }
            }
        }

        public T ReadAs<T>(string content)
        {
            using (var reader = new StringReader(content))
            {
                return _json.Deserialize<T>(new JsonTextReader(reader));
            }
        }

        protected Task<T> Get<T>(string url, string p1, string v1)
            => Get<T>($"{url}?{p1}={Uri.EscapeDataString(v1)}");

        protected Task<T> Get<T>(string url, params (string k, string v)[] p)
            => Get<T>(string.Concat(url, "?", string.Join("&", p.Where(x => x.v != null).Select(x => $"{x.k}={Uri.EscapeDataString(x.v)}"))));

        protected async Task<T> Get<T>(string url)
        {
            _logger.LogInformation("GET {BaseUrl}/{Url}", BaseUrl, url);
            var response = await _client.GetAsync(url);

            if (response.IsSuccessStatusCode == false)
            {
                _logger.LogWarning("Unsuccessful status code GET {BaseUrl}/{Url} {StatusCode}",
                    BaseUrl, url, response.StatusCode);

                var error = await ReadAs<ApiErrorResponseResult>(response.Content);
                throw new ApiException(error);
            }

            return await ReadAs<T>(response.Content);
        }

        protected async Task<TOut> Post<TOut, TIn>(string url, TIn p)
        {
            _logger.LogInformation("POST {BaseUrl}/{Url}", BaseUrl, url);

            using (var writer = new StringWriter())
            {
                _json.Serialize(writer, p);
                var response = await _client.PostAsync(
                    url,
                    new StringContent(writer.GetStringBuilder().ToString(), System.Text.Encoding.UTF8, "application/json")
                );

                if (response.IsSuccessStatusCode == false)
                {
                    _logger.LogWarning("Unsuccessful status code POST {BaseUrl}/{Url} {StatusCode}",
                        BaseUrl, url, response.StatusCode);

                    var error = await ReadAs<ApiErrorResponseResult>(response.Content);
                    throw new ApiException(error);
                }

                return await ReadAs<TOut>(response.Content);
            }
        }

        protected async Task<TOut> Put<TOut, TIn>(string url, TIn p)
        {
            _logger.LogInformation("PUT {BaseUrl}/{Url}", BaseUrl, url);

            using (var writer = new StringWriter())
            {
                _json.Serialize(writer, p);
                var response = await _client.PutAsync(
                    url,
                    new StringContent(writer.GetStringBuilder().ToString(), System.Text.Encoding.UTF8, "application/json")
                );

                if (response.IsSuccessStatusCode == false)
                {
                    _logger.LogWarning("Unsuccessful status code PUT {BaseUrl}/{Url} {StatusCode}",
                        BaseUrl, url, response.StatusCode);

                    var error = await ReadAs<ApiErrorResponseResult>(response.Content);
                    throw new ApiException(error);
                }

                return await ReadAs<TOut>(response.Content);
            }
        }

        protected async Task Delete(string url)
        {
            _logger.LogInformation("DELETE {BaseUrl}/{Url}", BaseUrl, url);
            var response = await _client.DeleteAsync(url);

            if (response.IsSuccessStatusCode == false)
            {
                _logger.LogWarning("Unsuccessful status code DELETE {BaseUrl}/{Url} {StatusCode}",
                    BaseUrl, url, response.StatusCode);

                var error = await ReadAs<ApiErrorResponseResult>(response.Content);
                throw new ApiException(error);
            }
        }
    }
 }
