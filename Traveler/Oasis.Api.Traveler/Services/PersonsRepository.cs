using System;
using System.Linq;
using System.Threading.Tasks;
using Oasis.Api.Traveler.Abstractions;
using Oasis.Api.Traveler.Models;
using Oasis.Api.Traveler.DbContexts;
using Microsoft.EntityFrameworkCore;

namespace Oasis.Api.Traveler.Services
{
    public class PersonsRepository : IPersonsRepository
    {
        private readonly OasisDbContext _db;

        public PersonsRepository(OasisDbContext context)
        {
            _db = context;
        }


        public Task<Person[]> GetAllAsync()
        {
            return _db.Persons
                .AsNoTracking()
                .OrderBy(x => x.LastName)
                .ThenBy(x => x.FirstName)
                .ThenBy(x => x.MiddleName)
                .ToArrayAsync();
        }

        public Task<Person> GetByIdAsync(int id)
        {
            return _db.Persons
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Person> CreateAsync(Person model)
        {
            await _db.AddAsync(model);

            await _db.SaveChangesAsync();

            return model;
        }

        public async Task<Person> UpdateAsync(Person model)
        {
            var update = await _db.Persons
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == model.Id);

            update.FirstName = model.FirstName;
            update.MiddleName = model.MiddleName;
            update.LastName = model.LastName;
            update.Gender = model.Gender;
            update.BirthDate = model.BirthDate;

            _db.Update(update);

            await _db.SaveChangesAsync();

            return update;
        }

        public async Task DeleteAsync(int id)
        {
            var delete = await GetByIdAsync(id);
            await DeleteAsync(delete);
        }

        public Task DeleteAsync(Person model)
        {
            _db.Persons.Remove(model);
            return Task.FromResult(0);
        }
    }
}
