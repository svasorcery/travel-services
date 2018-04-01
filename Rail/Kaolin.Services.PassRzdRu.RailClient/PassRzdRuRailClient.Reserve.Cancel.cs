using System;
using System.Threading.Tasks;

namespace Kaolin.Services.PassRzdRu.RailClient
{
    using Parser;
    using Kaolin.Models.Rail;
    using Kaolin.Infrastructure.SessionStore;

    public partial class PassRzdRuRailClient
    {
        public async Task<QueryReserveCancel.Result> CancelReserveAsync(ISessionStore session)
        {
            if (session == null)
            {
                throw new ArgumentNullException(nameof(session));
            }

            var login = session.Retrieve<Session>("login");
            var reserve = session.Retrieve<Parser.Structs.Layer5705>("reserve");

            if (reserve == null)
            {
                throw new ArgumentNullException(nameof(reserve));
            }

            var requestData = new Parser.Structs.Layer5769.Request
            {
                OrderId = reserve.SaleOrderId
            };

            var response = await _parser.CancelReserveAsync(login, requestData);

            reserve.Canceled = true;
            session.Store("reserve", reserve);

            return new QueryReserveCancel.Result
            {
                OrderId = reserve.SaleOrderId,
                Code = response.Result,
                Status = response.Status
            };
        }
    }
}
