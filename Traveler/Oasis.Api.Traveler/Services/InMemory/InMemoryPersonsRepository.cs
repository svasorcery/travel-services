using System.Linq;
using System.Threading.Tasks;
using Oasis.Api.Traveler.Abstractions;
using Oasis.Api.Traveler.Models;
using Oasis.Api.Traveler.DbContexts;

namespace Oasis.Api.Traveler.Services
{
    public class InMemoryPersonsRepository : ICatalogueRepository<Person>
    {
        private readonly InMemoryStorageContext _db;

        public InMemoryPersonsRepository(InMemoryStorageContext context)
        {
            _db = context;
        }


        public Task<Person[]> GetAllAsync()
        {
            return Task.FromResult(_db.Persons.ToArray());
        }

        public Task<Person> GetByIdAsync(int id)
        {
            var model = _db.Persons.First(x => x.Id == id);

            return Task.FromResult(model);
        }

        public Task<Person> CreateAsync(Person model)
        {
            model.Id = _db.Persons.Count() > 0 ? _db.Persons.Last().Id + 1 : 1;
            _db.Persons.Add(model);

            return Task.FromResult(model);
        }

        public Task<Person> UpdateAsync(Person model)
        {
            var update = _db.Persons.First(x => x.Id == model.Id);

            update.FirstName = model.FirstName;
            update.MiddleName = model.MiddleName;
            update.LastName = model.LastName;
            update.BirthDate = model.BirthDate;
            update.Gender = model.Gender;

            return Task.FromResult(update);
        }

        public Task DeleteAsync(int id)
        {
            var model = _db.Persons.First(x => x.Id == id);
            _db.Persons.Remove(model);
            return Task.FromResult(0);
        }

        public Task DeleteAsync(Person model)
        {
            _db.Persons.Remove(model);
            return Task.FromResult(0);
        }
    }
}
