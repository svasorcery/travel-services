using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Oasis.Api.Traveler.Controllers
{
    using Oasis.Api.Traveler.Abstractions;
    using Oasis.Api.Traveler.Models;

    [Route("api/[controller]")]
    public class AccountsController : Controller
    {
        private readonly ICatalogueRepository<Account> _accounts;

        public AccountsController(ICatalogueRepository<Account> accountsRepository)
        {
            _accounts = accountsRepository;
        }


        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok(await _accounts.GetAllAsync());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            return Ok(await _accounts.GetByIdAsync(id));
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody]Account value)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest($"Model {nameof(Account)} is null");
            }

            return Ok(await _accounts.CreateAsync(value));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put([FromRoute]int id, [FromBody]Account value)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest($"Model {nameof(Account)} is null");
            }

            if ((await _accounts.GetByIdAsync(id)) == null)
            {
                return NotFound($"{nameof(Account)} with id = {id} was not found");
            }

            return Ok(await _accounts.UpdateAsync(value));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromRoute]int id)
        {
            var value = await _accounts.GetByIdAsync(id);

            if (value == null)
            {
                return NotFound($"{nameof(Account)} with id = {id} was not found");
            }

            await _accounts.DeleteAsync(value);

            return Ok();
        }
    }
}
