using System.Threading.Tasks;

namespace Kaolin.Services.PassRzdRu.Parser
{
    using Kaolin.Services.PassRzdRu.Parser.Catalogues;

    public partial class PassRzdRuClient
    {
        public Task<DocumentTypes> GetDocumentTypesAsync(string locale = "ru")
           => Get<DocumentTypes>("https://pass.rzd.ru/catalogue/app_passport_type/" + locale);
    }
}
