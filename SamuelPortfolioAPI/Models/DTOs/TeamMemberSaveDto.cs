using System.ComponentModel.DataAnnotations;

namespace SamuelPortfolioAPI.Models.DTOs
{
    public class TeamMemberSaveDto
    {
        public int Id { get; set; } // 0 = Create, >0 = Update

        [Required]
        public string FullName { get; set; }

        [Required]
        public string Role { get; set; }

        public string Bio { get; set; }

        public string LinkedInUrl { get; set; }

        public string GitHubUrl { get; set; }

        [Phone]
        public string Mobile { get; set; }

    }
}
