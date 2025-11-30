using Entities.Models;

namespace Repositories.Contracts
{
    public interface IClubMembershipRepository
    {
        Task<ClubMembership?> GetMembershipAsync(int clubId, string userId, bool trackChanges);

        Task<IEnumerable<ClubMembership>> GetMembersOfClubAsync(int clubId, bool trackChanges);

        void AddMembership(ClubMembership membership);
        void DeleteMembership(ClubMembership membership);
        void UpdateMembership(ClubMembership membership);
    }
}
