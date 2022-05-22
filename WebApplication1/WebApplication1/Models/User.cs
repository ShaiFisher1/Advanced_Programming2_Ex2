using System.ComponentModel.DataAnnotations;

namespace WebApplication1
{
    public class User
    {
        [Key]
        public string id { get; set; } // the username

        [Required]
        public string nickname { get; set; } // nickname given in registration

        [DataType(DataType.Password)]
        [Required]
        public string password { get; set; }
    }
}