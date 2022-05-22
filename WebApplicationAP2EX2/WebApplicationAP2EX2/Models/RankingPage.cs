using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

{

}

namespace WebApplication.Models
{
    public class RankingPage
    {

        public int Id { get; set; }

    
        [Range(0, 5)]
        [Required]
        public int Ranking { get; set; }

        [Required]
        public string Text { get; set; }

        [Required]
        public string Name { get; set; }

        [DataType(DataType.DateTime)]
        public string Time { get; set; }
    }
}
