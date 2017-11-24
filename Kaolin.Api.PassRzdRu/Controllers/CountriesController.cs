using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Kaolin.Api.PassRzdRu.Controllers
{
    using Kaolin.Infrastructure.Database;

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
