using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Repositories.Contracts;

namespace Repositories.EFCore
{
    public class ClubEventRepository : IClubEventRepository
    {
        private readonly RepositoryContext _context;

        public ClubEventRepository(RepositoryContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<ClubEvent>> GetEventsByClubAsync(int clubId, bool trackChanges)
        {
            var query = _context.ClubEvents.Where(e => e.ClubId == clubId)
                                           .OrderBy(e => e.StartTime);
            return trackChanges ? await query.ToListAsync()
                                : await query.AsNoTracking().ToListAsync();
        }

        public async Task<ClubEvent?> GetEventAsync(int clubId, int eventId, bool trackChanges)
        {
            var query = _context.ClubEvents.Where(e => e.ClubId == clubId && e.Id == eventId);
            return trackChanges ? await query.FirstOrDefaultAsync()
                                : await query.AsNoTracking().FirstOrDefaultAsync();
        }

        public void CreateEvent(ClubEvent clubEvent) => _context.ClubEvents.Add(clubEvent);

        public void UpdateEvent(ClubEvent clubEvent) => _context.ClubEvents.Update(clubEvent);

        public void DeleteEvent(ClubEvent clubEvent) => _context.ClubEvents.Remove(clubEvent);
    }
}
