using Entities.DTOs;
using Entities.Models;

namespace Services.Contracts
{
    public interface IClubService
    {
        Task<ClubDetailDto> CreateClubAsync(CreateClubDto dto);
        Task<IEnumerable<ClubListDto>> GetAllClubsAsync();
        Task<ClubDetailDto> GetClubByIdAsync(int id);
        Task UpdateClubAsync(int id, UpdateClubDto dto);
        Task DeleteClubAsync(int id);
    }
}
