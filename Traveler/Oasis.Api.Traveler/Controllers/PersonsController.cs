using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Oasis.Api.Traveler.Controllers
{
    using Oasis.Api.Traveler.Abstractions;
    using Oasis.Api.Traveler.Models;

    [Route("api/[controller]")]
    public class PersonsController : Controller
    {
        private readonly ICatalogueRepository<Person> _persons;

        public PersonsController(ICatalogueRepository<Person> personsRepository)
        {
            _persons = personsRepository;
        }


        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok(await _persons.GetAllAsync());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            return Ok(await _persons.GetByIdAsync(id));
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody]Person value)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest($"Model {nameof(Person)} is null");
            }

            return Ok(await _persons.CreateAsync(value));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put([FromRoute]int id, [FromBody]Person value)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest($"Model {nameof(Person)} is null");
            }

            if ((await _persons.GetByIdAsync(id)) == null)
            {
                return NotFound($"{nameof(Person)} with id = {id} was not found");
            }

            return Ok(await _persons.UpdateAsync(value));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromRoute]int id)
        {
            var value = await _persons.GetByIdAsync(id);

            if (value == null)
            {
                return NotFound($"{nameof(Person)} with id = {id} was not found");
            }

            await _persons.DeleteAsync(value);

            return Ok();
        }
    }
}
