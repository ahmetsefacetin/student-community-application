using Entities.DTOs;

namespace Services.Contracts
{
    public interface IClubEventService
    {
        Task<IEnumerable<ClubEventResponseDto>> GetEventsAsync(int clubId);
        Task<ClubEventResponseDto> CreateEventAsync(int clubId, CreateClubEventDto dto, string currentUserId);
        Task<ClubEventResponseDto> UpdateEventAsync(int clubId, int eventId, UpdateClubEventDto dto, string currentUserId);
        Task DeleteEventAsync(int clubId, int eventId, string currentUserId);
    }
}
