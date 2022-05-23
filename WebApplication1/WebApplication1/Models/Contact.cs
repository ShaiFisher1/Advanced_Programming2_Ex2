using System.ComponentModel.DataAnnotations;
namespace WebApplication1
{
    public class Contact
    {
        [Key]
        public string id { get; set; }
        public string name { get; set; } // nickname given to a contact by the user 
        public string server { get; set; }
        public string last { get; set; } // the last message that was sent
        public DateTime lastdate { get; set; } // when last message was sent
        public string username { get; set; } // the username of the contact

    }
}