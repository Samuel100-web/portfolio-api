using System.ComponentModel.DataAnnotations;

namespace SamuelPortfolioAPI.Models
{
    public class Education
    {
        [Key]
        public int Id { get; set; }
        public string Degree { get; set; }
        public string Institute { get; set; }
        public string Year { get; set; }
        public string Grade { get; set; }
    }
}
