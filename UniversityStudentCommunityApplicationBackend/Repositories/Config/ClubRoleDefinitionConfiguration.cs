using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Repositories.Config
{
    public class ClubRoleDefinitionConfiguration : IEntityTypeConfiguration<ClubRoleDefinition>
    {
        public void Configure(EntityTypeBuilder<ClubRoleDefinition> builder)
        {
            builder.ToTable("ClubRoleDefinitions");

            builder.HasKey(r => r.Id);

            builder.Property(r => r.RoleValue)
                .IsRequired();

            builder.Property(r => r.RoleName)
                .HasMaxLength(100)
                .IsRequired();

            builder.Property(r => r.DisplayName)
                .HasMaxLength(100)
                .IsRequired();

            builder.Property(r => r.Description)
                .HasMaxLength(300);

            // ⭐ Seed (Enum ile birebir, 1-2-3)
            builder.HasData(
                new ClubRoleDefinition
                {
                    Id = 1,
                    RoleValue = 1,
                    RoleName = "Member",
                    DisplayName = "Club Member",
                    Description = "Regular club member."
                },
                new ClubRoleDefinition
                {
                    Id = 2,
                    RoleValue = 2,
                    RoleName = "Officer",
                    DisplayName = "Club Officer",
                    Description = "Club officer with additional permissions."
                },
                new ClubRoleDefinition
                {
                    Id = 3,
                    RoleValue = 3,
                    RoleName = "Manager",
                    DisplayName = "Club Manager",
                    Description = "Full control of the club."
                }
            );
        }
    }
}
