namespace Kaolin.Services.PassRzdRu.Parser.Exceptions
{
    public class NoMoreRetriesException : ParserException
    {
        public NoMoreRetriesException(string url, int count) 
            : base($"NoMoreRetries {url} {count}")
        {
        }
    }
}
