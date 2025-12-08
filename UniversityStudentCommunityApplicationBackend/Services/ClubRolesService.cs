using Entities.DTOs;
using Entities.Enums;
using Entities.Exceptions;
using Repositories.Contracts;
using Services.Contracts;
using System.IdentityModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities.Models;

namespace Services
{
    public class ClubRolesService : IClubRolesService
    {
        private readonly IClubRepository _clubRepo;
        private readonly IClubMembershipRepository _membershipRepo;
        private readonly IUnitOfWork _unitOfWork;

        public ClubRolesService(
            IClubRepository clubRepo,
            IClubMembershipRepository membershipRepo,
            IUnitOfWork unitOfWork)
        {
            _clubRepo = clubRepo;
            _membershipRepo = membershipRepo;
            _unitOfWork = unitOfWork;
        }

        // -----------------------
        // GET USER CLUB ROLE
        // -----------------------
        public async Task<UserClubRoleDto> GetUserClubRoleAsync(int clubId, string userId)
        {
            var club = await _clubRepo.GetClubByIdAsync(clubId, false)
                       ?? throw new NotFoundException("Club not found");

            // Kullanıcı manager mı kontrol et
            if (club.ManagerId == userId)
            {
                return new UserClubRoleDto { ClubRole = ClubRole.Manager.ToString() };
            }

            // ClubMembership'ten rolü al
            var membership = await _membershipRepo.GetMembershipAsync(clubId, userId, false);

            if (membership == null)
            {
                return new UserClubRoleDto { ClubRole = "None" }; // Kulüp üyesi değil
            }

            return new UserClubRoleDto { ClubRole = membership.Role.ToString() };
        }

        public async Task MakeMemberOfficerAsync(int clubId, string userId, string currentUserId)
        {
            UserClubRoleDto userRole;
            try {
                userRole = await GetUserClubRoleAsync(clubId, userId);
            }
            catch(NotFoundException)
            {
                throw;
            }

            if (userRole == null || userRole.ClubRole == "None") { 
                throw new NotFoundException("User is not a member of the club.");
            }
            if (userRole.ClubRole != ClubRole.Member.ToString())
            {
                throw new InvalidOperationException("Only members can be promoted to officers.");
            }
            ClubMembership membership = await _membershipRepo.GetMembershipAsync(clubId, userId, false)!;
            membership.Role = ClubRole.Officer;
            _membershipRepo.UpdateMembership(membership);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task DemoteOfficerAsync(int clubId, string userId, string currentUserId)
        {
            UserClubRoleDto userRole;
            try {
                userRole = await GetUserClubRoleAsync(clubId, userId);
            }
            catch(NotFoundException)
            {
                throw;
            }
            if (userRole == null || userRole.ClubRole == "None")
            {
                throw new NotFoundException("User is not a member of the club.");
            }
            if (userRole.ClubRole != ClubRole.Officer.ToString())
            {
                throw new InvalidOperationException("User is not an officer.");
            }
            ClubMembership membership = await _membershipRepo.GetMembershipAsync(clubId, userId, false)!;
            membership.Role = ClubRole.Member;
            _membershipRepo.UpdateMembership(membership);
            await _unitOfWork.SaveChangesAsync();
        }
    }
}
