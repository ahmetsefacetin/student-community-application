using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Repositories.Config
{
    public class ClubEventConfiguration : IEntityTypeConfiguration<ClubEvent>
    {
        public void Configure(EntityTypeBuilder<ClubEvent> builder)
        {
            builder.ToTable("ClubEvents");

            builder.HasKey(e => e.Id);

            builder.Property(e => e.Title)
                   .IsRequired()
                   .HasMaxLength(200);

            builder.Property(e => e.Description)
                   .HasMaxLength(2000);

            builder.Property(e => e.Location)
                   .HasMaxLength(300);

            builder.Property(e => e.StartTime)
                   .IsRequired();

            builder.Property(e => e.EndTime)
                   .IsRequired();

            builder.Property(e => e.CreatedByUserId)
                   .IsRequired();

            builder.HasOne(e => e.Club)
                   .WithMany(c => c.Events)
                   .HasForeignKey(e => e.ClubId)
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
