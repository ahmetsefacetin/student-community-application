using Entities.Models;

namespace Repositories.Contracts
{
    public interface IClubEventRepository
    {
        Task<IEnumerable<ClubEvent>> GetEventsByClubAsync(int clubId, bool trackChanges);
        Task<ClubEvent?> GetEventAsync(int clubId, int eventId, bool trackChanges);
        void CreateEvent(ClubEvent clubEvent);
        void UpdateEvent(ClubEvent clubEvent);
        void DeleteEvent(ClubEvent clubEvent);
    }
}
