using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace Kaolin.Api.Rail.Controllers
{
    using Kaolin.Infrastructure.Database;

    [Authorize]
    [Route("api/[controller]")]
    public class CountriesController : Controller
    {
        private readonly CountriesDbContext _countries;

        public CountriesController(CountriesDbContext countries)
        {
            _countries = countries;
        }

        [HttpGet]
        public async Task<IActionResult> Get([FromQuery]string term)
        {
            return Ok(await _countries.SearchAsync(term));
        }
    }
}
