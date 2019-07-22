using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SamuraiApp.Domain;

namespace SamuraiApp.Data.Configurations
{
    public class SamuraiBattleConfiguration : IEntityTypeConfiguration<SamuraiBattle>
    {
        public void Configure(EntityTypeBuilder<SamuraiBattle> builder)
        {
            builder
                .ToTable("SamuraiBattles");

            builder
                .HasKey(s => new { s.BattleId, s.SamuraiId });

            builder
                .Property(sb => sb.KillStreak)
                .IsRequired();

            builder
                .HasOne(sb => sb.Battle)
                .WithMany(b => b.SamuraiBattles)
                .HasForeignKey(sb => new { sb.BattleId })
                .OnDelete(DeleteBehavior.Cascade);

            builder
                .HasOne(sb => sb.Samurai)
                .WithMany(s => s.SamuraiBattles)
                .HasForeignKey(sb => new { sb.SamuraiId })
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}