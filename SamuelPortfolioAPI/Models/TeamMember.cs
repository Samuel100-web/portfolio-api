using System.ComponentModel.DataAnnotations;

namespace SamuelPortfolioAPI.Models
{
    public class TeamMember
    {
        public int Id { get; set; }

        [Required]
        public string FullName { get; set; }

        [Required]
        public string Role { get; set; } // e.g., "Frontend Developer", "UI/UX Designer"

        public string Bio { get; set; } // short description (optional)

        public string ImageUrl { get; set; } // frontend ke liye image path

        public string LinkedInUrl { get; set; }
        public string GitHubUrl { get; set; }
        [Phone]
        public string Mobile { get; set; }

        public bool IsActive { get; set; } = true;
    }
}
