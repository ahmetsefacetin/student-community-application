using Entities.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.DTOs
{
    public class AddMemberDto
    {
        public int ClubId { get; set; }
        public string UserId { get; set; } = string.Empty;
    }

    public class UpdateMemberRoleDto
    {
        public int ClubId { get; set; }
        public string UserId { get; set; } = string.Empty;
        public ClubRole NewRole { get; set; }
    }

    public class RemoveMemberDto
    {
        public int ClubId { get; set; }
        public string UserId { get; set; } = string.Empty;
    }

    public class ClubMemberResponseDto
    {
        public string UserId { get; set; } = string.Empty;
        public string FullName { get; set; } = string.Empty;
        public ClubRole Role { get; set; }
        public DateTime JoinedAt { get; set; }
    }

    public class UserClubRoleDto
    {
        public string ClubRole { get; set; } = string.Empty;
    }
}
