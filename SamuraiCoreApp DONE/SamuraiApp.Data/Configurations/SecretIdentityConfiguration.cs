using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SamuraiApp.Domain;

namespace SamuraiApp.Data.Configurations
{
    public class SecretIdentityConfiguration : IEntityTypeConfiguration<SecretIdentity>
    {
        public void Configure(EntityTypeBuilder<SecretIdentity> builder)
        {
            builder
                .ToTable("SecretIdentities")
                .HasKey(si => si.Id);

            builder
                .HasOne(si => si.Samurai)
                .WithOne(s => s.SecretIdentity)
                .HasForeignKey<SecretIdentity>(si => si.SamuraiId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
