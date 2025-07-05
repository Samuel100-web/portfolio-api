using System.ComponentModel.DataAnnotations;

namespace SamuelPortfolioAPI.Models
{
    public class Skill
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Level { get; set; } // Beginner, Intermediate, Expert

    }
}
