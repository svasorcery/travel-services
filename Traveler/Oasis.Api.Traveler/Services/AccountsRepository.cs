using System;
using System.Linq;
using System.Threading.Tasks;
using Oasis.Api.Traveler.Abstractions;
using Oasis.Api.Traveler.Models;
using Oasis.Api.Traveler.DbContexts;
using Microsoft.EntityFrameworkCore;

namespace Oasis.Api.Traveler.Services
{
    public class AccountsRepository : ICatalogueRepository<Account>
    {
        private readonly OasisDbContext _db;

        public AccountsRepository(OasisDbContext context)
        {
            _db = context;
        }


        public Task<Account[]> GetAllAsync()
        {
            return _db.Accounts
                .AsNoTracking()
                .Include(x => x.Person)
                .OrderBy(x => x.UserName)
                .ToArrayAsync();
        }

        public Task<Account> GetByIdAsync(int id)
        {
            return _db.Accounts
                .AsNoTracking()
                .Include(x => x.Person)
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Account> CreateAsync(Account model)
        {
            model.CreatedAt = DateTime.UtcNow;
            model.IsDeleted = false;

            await _db.AddAsync(model);

            await _db.SaveChangesAsync();

            return model;
        }

        public async Task<Account> UpdateAsync(Account model)
        {
            var update = await _db.Accounts
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == model.Id);

            update.UserName = model.UserName;
            update.Password = model.Password;
            update.Email = model.Email;
            update.MobilePhone = model.MobilePhone;
            update.AvatarUri = model.AvatarUri;
            update.LastUpdateAt = DateTime.UtcNow;

            _db.Update(update);

            await _db.SaveChangesAsync();

            return update;
        }

        public async Task DeleteAsync(int id)
        {
            var delete = await GetByIdAsync(id);
            await DeleteAsync(delete);
        }

        public Task DeleteAsync(Account model)
        {
            model.IsDeleted = true;
            _db.Accounts.Update(model);
            return _db.SaveChangesAsync();
        }
    }
}
