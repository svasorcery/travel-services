using System;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Kaolin.Api.Rail.Controllers
{
    using Kaolin.Models.Rail.Abstractions;
    using Kaolin.Infrastructure.SessionStore;
    using Kaolin.Models.Rail;

    [Route("api/[controller]")]
    public class ReserveController : RailControllerBase
    {
        private readonly IRailClient _rail;
        private readonly ISessionProvider _ssp;
        private readonly ILogger<ReserveController> _logger;

        public ReserveController(IRailClient railClient, ISessionProvider sessionProvider, ILogger<ReserveController> logger)
        {
            _rail = railClient;
            _ssp = sessionProvider;
            _logger = logger;
        }


        [HttpPost]
        public async Task<IActionResult> Create([FromBody]QueryReserveCreate.Request request)
        {
            if (request == null)
            {
                return BadRequest(ModelState);
            }

            _logger.LogInformation($"Rail reserve attempt SessionId: {request.Option.SessionId}, CarOptionRef: {request.Option.CarOptionRef}");

            var session = await _ssp.LoadAsync(request.Option.SessionId);

            if (session == null)
            {
                return SessionExpired();
            }

            try
            {
                var result = await _rail.CreateReserveAsync(session, request);
                await _ssp.SaveAsync(session);
                return Ok(result);
            }
            catch (Services.PassRzdRu.Parser.ParserException ex)
            {
                _logger.LogError(LogEventId.RESERVE_TICKET_FAIL, ex, $"{nameof(_rail.CreateReserveAsync)} failed, SessionId: {session.Id}, Request: {request}");
                return StatusCode((int)HttpStatusCode.InternalServerError, new { message = ex.Message });
            }
            catch (Exception ex)
            {
                _logger.LogError(LogEventId.RESERVE_TICKET_FAIL, ex, $"{nameof(_rail.CreateReserveAsync)} failed, SessionId: {session.Id}, Request: {request}");
                return BadRequest();
            }
        }

        [HttpPost("cancel")]
        public async Task<IActionResult> Cancel([FromBody]QueryReserveCancel.Request request)
        {
            if (request == null || request.SessionId == null)
            {
                return BadRequest();
            }

            _logger.LogInformation($"Rail reserve cancel SessionId: {request.SessionId}");

            var session = await _ssp.LoadAsync(request.SessionId);

            if (session == null)
            {
                return SessionExpired();
            }

            try
            {
                var result = await _rail.CancelReserveAsync(session);
                await _ssp.SaveAsync(session);
                return Ok(result);
            }
            catch (Services.PassRzdRu.Parser.ParserException ex)
            {
                _logger.LogError(LogEventId.CANCEL_RESERVE_FAIL, ex, $"{nameof(_rail.CancelReserveAsync)} failed {request}");
                return StatusCode((int)HttpStatusCode.InternalServerError, ex);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
    }
}
