using Microsoft.Extensions.Options;
using Microsoft.EntityFrameworkCore;
using Oasis.Api.Traveler.Models;

namespace Oasis.Api.Traveler.DbContexts
{
    public class OasisDbContext : DbContext
    {
        private readonly OasisDbContextOptions _options;

        public OasisDbContext(IOptions<OasisDbContextOptions> optionsAccessor) : base()
        {
            _options = optionsAccessor.Value;
        }


        public DbSet<Person> Persons { get; set; }
        public DbSet<Account> Accounts { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(_options.ConnectionString);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            CommonModelCreating(modelBuilder);

            base.OnModelCreating(modelBuilder);
        }

        private void CommonModelCreating(ModelBuilder modelBuilder)
        {
            var personEntity = modelBuilder.Entity<Person>();
            personEntity.ToTable("Persons");
            personEntity.Property(x => x.LastName).IsRequired().HasMaxLength(40);
            personEntity.Property(x => x.FirstName).IsRequired().HasMaxLength(40);
            personEntity.Property(x => x.MiddleName).HasMaxLength(40);

            var accountEntity = modelBuilder.Entity<Account>();
            accountEntity.ToTable("Accounts");
            accountEntity.Property(x => x.UserName).IsRequired().HasMaxLength(25);
            accountEntity.Property(x => x.Password).IsRequired().HasMaxLength(40);
            accountEntity.Property(x => x.Email).IsRequired();
            accountEntity.Property(x => x.MobilePhone).IsRequired().HasMaxLength(16);
            accountEntity.Ignore(x => x.Person);
        }
    }
}
