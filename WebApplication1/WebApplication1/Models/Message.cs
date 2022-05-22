namespace WebApplication1
{
    public class Message
    {
        public int id { get; set; } // id of the message
        public string content { get; set; } //content of the message
        public DateTime created { get; set; } // when the message was sent
        public bool sent { get; set; }
        public int From { get; set; } // from which side (0 or 1)
        public int ChatId { get; set; } // the chat it belongs to
    }
}