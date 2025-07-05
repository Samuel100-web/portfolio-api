namespace SamuelPortfolioAPI.Models.DTOs
{
    public class TeamMemberDto
    {
        public int Id { get; set; }

        public string FullName { get; set; }

        public string Role { get; set; }

        public string Bio { get; set; }

        public string ImageUrl { get; set; }

        public string LinkedInUrl { get; set; }

        public string GitHubUrl { get; set; }

        public string Mobile { get; set; }

        public bool IsActive { get; set; }
    }
}
