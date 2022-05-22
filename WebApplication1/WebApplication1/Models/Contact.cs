using System.ComponentModel.DataAnnotations;
namespace WebApplication1
{
    public class Contact
    {
        [Key]
        public string UserName { get; set; }
        public string NickNameGiven { get; set; } // nickname given to a contact by the user 
        public DateTime Last_Date { get; set; } // when last message was sent
        public string Last_Message { get; set; } // the last message that was sent
        public string ServerAdress { get; set; }
    }
}