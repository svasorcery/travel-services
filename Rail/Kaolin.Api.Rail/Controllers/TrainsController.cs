using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Kaolin.Api.Rail.Controllers
{
    using Kaolin.Models.Rail.Abstractions;
    using Kaolin.Infrastructure.SessionStore;
    using Kaolin.Api.Rail.Models;
    using Kaolin.Models.Rail;

    [Route("api/[controller]")]
    public class TrainsController : Controller
    {
        private readonly IRailClient _rail;
        private readonly ISessionProvider _ssp;
        private readonly ILogger _logger;

        public TrainsController(IRailClient railClient, ISessionProvider sessionProvider, ILogger<TrainsController> logger)
        {
            _rail = railClient;
            _ssp = sessionProvider;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> Get(QueryTrains.Request request)
        {
            if (request == null)
            {
                return BadRequest($"{typeof(QueryTrains.Request)} is null");
            }

            try
            {
                var session = _ssp.Create(TimeSpan.FromDays(1));

                var result = await _rail.QueryTrainsAsync(session, request);

                await _ssp.SaveAsync(session);

                return Ok(new GetTrainsResult
                {
                    SessionId = session.Id,
                    Trains = result.Trains
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "GetTrains failed");

                return StatusCode(500);
            }
        }

        [HttpGet("{sessionId}/{optionRef:int}/cars")]
        public async Task<IActionResult> Cars(string sessionId, int optionRef)
        {
            try
            {
                var session = await _ssp.LoadAsync(sessionId);

                var result = await _rail.QueryCarsAsync(session, new QueryCars.Request(optionRef));

                await _ssp.SaveAsync(session);

                return new JsonResult(new GetCarsResult
                {
                    SessionId = sessionId,
                    TrainOption = optionRef,
                    Cars = result.Cars,
                    InsuranceProviders = result.InsuranceProviders,
                    AgeLimits = result.AgeLimits
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "GetCars failed");

                return StatusCode(500);
            }
        }

        [HttpGet("{sessionId}/{trainRef:int}/cars/{optionRef:int}")]
        public async Task<IActionResult> Seats(string sessionId, int trainRef, int optionRef)
        {
            try
            {
                var session = await _ssp.LoadAsync(sessionId);

                var result = await _rail.QueryCarAsync(session, new QueryCar.Request(trainRef, optionRef));

                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "GetSeats failed");

                return StatusCode(500);
            }
        }
    }
}
