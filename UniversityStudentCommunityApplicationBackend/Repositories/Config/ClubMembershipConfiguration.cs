using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Repositories.Config
{
    public class ClubMembershipConfiguration : IEntityTypeConfiguration<ClubMembership>
    {
        public void Configure(EntityTypeBuilder<ClubMembership> builder)
        {
            builder.ToTable("ClubMemberships");

            builder.HasKey(cm => cm.Id);

            // Unique constraint: user can join a club once
            builder.HasIndex(cm => new { cm.ClubId, cm.UserId })
                .IsUnique();

            // ⭐ EF Enum -> int map’ler
            builder.Property(cm => cm.Role)
                .IsRequired()
                .HasConversion<int>();

            // Relationships
            builder.HasOne(cm => cm.Club)
                .WithMany(c => c.Memberships)
                .HasForeignKey(cm => cm.ClubId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(cm => cm.User)
                .WithMany()
                .HasForeignKey(cm => cm.UserId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
