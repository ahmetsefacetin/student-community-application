using Entities.DTOs;

namespace Services.Contracts
{
    public interface IClubMembershipService
    {
        Task AddMemberAsync(AddMemberDto dto);
        Task RemoveMemberAsync(RemoveMemberDto dto);
        Task UpdateMemberRoleAsync(UpdateMemberRoleDto dto);
        Task<IEnumerable<ClubMemberResponseDto>> GetMembersAsync(int clubId);
        Task JoinClubAsync(int clubId, string userId);
        Task LeaveClubAsync(int clubId, string userId);

    }
}
