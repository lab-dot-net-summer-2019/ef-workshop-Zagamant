using Microsoft.EntityFrameworkCore;
using SamuraiApp.Data.Configurations;
using SamuraiApp.Domain;

namespace SamuraiApp.Data
{
    public class SamuraiContext : DbContext
    {
        public SamuraiContext()
        {
        }

        public SamuraiContext(DbContextOptions<SamuraiContext> options)
            : base(options)
        {
        }


        public DbSet<Samurai> Samurais { get; set; }
        public DbSet<Battle> Battles { get; set; }
        public DbSet<SamuraiBattle> SamuraiBattles { get; set; }
        public DbSet<Quote> Quotes { get; set; }
        public DbSet<SecretIdentity> SecretIdentities { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {


            base.OnModelCreating(modelBuilder);

            modelBuilder
                .ApplyConfiguration(new SamuraiConfiguration())
                .ApplyConfiguration(new SamuraiBattleConfiguration())
                .ApplyConfiguration(new SecretIdentityConfiguration())
                .ApplyConfiguration(new QuoteConfiguration());
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseLazyLoadingProxies().UseSqlServer(
                "Server=REDEK-PC;Database=SamuraiAppDataCore;Trusted_Connection=True;");
        }
    }
}
