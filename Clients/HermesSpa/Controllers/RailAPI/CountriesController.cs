using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace HermesSpa.Controllers.RailAPI
{
    using HermesSpa.Services;
 
    [Route("api/rail/[controller]")]
    public class CountriesController : Controller
    {
        private readonly RailKaolinApiClient _rail;

        public CountriesController(RailKaolinApiClient railClient)
        {
            _rail = railClient;
        }


        [HttpGet]
        public async Task<IActionResult> Get([FromQuery]string term)
        {
            return Ok(await _rail.SearchCountriesAsync(term));
        }
    }
}
