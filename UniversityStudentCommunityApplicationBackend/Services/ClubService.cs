using Entities.DTOs;
using Entities.Enums;
using Entities.Exceptions;
using Entities.Models;
using Microsoft.AspNetCore.Identity;
using Repositories.Contracts;
using Services.Contracts;

namespace Services
{
    public class ClubService : IClubService
    {
        private readonly IClubRepository _clubRepository;
        private readonly IClubMembershipRepository _membershipRepository;
        private readonly UserManager<User> _userManager;
        private readonly IUnitOfWork _unitOfWork;

        public ClubService(
            IClubRepository clubRepository,
            IClubMembershipRepository membershipRepository,
            UserManager<User> userManager,
            IUnitOfWork unitOfWork)
        {
            _clubRepository = clubRepository;
            _membershipRepository = membershipRepository;
            _userManager = userManager;
            _unitOfWork = unitOfWork;
        }

        // -------------------------------------------------------------
        // 1) CREATE CLUB
        // -------------------------------------------------------------
        public async Task<ClubResponseDto> CreateClubAsync(CreateClubDto dto)
        {
            var manager = await _userManager.FindByIdAsync(dto.ManagerUserId);
            if (manager == null)
                throw new NotFoundException("Manager user not found.");

            var club = new Club
            {
                Name = dto.Name,
                Description = dto.Description,
                ManagerId = dto.ManagerUserId
            };

            _clubRepository.CreateClub(club);
            await _unitOfWork.SaveChangesAsync();

            // Manager membership
            var membership = new ClubMembership
            {
                ClubId = club.Id,
                UserId = dto.ManagerUserId,
                Role = ClubRole.Manager
            };

            _membershipRepository.AddMembership(membership);
            await _unitOfWork.SaveChangesAsync();

            return new ClubResponseDto
            {
                Id = club.Id,
                Name = club.Name,
                Description = club.Description,
                ManagerId = manager.Id,
                ManagerFullName = manager.FullName
            };
        }

        // -------------------------------------------------------------
        // 2) GET CLUB BY ID
        // -------------------------------------------------------------
        public async Task<ClubResponseDto> GetClubByIdAsync(int id)
        {
            var club = await _clubRepository.GetClubWithMembersAsync(id, false);
            if (club == null)
                throw new NotFoundException("Club not found.");

            var members = club.Memberships.Select(m => new ClubMemberDto
            {
                UserId = m.UserId,
                FullName = $"{m.User.FirstName} {m.User.LastName}",
                Role = m.Role.ToString()
            }).ToList();

            return new ClubResponseDto
            {
                Id = club.Id,
                Name = club.Name,
                Description = club.Description,
                ManagerId = club.ManagerId,
                ManagerFullName = club.Manager.FullName
            };
        }

        // -------------------------------------------------------------
        // 3) GET ALL CLUBS
        // -------------------------------------------------------------
        public async Task<IEnumerable<ClubResponseDto>> GetAllClubsAsync()
        {
            var clubs = await _clubRepository.GetAllClubsAsync(false);

            return clubs.Select(c => new ClubResponseDto
            {
                Id = c.Id,
                Name = c.Name,
                Description = c.Description,
                ManagerId = c.ManagerId,
                ManagerFullName = c.Manager.FullName
            });
        }

        // -------------------------------------------------------------
        // 4) UPDATE CLUB
        // -------------------------------------------------------------
        public async Task UpdateClubAsync(int id, UpdateClubDto dto)
        {
            var club = await _clubRepository.GetClubByIdAsync(id, true);
            if (club == null)
                throw new NotFoundException("Club not found.");

            club.Name = dto.Name;
            club.Description = dto.Description;

            _clubRepository.UpdateClub(club);
            await _unitOfWork.SaveChangesAsync();
        }

        // -------------------------------------------------------------
        // 5) DELETE CLUB
        // -------------------------------------------------------------
        public async Task DeleteClubAsync(int id)
        {
            var club = await _clubRepository.GetClubByIdAsync(id, true);
            if (club == null)
                throw new NotFoundException("Club not found.");

            _clubRepository.DeleteClub(club);
            await _unitOfWork.SaveChangesAsync();
        }
    }
}
