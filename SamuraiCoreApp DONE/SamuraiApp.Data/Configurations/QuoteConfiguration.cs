using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SamuraiApp.Domain;

namespace SamuraiApp.Data.Configurations
{
    public class QuoteConfiguration : IEntityTypeConfiguration<Quote>
    {
        public void Configure(EntityTypeBuilder<Quote> builder)
        {
            builder
                .ToTable("Quotes");

            builder
                .HasKey(s => new { s.Id, s.SamuraiId });

            builder
                .HasOne(q => q.Samurai)
                .WithMany(s => s.Quotes)
                .HasForeignKey(q => q.SamuraiId)
                .OnDelete(DeleteBehavior.Cascade);

        }
    }
}
