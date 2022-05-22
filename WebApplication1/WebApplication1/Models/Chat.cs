namespace WebApplication1
{
    public class Chat
    {
        public int Id { get; set; } // id of the chat
        public string UserName { get; set; } // the user that the chat belongs to
        public Contact Contact { get; set; } // the contact that the chat is with
    }
}