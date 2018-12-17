using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace Kaolin.Api.Rail.Controllers
{
    using Kaolin.Infrastructure.Database;

    [Authorize]
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
