namespace Entities.Models
{
    public class Club
    {
        public int Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public string? Description { get; set; }

        // Manager
        public string ManagerId { get; set; } = string.Empty;
        public User Manager { get; set; } = null!;

        // Memberships
        public ICollection<ClubMembership> Memberships { get; set; } = new List<ClubMembership>();

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
