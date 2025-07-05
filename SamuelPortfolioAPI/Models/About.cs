using System.ComponentModel.DataAnnotations;

namespace SamuelPortfolioAPI.Models
{
    public class About
    {
        [Key]
        public int Id { get; set; }
        public string FullName { get; set; }
        public string Bio { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string ProfileImageUrl { get; set; }
    }
}
