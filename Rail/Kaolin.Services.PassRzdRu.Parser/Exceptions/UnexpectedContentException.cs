namespace Kaolin.Services.PassRzdRu.Parser
{
    public class UnexpectedContentException : ParserException
    {
        private readonly string _method;
        private readonly string _url;
        private readonly string _content;

        public string Method => _method;
        public string Url => _url;
        public string Content => _content;

        public UnexpectedContentException(string message, string method, string url, string content) 
            : base($"Unexpected content {method} {content}: {message}")
        {
            _method = method;
            _url = url;
            _content = content;
        }
    }
}
