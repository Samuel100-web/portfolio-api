using System.ComponentModel.DataAnnotations;

namespace SamuelPortfolioAPI.Models
{
    public class Contact
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string Message { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
