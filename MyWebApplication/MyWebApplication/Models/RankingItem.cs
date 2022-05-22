using System.ComponentModel.DataAnnotations;

namespace MyWebApplication.Models
{
    public class RankingItem
    {
        public int Id { get; set; }

        [Range(0, 5, ErrorMessage = "Ranking should be between 1 and 5.")]
        [Required]
       
        public int Ranking { get; set; }

        [Required]
        public string Feedback { get; set; }

        [Required]
        public string Name { get; set; }

        public string Time { get; set; }
    }
}
