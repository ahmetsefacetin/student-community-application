using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Repositories.Config
{
    public class AdminRoleAssignmentConfiguration : IEntityTypeConfiguration<IdentityUserRole<string>>
    {
        public void Configure(EntityTypeBuilder<IdentityUserRole<string>> builder)
        {
            // Admin kullanýcýsýna Admin rolü ata
            builder.HasData(
                new IdentityUserRole<string>
                {
                    UserId = "admin-seed-001", // AdminUserSeedConfiguration'daki Id ile ayný olmalý
                    RoleId = "1" // RoleConfiguration'daki Admin role Id'si
                }
            );
        }
    }
}