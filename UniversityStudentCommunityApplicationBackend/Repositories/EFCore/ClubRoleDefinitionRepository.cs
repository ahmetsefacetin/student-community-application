using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Repositories.Contracts;

namespace Repositories.EFCore
{
    public class ClubRoleDefinitionRepository : IClubRoleDefinitionRepository
    {
        private readonly RepositoryContext _context;

        public ClubRoleDefinitionRepository(RepositoryContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<ClubRoleDefinition>> GetAllAsync(bool trackChanges)
        {
            return await (trackChanges
                ? _context.ClubRoleDefinitions
                : _context.ClubRoleDefinitions.AsNoTracking())
                .ToListAsync();
        }

        public async Task<ClubRoleDefinition?> GetByRoleValueAsync(int roleValue, bool trackChanges)
        {
            return await (trackChanges
                ? _context.ClubRoleDefinitions
                : _context.ClubRoleDefinitions.AsNoTracking())
                .FirstOrDefaultAsync(r => r.RoleValue == roleValue);
        }
    }
}
