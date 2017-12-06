using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Kaolin.Api.Rail.Controllers
{
    using Kaolin.Infrastructure.Database;

    [Route("api/[controller]")]
    public class StationsController : Controller
    {
        private readonly StationsDbContext _stations;

        public StationsController(StationsDbContext stations)
        {
            _stations = stations;
        }

        [HttpGet]
        public async Task<IActionResult> Get([FromQuery]string term)
        {
            return Ok(await _stations.SearchAsync(term));
        }
    }
}
