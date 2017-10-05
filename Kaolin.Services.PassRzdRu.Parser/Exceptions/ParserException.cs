using System;

namespace Kaolin.Services.PassRzdRu.Parser
{
    public partial class ParserException : Exception
    {
        public ParserException(string message) : base(message)
        {

        }

        public ParserException(string message, Exception innerException) : base(message, innerException)
        {

        }
    }
}
