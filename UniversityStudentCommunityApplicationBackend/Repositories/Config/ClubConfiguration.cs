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

            builder.Property(c => c.ManagerId)
                   .IsRequired();

            // Manager (User) → Club (1:N)
            builder.HasOne(c => c.Manager)
                   .WithMany()                    // User tarafında koleksiyon tutmuyoruz
                   .HasForeignKey(c => c.ManagerId)
                   .OnDelete(DeleteBehavior.Restrict);
            // Restrict → Manager silinirse Club silinmez ve hata verir

            // ClubMembership ilişkisi
            builder.HasMany(c => c.Memberships)
                   .WithOne(cm => cm.Club)
                   .HasForeignKey(cm => cm.ClubId)
                   .OnDelete(DeleteBehavior.Cascade);
            // Club silinirse tüm üyelikler silinsin
        }
    }
}
