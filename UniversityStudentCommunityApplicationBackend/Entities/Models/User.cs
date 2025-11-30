using Microsoft.AspNetCore.Identity;

namespace Entities.Models
{
    public class User : IdentityUser
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public DateTime CreatedAt { get; set; }
        public ICollection<RefreshToken>? RefreshTokens { get; set; }

        // Add this computed property to fix the error
        public string FullName => $"{FirstName} {LastName}".Trim();
    }
}
