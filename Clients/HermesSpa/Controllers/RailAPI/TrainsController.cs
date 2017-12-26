using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;

namespace HermesSpa.Controllers.RailAPI
{
    using HermesSpa.Services;

    [Route("api/[controller]")]
    public class TrainsController : Controller
    {
        private readonly RailKaolinApiClient _rail;

        public TrainsController(RailKaolinApiClient railClient)
        {
            _rail = railClient;
        }


        [HttpGet]
        public async Task<IActionResult> Get(RailKaolinApiClient.TrainsListRequest request)
        {
            if (request == null)
            {
                return BadRequest(nameof(request));
            }

            return Ok(await _rail.QueryTrainsAsync(request));
        }
    }
}
