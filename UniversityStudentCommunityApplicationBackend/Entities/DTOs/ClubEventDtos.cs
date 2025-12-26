using System.ComponentModel.DataAnnotations;

namespace Entities.DTOs
{
    public class CreateClubEventDto
    {
        [Required]
        [MaxLength(200)]
        public string Title { get; set; } = string.Empty;

        [MaxLength(2000)]
        public string? Description { get; set; }

        [MaxLength(300)]
        public string? Location { get; set; }

        [Required]
        public DateTime StartTime { get; set; }

        [Required]
        public DateTime EndTime { get; set; }
    }

    public class UpdateClubEventDto : CreateClubEventDto
    {
    }

    public class ClubEventResponseDto
    {
        public int Id { get; set; }
        public int ClubId { get; set; }
        public string Title { get; set; } = string.Empty;
        public string? Description { get; set; }
        public string? Location { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
    }
}
