using Entities.DTOs;
using Entities.Enums;
using Entities.Exceptions;
using Entities.Models;
using Repositories.Contracts;
using Services.Contracts;

namespace Services
{
    public class ClubEventService : IClubEventService
    {
        private readonly IClubRepository _clubRepository;
        private readonly IClubMembershipRepository _membershipRepository;
        private readonly IClubEventRepository _eventRepository;
        private readonly IUnitOfWork _unitOfWork;

        public ClubEventService(
            IClubRepository clubRepository,
            IClubMembershipRepository membershipRepository,
            IClubEventRepository eventRepository,
            IUnitOfWork unitOfWork)
        {
            _clubRepository = clubRepository;
            _membershipRepository = membershipRepository;
            _eventRepository = eventRepository;
            _unitOfWork = unitOfWork;
        }

        private static void ValidateTimes(DateTime start, DateTime end)
        {
            if (end <= start)
                throw new ArgumentException("Event end time must be after start time.");
        }

        private static ClubEventResponseDto Map(ClubEvent e) => new ClubEventResponseDto
        {
            Id = e.Id,
            ClubId = e.ClubId,
            Title = e.Title,
            Description = e.Description,
            Location = e.Location,
            StartTime = e.StartTime,
            EndTime = e.EndTime
        };

        private async Task<bool> IsManagerOrOfficer(int clubId, string userId)
        {
            var club = await _clubRepository.GetClubByIdAsync(clubId, false)
                       ?? throw new NotFoundException("Club not found.");

            if (club.ManagerId == userId) return true;

            var membership = await _membershipRepository.GetMembershipAsync(clubId, userId, false);
            return membership != null && membership.Role == ClubRole.Officer;
        }

        public async Task<IEnumerable<ClubEventResponseDto>> GetEventsAsync(int clubId)
        {
            var club = await _clubRepository.GetClubByIdAsync(clubId, false)
                       ?? throw new NotFoundException("Club not found.");

            var events = await _eventRepository.GetEventsByClubAsync(club.Id, false);
            return events.Select(Map);
        }

        public async Task<ClubEventResponseDto> CreateEventAsync(int clubId, CreateClubEventDto dto, string currentUserId)
        {
            if (!await IsManagerOrOfficer(clubId, currentUserId))
                throw new UnauthorizedAccessException("Only managers or officers can create events.");

            ValidateTimes(dto.StartTime, dto.EndTime);

            var entity = new ClubEvent
            {
                ClubId = clubId,
                Title = dto.Title,
                Description = dto.Description,
                Location = dto.Location,
                StartTime = dto.StartTime,
                EndTime = dto.EndTime,
                CreatedByUserId = currentUserId
            };

            _eventRepository.CreateEvent(entity);
            await _unitOfWork.SaveChangesAsync();

            return Map(entity);
        }

        public async Task<ClubEventResponseDto> UpdateEventAsync(int clubId, int eventId, UpdateClubEventDto dto, string currentUserId)
        {
            if (!await IsManagerOrOfficer(clubId, currentUserId))
                throw new UnauthorizedAccessException("Only managers or officers can update events.");

            ValidateTimes(dto.StartTime, dto.EndTime);

            var entity = await _eventRepository.GetEventAsync(clubId, eventId, true)
                        ?? throw new NotFoundException("Event not found.");

            entity.Title = dto.Title;
            entity.Description = dto.Description;
            entity.Location = dto.Location;
            entity.StartTime = dto.StartTime;
            entity.EndTime = dto.EndTime;

            _eventRepository.UpdateEvent(entity);
            await _unitOfWork.SaveChangesAsync();

            return Map(entity);
        }

        public async Task DeleteEventAsync(int clubId, int eventId, string currentUserId)
        {
            if (!await IsManagerOrOfficer(clubId, currentUserId))
                throw new UnauthorizedAccessException("Only managers or officers can delete events.");

            var entity = await _eventRepository.GetEventAsync(clubId, eventId, false)
                        ?? throw new NotFoundException("Event not found.");

            _eventRepository.DeleteEvent(entity);
            await _unitOfWork.SaveChangesAsync();
        }
    }
}
