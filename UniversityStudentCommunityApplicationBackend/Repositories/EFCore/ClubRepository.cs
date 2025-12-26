using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Repositories.Contracts;

namespace Repositories.EFCore
{
    public class ClubRepository : IClubRepository
    {
        private readonly RepositoryContext _context;

        public ClubRepository(RepositoryContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Club>> GetAllClubsAsync(bool trackChanges)
        {
            return await (trackChanges
                ? _context.Clubs
                : _context.Clubs.AsNoTracking())
                .ToListAsync();
        }

        public async Task<Club?> GetClubByIdAsync(int id, bool trackChanges)
        {
            return await (trackChanges
                ? _context.Clubs.Include(c => c.Manager)
                : _context.Clubs.Include(c => c.Manager).AsNoTracking())
                .FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<Club?> GetClubWithMembersAsync(int id, bool trackChanges)
        {
            return await (trackChanges
                ? _context.Clubs.Include(c => c.Manager).Include(c => c.Memberships)
                : _context.Clubs.Include(c => c.Manager).Include(c => c.Memberships).AsNoTracking())
                .FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<Club?> GetClubWithEventsAsync(int id, bool trackChanges)
        {
            return await (trackChanges
                ? _context.Clubs.Include(c => c.Events)
                : _context.Clubs.Include(c => c.Events).AsNoTracking())
                .FirstOrDefaultAsync(c => c.Id == id);
        }

        public void CreateClub(Club club) => _context.Clubs.Add(club);

        public void DeleteClub(Club club) => _context.Clubs.Remove(club);

        public void UpdateClub(Club club) => _context.Clubs.Update(club);
    }
}
