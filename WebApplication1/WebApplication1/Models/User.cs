using System.ComponentModel.DataAnnotations;

namespace WebApplication1
{
    public class User
    {
        [Key]
        public string UserName { get; set; }

        [Required]
        public string NickName { get; set; } // nickname given in registration

        [DataType(DataType.Password)]
        [Required]
        public string Password { get; set; }

        [Required]
        public string? User_Photo { get; set; }
    }
}