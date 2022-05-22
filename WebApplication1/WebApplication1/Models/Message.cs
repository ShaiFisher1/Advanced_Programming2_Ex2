namespace WebApplication1
{
    public class Message
    {
        public int Id { get; set; } // id of the message
        public DateTime Date { get; set; } // when the message was sent
        public int From { get; set; } // from which side (0 or 1)
        public string Body { get; set; } //content of the message
        public int ChatId { get; set; } // the chat it belongs to
    }
}