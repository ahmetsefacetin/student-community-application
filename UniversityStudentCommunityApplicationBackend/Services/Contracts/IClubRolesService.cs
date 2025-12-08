using Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Contracts
{
    public interface IClubRolesService
    {
        Task<UserClubRoleDto> GetUserClubRoleAsync(int clubId, string userId);
        Task MakeMemberOfficerAsync(int clubId, string userId, string currentUserId);
        Task DemoteOfficerAsync(int clubId, string userId, string currentUserId);

    }
}
