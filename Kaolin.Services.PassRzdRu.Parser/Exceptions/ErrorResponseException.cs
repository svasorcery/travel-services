namespace Kaolin.Services.PassRzdRu.Parser.Exceptions
{
    public class ErrorResponseException : ParserException
    {
        private readonly string _method;
        private readonly string _url;

        public string Method => _method;
        public string Url => _url;

        public ErrorResponseException(string message, string method, string url) 
            : base(message)
        {
            _method = method;
            _url = url;
        }
    }
}
