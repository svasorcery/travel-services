using System;
using Newtonsoft.Json;
using System.Net.Http;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.Extensions.Logging;

namespace Kaolin.Services.PassRzdRu.Parser
{
    using Kaolin.Services.PassRzdRu.Parser.Structs;
    using Kaolin.Services.PassRzdRu.Parser.Exceptions;

    public partial class PassRzdRuClient
    {
        private static readonly JsonSerializer _json;
        
        static PassRzdRuClient()
        {
            _json = JsonSerializer.Create(new JsonSerializerSettings { ContractResolver = new Newtonsoft.Json.Serialization.CamelCasePropertyNamesContractResolver() });
        }


        public async Task<T> ReadAs<T>(HttpContent content)
        {
            using (var stream = await content.ReadAsStreamAsync())
            {
                using (var reader = new System.IO.StreamReader(stream))
                {
                    return _json.Deserialize<T>(new JsonTextReader(reader));
                }
            }
        }

        public T ReadAs<T>(string content)
        {
            using (var reader = new System.IO.StringReader(content))
            {
                return _json.Deserialize<T>(new JsonTextReader(reader));
            }
        }

        protected async Task<T> Get<T>(string url)
        {
            var client = new HttpClient();
            _log.LogInformation("GET {Url}", url);

            var response = await client.GetAsync(url);

            if (response.IsSuccessStatusCode == false)
            {
                _log.LogWarning("Unsuccessful status code GET {Url} {StatusCode}", url, response.StatusCode);
            }

            return await ReadAs<T>(response.Content);
        }

        public Task<T> PostRidDictionary<T>(string requestUri, Session session, Config.PollingConfig.Polling config, Dictionary<string, string> requestParams) where T : IRidRequestResponse
            => PostRidDictionary<T>(requestUri, new HttpClient(RestoreSession(session)), config, requestParams);

        private async Task<T> PostRidDictionary<T>(string requestUri, HttpClient http, Config.PollingConfig.Polling config, Dictionary<string, string> requestParams) where T : IRidRequestResponse
        {
            // Get RID
            var response = await http.PostAsync(requestUri, new FormUrlEncodedContent(requestParams));
            var content = await response.Content.ReadAsStringAsync();
            var result = ReadAs<T>(content);

            if ("RID" == result.Result)
            {
                requestParams.Add("rid", result.RID);
            }

            var retries = config.MaxRetry;
            for (; ; )
            {
                if ("OK".Equals(result.Result))
                {
                    return result;
                }
                else if ("RID".Equals(result.Result))
                {
                    if (--retries <= 0)
                    {
                        break;
                    }
                    await Task.Delay(config.Interval);
                    response = await http.PostAsync(requestUri, new FormUrlEncodedContent(requestParams));
                    content = await response.Content.ReadAsStringAsync();
                    result = ReadAs<T>(content);
                }
                else if ("FAIL".Equals(result.Result))
                {
                    var errorResult = ReadAs<ParserErrorResponse>(content);
                    var message = errorResult.Info ?? errorResult.Error;

                    throw new ErrorResponseException(message, "POST", requestUri);
                }
                else
                {
                    throw new UnexpectedContentException("Unexpected Status", "POST", requestUri, content);
                }
            };

            throw new NoMoreRetriesException(requestUri, config.MaxRetry);
        }


        public Task<T> PostDictionary<T>(string requestUri, Session session, Dictionary<string, string> requestParams) where T : IParserResponse
            => PostDictionary<T>(requestUri, new HttpClient(RestoreSession(session)), requestParams);

        public async Task<T> PostDictionary<T>(string requestUri, HttpClient http, Dictionary<string, string> requestParams) where T : IParserResponse
        {
            var response = await http.PostAsync(requestUri, new FormUrlEncodedContent(requestParams));

            var result = await ReadAs<T>(response.Content);

            if ("OK".Equals(result.Result))
            {
                return result;
            }
            else
            {
                var content = await response.Content.ReadAsStringAsync();
                var errorResult = ReadAs<ParserErrorResponse>(content);
                var message = errorResult.Info ?? errorResult.Error;

                throw new ErrorResponseException(message, "POST", requestUri);
            }
        }
    }
}
