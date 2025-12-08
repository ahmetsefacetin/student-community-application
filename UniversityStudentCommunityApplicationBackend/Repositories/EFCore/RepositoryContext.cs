using Entities.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Repositories.Config;
using System.Reflection.Emit;

namespace Repositories.EFCore
{
    public class RepositoryContext : IdentityDbContext<User>
    {
        public RepositoryContext(DbContextOptions<RepositoryContext> options) : base(options) { }

        public DbSet<SystemMessage> SystemMessages => Set<SystemMessage>();
        public DbSet<RefreshToken> RefreshTokens => Set<RefreshToken>();
        public DbSet<Club> Clubs => Set<Club>();
        public DbSet<ClubMembership> ClubMemberships => Set<ClubMembership>();
        public DbSet<ClubRoleDefinition> ClubRoleDefinitions => Set<ClubRoleDefinition>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfiguration(new SystemMessageConfiguration());
            modelBuilder.ApplyConfiguration(new UserConfiguration());
            modelBuilder.ApplyConfiguration(new RoleConfiguration());
            modelBuilder.ApplyConfiguration(new RefreshTokenConfiguration());
            modelBuilder.ApplyConfiguration(new ClubConfiguration());
            modelBuilder.ApplyConfiguration(new ClubMembershipConfiguration());
            modelBuilder.ApplyConfiguration(new ClubRoleDefinitionConfiguration());
            
            // Admin seed configuration'larını ekle
            modelBuilder.ApplyConfiguration(new AdminUserSeedConfiguration());
            modelBuilder.ApplyConfiguration(new AdminRoleAssignmentConfiguration());

            // Club member seed configuration'larını ekle
            modelBuilder.ApplyConfiguration(new ClubMemberSeedConfiguration());
        }
    }
}
