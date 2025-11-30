using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.DTOs
{
    // ---------------------------------------------------------
    // CREATE
    // ---------------------------------------------------------
    public class CreateClubDto
    {
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; }

        // Admin tarafından atanacak
        public string ManagerUserId { get; set; } = string.Empty;
    }

    // ---------------------------------------------------------
    // UPDATE
    // ---------------------------------------------------------
    public class UpdateClubDto
    {
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; }
    }

    // ---------------------------------------------------------
    // LIST PAGE DTO
    // ---------------------------------------------------------
    public class ClubListDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; }

        public string ManagerId { get; set; } = string.Empty;
        public string ManagerFullName { get; set; } = string.Empty;
    }

    // ---------------------------------------------------------
    // MEMBER DTO
    // ---------------------------------------------------------
    public class ClubMemberDto
    {
        public string UserId { get; set; } = string.Empty;
        public string FullName { get; set; } = string.Empty;
        public string Role { get; set; } = string.Empty; // Member | Officer | Manager
    }

    // ---------------------------------------------------------
    // DETAIL VIEW DTO
    // ---------------------------------------------------------
    public class ClubDetailDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; }

        public string ManagerId { get; set; } = string.Empty;
        public string ManagerFullName { get; set; } = string.Empty;

        public List<ClubMemberDto> Members { get; set; } = new();
    }
}
