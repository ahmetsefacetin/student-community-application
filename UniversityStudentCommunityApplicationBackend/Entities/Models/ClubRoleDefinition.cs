namespace Entities.Models
{
    public class ClubRoleDefinition
    {
        public int Id { get; set; }
        public int RoleValue { get; set; }      // 0,1,2
        public string RoleName { get; set; } = string.Empty; // "Member"
        public string DisplayName { get; set; } = string.Empty; // "Club Member"
        public string? Description { get; set; }
    }
}
