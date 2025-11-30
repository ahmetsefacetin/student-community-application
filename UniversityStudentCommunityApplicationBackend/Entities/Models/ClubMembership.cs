using Entities.Enums;

namespace Entities.Models
{
    public class ClubMembership
    {
        public int Id { get; set; }

        public int ClubId { get; set; }
        public Club Club { get; set; } = null!;

        public string UserId { get; set; } = string.Empty;
        public User User { get; set; } = null!;

        public ClubRole Role { get; set; } = ClubRole.Member;

        public DateTime JoinedAt { get; set; } = DateTime.UtcNow;
    }
}
