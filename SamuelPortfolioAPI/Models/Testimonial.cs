using System.ComponentModel.DataAnnotations;

namespace SamuelPortfolioAPI.Models
{
    public class Testimonial
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Feedback { get; set; }
        public string Designation { get; set; }
        public string ProfileImageUrl { get; set; }
    }
}
