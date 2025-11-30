using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Repositories.Contracts;

namespace Repositories.EFCore
{
    public class ClubMembershipRepository : IClubMembershipRepository
    {
        private readonly RepositoryContext _context;

        public ClubMembershipRepository(RepositoryContext context)
        {
            _context = context;
        }

        public async Task<ClubMembership?> GetMembershipAsync(int clubId, string userId, bool trackChanges)
        {
            return await (trackChanges
                ? _context.ClubMemberships
                : _context.ClubMemberships.AsNoTracking())
                .FirstOrDefaultAsync(m => m.ClubId == clubId && m.UserId == userId);
        }

        public async Task<IEnumerable<ClubMembership>> GetMembersOfClubAsync(int clubId, bool trackChanges)
        {
            return await (trackChanges
                ? _context.ClubMemberships.Include(m => m.User)
                : _context.ClubMemberships.Include(m => m.User).AsNoTracking())
                .Where(m => m.ClubId == clubId)
                .ToListAsync();
        }

        public void AddMembership(ClubMembership membership) => _context.ClubMemberships.Add(membership);

        public void DeleteMembership(ClubMembership membership) => _context.ClubMemberships.Remove(membership);

        public void UpdateMembership(ClubMembership membership) => _context.ClubMemberships.Update(membership);
    }
}
