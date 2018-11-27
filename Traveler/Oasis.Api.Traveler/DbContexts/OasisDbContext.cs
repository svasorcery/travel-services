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


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(_options.ConnectionString);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            PersonModelCreating(modelBuilder);

            base.OnModelCreating(modelBuilder);
        }

        private void PersonModelCreating(ModelBuilder modelBuilder)
        {
            var personEntity = modelBuilder.Entity<Person>();
            personEntity.ToTable("Persons");
            personEntity.Property(x => x.LastName).IsRequired().HasMaxLength(40);
            personEntity.Property(x => x.FirstName).IsRequired().HasMaxLength(40);
            personEntity.Property(x => x.MiddleName).HasMaxLength(40);
        }
    }
}
