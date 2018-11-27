using System.Threading.Tasks;
using Oasis.Api.Traveler.Models;

namespace Oasis.Api.Traveler.Abstractions
{
    public interface IPersonsRepository
    {
        Task<Person[]> GetAllAsync();
        Task<Person> GetByIdAsync(int id);
        Task<Person> CreateAsync(Person model);
        Task<Person> UpdateAsync(Person model);
        Task DeleteAsync(int id);
        Task DeleteAsync(Person model);
    }
}
