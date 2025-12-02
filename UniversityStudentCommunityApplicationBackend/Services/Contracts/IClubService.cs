using Entities.DTOs;
using Entities.Models;

namespace Services.Contracts
{
    public interface IClubService
    {
        Task<ClubResponseDto> CreateClubAsync(CreateClubDto dto);
        Task<IEnumerable<ClubResponseDto>> GetAllClubsAsync();
        Task<ClubResponseDto> GetClubByIdAsync(int id);
        Task UpdateClubAsync(int id, UpdateClubDto dto);
        Task DeleteClubAsync(int id);
    }
}
