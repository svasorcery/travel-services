namespace Kaolin.Services.PassRzdRu.Parser.Structs
{
    public interface IParserResponse
    {
        string Result { get; }
    }

    public interface IRidRequestResponse : IParserResponse
    {
        string RID { get; }
    }

    public class ParserErrorResponse
    {
        public string Error { get; set; }
        public string Info { get; set; }
    }
}
