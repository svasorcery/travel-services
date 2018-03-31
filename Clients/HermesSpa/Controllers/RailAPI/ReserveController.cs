using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace HermesSpa.Controllers.RailAPI
{
    using HermesSpa.Services;

    [Route("api/rail/[controller]")]
    public class ReserveController : Controller
    {
        private readonly RailKaolinApiClient _rail;

        public ReserveController(RailKaolinApiClient railClient)
        {
            _rail = railClient;
        }


        [HttpPost]
        public async Task<IActionResult> Create(RailKaolinApiClient.ReserveCreateRequest request)
        {
            if (request == null)
            {
                return BadRequest(nameof(request));
            }

            return Ok(await _rail.CreateReserveAsync(request));
        }
    }
}
