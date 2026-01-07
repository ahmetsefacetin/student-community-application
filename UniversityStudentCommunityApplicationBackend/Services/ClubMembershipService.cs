using Entities.DTOs;
using Entities.Enums;
using Entities.Exceptions;
using Entities.Models;
using Repositories.Contracts;
using Services.Contracts;

namespace Services
{
    public class ClubMembershipService : IClubMembershipService
    {
        private readonly IClubRepository _clubRepo;
        private readonly IClubMembershipRepository _membershipRepo;
        private readonly IUnitOfWork _unitOfWork;

        public ClubMembershipService(
            IClubRepository clubRepo,
            IClubMembershipRepository membershipRepo,
            IUnitOfWork unitOfWork)
        {
            _clubRepo = clubRepo;
            _membershipRepo = membershipRepo;
            _unitOfWork = unitOfWork;
        }

        // -----------------------
        // ADD MEMBER
        // -----------------------
        public async Task AddMemberAsync(AddMemberDto dto)
        {
            var club = await _clubRepo.GetClubByIdAsync(dto.ClubId, false)
                       ?? throw new NotFoundException("Club not found");

            // Manager kulübe tekrar eklenmesin
            if (club.ManagerId == dto.UserId)
                throw new ArgumentException("The manager is already part of the club.");

            var existing = await _membershipRepo.GetMembershipAsync(dto.ClubId, dto.UserId, false);
            if (existing != null)
                throw new ArgumentException("This user is already a member of the club.");

            var newMembership = new ClubMembership
            {
                ClubId = dto.ClubId,
                UserId = dto.UserId,
                Role = ClubRole.Member
            };

            _membershipRepo.AddMembership(newMembership);
            await _unitOfWork.SaveChangesAsync();
        }

        // -----------------------
        // REMOVE MEMBER
        // -----------------------
        public async Task RemoveMemberAsync(RemoveMemberDto dto)
        {
            var club = await _clubRepo.GetClubByIdAsync(dto.ClubId, false)
                       ?? throw new NotFoundException("Club not found");

            // Manager silinemez
            if (club.ManagerId == dto.UserId)
                throw new ArgumentException("You cannot remove the manager from the club.");

            var membership = await _membershipRepo.GetMembershipAsync(dto.ClubId, dto.UserId, true)
                             ?? throw new NotFoundException("Membership not found");

            _membershipRepo.DeleteMembership(membership);
            await _unitOfWork.SaveChangesAsync();
        }

        // -----------------------
        // UPDATE ROLE
        // -----------------------
        public async Task UpdateMemberRoleAsync(UpdateMemberRoleDto dto)
        {
            var club = await _clubRepo.GetClubByIdAsync(dto.ClubId, false)
                       ?? throw new NotFoundException("Club not found");

            // Yöneticinin rolü değiştirilemez
            if (club.ManagerId == dto.UserId)
                throw new ArgumentException("Manager role cannot be changed.");

            var membership = await _membershipRepo.GetMembershipAsync(dto.ClubId, dto.UserId, true)
                             ?? throw new NotFoundException("Membership not found");

            membership.Role = dto.NewRole;

            _membershipRepo.UpdateMembership(membership);
            await _unitOfWork.SaveChangesAsync();
        }

        // -----------------------
        // LIST MEMBERS
        // -----------------------
        public async Task<IEnumerable<ClubMemberResponseDto>> GetMembersAsync(int clubId)
        {
            var club = await _clubRepo.GetClubByIdAsync(clubId, false)
                       ?? throw new NotFoundException("Club not found");

            var members = await _membershipRepo.GetMembersOfClubAsync(clubId, false);

            var mapped = members.Select(m => new ClubMemberResponseDto
            {
                UserId = m.UserId,
                FullName = m.User.FullName,
                Role = m.Role,
                JoinedAt = m.JoinedAt
            }).ToList();

            return mapped;
        }

        // -----------------------
        // SELF JOIN / LEAVE
        // -----------------------
        public async Task JoinClubAsync(int clubId, string userId)
        {
            await AddMemberAsync(new AddMemberDto
            {
                ClubId = clubId,
                UserId = userId
            });
        }

        public async Task LeaveClubAsync(int clubId, string userId)
        {
            await RemoveMemberAsync(new RemoveMemberDto
            {
                ClubId = clubId,
                UserId = userId
            });
        }

    }
}
