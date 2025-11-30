using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Repositories.Config
{
    public class ClubConfiguration : IEntityTypeConfiguration<Club>
    {
        public void Configure(EntityTypeBuilder<Club> builder)
        {
            builder.ToTable("Clubs");

            builder.HasKey(c => c.Id);

            builder.Property(c => c.Name)
                   .IsRequired()
                   .HasMaxLength(100);

            builder.Property(c => c.Description)
                   .HasMaxLength(500);

            // Club.ManagerId → User (1:N)
            builder.HasOne(c => c.Manager)
                   .WithMany() // Managers do not need navigation for all clubs
                   .HasForeignKey(c => c.ManagerId)
                   .OnDelete(DeleteBehavior.Restrict);
            // Restrict: Manager silinirse kulübe dokunmasın!
        }
    }
}
