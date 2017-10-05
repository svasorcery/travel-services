using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace Kaolin.Services.PassRzdRu.Parser
{
    public partial class PassRzdRuClient
    {
        private readonly CookieContainer _cookieContainer;

        public PassRzdRuClient()
        {
            _cookieContainer = new CookieContainer();
        }


        public async Task<Session> Authorize(string login, string password)
        {
            var url = "https://pass.rzd.ru/selfcare/j_security_check/ru";

            var response = await LoginAsync(url, login, password);

            if (response.StatusCode != HttpStatusCode.OK)
            {
                _log.LogWarning("Login {Login} fault with {StatusCode}", login, response.StatusCode);
                throw new UnexpectedStatusCodeException("GET", url, (int)response.StatusCode);
            }

            var content = await ParseAsync(response);

            if (!content.Contains("Выход"))
            {
                _log.LogWarning("Login {Login} fault with {Content}", login, content);
                throw new UnexpectedContentException("[Выход] not found", "GET", url, content);
            }

            _log.LogInformation("Login {Login} success", login);

            return GetSession(login);
        }

        private Task<HttpResponseMessage> LoginAsync(string url, string login, string password)
        {
            var uri = new Uri(url);

            var http = InitHttpClient();

            var response = http.PostAsync(uri, EncodeCredentials(login, password)).Result;

            if (response.StatusCode == HttpStatusCode.Redirect)
            {
                uri = response.Headers.Location;
                response = http.GetAsync(uri).Result;
            }

            return Task.FromResult(response);
        }

        private HttpClient InitHttpClient()
        {
            var handler = new HttpClientHandler();
            handler.CookieContainer = _cookieContainer;
            handler.UseCookies = true;

            return new HttpClient(handler);
        }

        private HttpContent EncodeCredentials(string login, string password)
        {
            return new FormUrlEncodedContent(new Dictionary<string, string>
            {
                { "j_username", login },
                { "j_password", password },
                { "action", "Вход" }
            });
        }

        private Task<string> ParseAsync(HttpResponseMessage response)
        {
            return response.Content.ReadAsStringAsync();
        }

        private Session GetSession(string login)
        {
            var cookies = _cookieContainer.GetCookies(new Uri("https://pass.rzd.ru"));

            return new Session
            {
                UserName = login,
                LtpaToken2 = cookies["LtpaToken2"].Value,
                JSessionId = cookies["JSESSIONID"].Value
            };
        }
    }
}
