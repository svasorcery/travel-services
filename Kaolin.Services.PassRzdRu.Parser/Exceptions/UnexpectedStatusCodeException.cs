namespace Kaolin.Services.PassRzdRu.Parser
{
    public class UnexpectedStatusCodeException : ParserException
    {
        private readonly string _url, _method;
        private readonly int _statusCode;

        public string Url => _url;
        public string Method => _method;
        public int StatusCode => _statusCode;

        public UnexpectedStatusCodeException(string method, string url, int statusCode) 
            : base($"Unexpected status code: {method} {url} got {statusCode}")
        {
            _url = url;
            _method = method;
            _statusCode = statusCode;
        }
    }
}
