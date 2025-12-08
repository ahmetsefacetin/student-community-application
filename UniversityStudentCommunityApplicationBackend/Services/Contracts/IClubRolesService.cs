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
        Task MakeMemberOfficerAsync(OfficerActionsDto officerActionsDto);
        Task DemoteOfficerAsync(OfficerActionsDto officerActionsDto, string currentUserId);

    }
}
