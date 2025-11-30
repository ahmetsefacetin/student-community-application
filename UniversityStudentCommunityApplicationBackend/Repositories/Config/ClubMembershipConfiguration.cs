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

            // Unique constraint: a user can join a club only once
            builder.HasIndex(cm => new { cm.ClubId, cm.UserId })
                   .IsUnique();

            // Store enum as string (IMPORTANT)
            builder.Property(cm => cm.Role)
                   .HasConversion<string>()     // <— İŞTE EN ÖNEMLİ KISIM
                   .IsRequired();

            builder.Property(cm => cm.JoinedAt)
                   .IsRequired()
                   .HasDefaultValueSql("GETUTCDATE()");

            // Relationship: ClubMembership → Club (N:1)
            builder.HasOne(cm => cm.Club)
                   .WithMany(c => c.Memberships)
                   .HasForeignKey(cm => cm.ClubId)
                   .OnDelete(DeleteBehavior.Cascade);

            // Relationship: ClubMembership → User (N:1)
            builder.HasOne(cm => cm.User)
                   .WithMany() // User → Memberships koleksiyonuna ihtiyaç yoksa
                   .HasForeignKey(cm => cm.UserId)
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
