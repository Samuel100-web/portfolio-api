using System.ComponentModel.DataAnnotations;

namespace SamuelPortfolioAPI.Models
{
    public class Project
    {
        [Key]
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string ImageUrl { get; set; }
        public string GithubLink { get; set; }
        public string LiveLink { get; set; }
        public string TechStack { get; set; }

    }
}
