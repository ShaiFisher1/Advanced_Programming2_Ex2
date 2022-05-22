namespace WebApplication1
{
    public class Chat
    {
        public int id { get; set; } // id of the chat
        public string userid { get; set; } // the user that the chat belongs to
        public Contact contact { get; set; } // the contact that the chat is with
    }
}