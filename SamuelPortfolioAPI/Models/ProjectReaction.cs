using System.ComponentModel.DataAnnotations;

namespace SamuelPortfolioAPI.Models
{
    public class ProjectReaction
    {
        [Key]
        public int Id { get; set; }
        public int ProjectId { get; set; }
        public bool IsLike { get; set; }
        public int Rating { get; set; }
    }
}
