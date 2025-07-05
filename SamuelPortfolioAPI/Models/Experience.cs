using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace SamuelPortfolioAPI.Models
{
    public class Experience
    {
        [Key]
        public int Id { get; set; }
        public string Company { get; set; }
        public string Role { get; set; }
        [JsonPropertyName("from")]
        public DateTime FromDate { get; set; }
        [JsonPropertyName("to")]
        public DateTime? ToDate { get; set; }
        public string Description { get; set; }
    }
}
