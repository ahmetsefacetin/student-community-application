using Entities.Models;

namespace Repositories.Contracts
{
    public interface IClubRepository
    {
        Task<IEnumerable<Club>> GetAllClubsAsync(bool trackChanges);
        Task<Club?> GetClubByIdAsync(int id, bool trackChanges);
        Task<Club?> GetClubWithMembersAsync(int id, bool trackChanges);
        void CreateClub(Club club);
        void DeleteClub(Club club);
        void UpdateClub(Club club);
    }
}
