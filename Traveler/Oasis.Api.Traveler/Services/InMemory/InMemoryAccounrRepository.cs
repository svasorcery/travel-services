using System;
using System.Linq;
using System.Threading.Tasks;
using Oasis.Api.Traveler.Abstractions;
using Oasis.Api.Traveler.Models;
using Oasis.Api.Traveler.DbContexts;

namespace Oasis.Api.Traveler.Services
{
    public class InMemoryAccountsRepository : ICatalogueRepository<Account>
    {
        private readonly InMemoryStorageContext _db;

        public InMemoryAccountsRepository(InMemoryStorageContext context)
        {
            _db = context;
        }


        public Task<Account[]> GetAllAsync()
        {
            return Task.FromResult(_db.Accounts.ToArray());
        }

        public Task<Account> GetByIdAsync(int id)
        {
            var model = _db.Accounts.First(x => x.Id == id);
            model.Person = _db.Persons.FirstOrDefault(p => p.Id == model.PersonId);

            return Task.FromResult(model);
        }

        public Task<Account> CreateAsync(Account model)
        {
            model.Id = _db.Accounts.Count() > 0 ? _db.Accounts.Last().Id + 1 : 1;
            model.CreatedAt = DateTime.UtcNow;
            model.IsDeleted = false;

            _db.Accounts.Add(model);

            return Task.FromResult(model);
        }

        public Task<Account> UpdateAsync(Account model)
        {
            var update = _db.Accounts.First(x => x.Id == model.Id);

            update.UserName = model.UserName;
            update.Password = model.Password;
            update.Email = model.Email;
            update.MobilePhone = model.MobilePhone;
            update.AvatarUri = model.AvatarUri;
            update.LastUpdateAt = DateTime.UtcNow;

            return Task.FromResult(update);
        }

        public Task DeleteAsync(int id)
        {
            var model = _db.Accounts.First(x => x.Id == id);
            model.IsDeleted = true;
            return Task.FromResult(0);
        }

        public Task DeleteAsync(Account model)
        {
            model.IsDeleted = true;
            return Task.FromResult(0);
        }
    }
}
