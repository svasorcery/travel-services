using System.Threading.Tasks;

namespace Kaolin.Services.PassRzdRu.Parser
{
    using Kaolin.Services.PassRzdRu.Parser.Structs;
    using Kaolin.Services.PassRzdRu.Parser.Catalogues;

    public partial class PassRzdRuClient
    {
        public Task<DocumentTypes> GetDocumentTypesAsync(string locale = "ru")
           => Get<DocumentTypes>("https://pass.rzd.ru/catalogue/app_passport_type/" + locale);

        public Task<Layer5887> MedicalPolicyCostAsync(Session session, Layer5887.Request request)
            => PostRidDictionary<Layer5887>("https://pass.rzd.ru/ticket/secure/ru?layer_id=5887", session, _config.Polling.Order, request.ToDictionary());
    }
}
