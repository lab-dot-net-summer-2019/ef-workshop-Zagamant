using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SamuraiApp.Domain;

namespace SamuraiApp.Data.Configurations
{
    public class SamuraiConfiguration : IEntityTypeConfiguration<Samurai>
    {
        public void Configure(EntityTypeBuilder<Samurai> builder)
        {
            builder
                .ToTable("Samurais")
                .HasKey(s => new {s.Id});

            builder
                .HasMany(s => s.Quotes)
                .WithOne(q => q.Samurai)
                .HasForeignKey(b => b.SamuraiId)
                .OnDelete(DeleteBehavior.Cascade);

            builder
                .HasMany(s => s.SamuraiBattles)
                .WithOne(sb => sb.Samurai)
                .HasForeignKey(sb => sb.SamuraiId)
                .OnDelete(DeleteBehavior.Cascade);

            builder
                .HasOne(se => se.SecretIdentity)
                .WithOne()
                .HasForeignKey<SecretIdentity>(se =>se.SamuraiId);
        }
    }
}
