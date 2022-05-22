using Microsoft.AspNetCore.Mvc;

namespace WebApplication1.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MessagesController : ControllerBase
    {
        //public IActionResult Index()
        //{
        //  return View();
        //}

        private static List<Message> _messages = new List<Message>() { new Message() { Id = 1,  Date= new DateTime(2022, 05, 22), From=1, Body="Hello", ChatId=1} ,
                                                                       new Message() { Id = 2,  Date = new DateTime(2022, 05, 22), From = 0 , Body = "Hey whats up", ChatId=1},
                                                                       new Message() { Id = 3,  Date = new DateTime(2022, 05, 22), From = 1 , Body = "I'm good hbu?", ChatId=1 },
                                                                       new Message() { Id = 4,  Date = new DateTime(2022, 05, 22), From = 0 , Body = "me too", ChatId=1 }};
        [HttpGet]
        public IEnumerable<Message> Index() //get all messages list
        {
            return _messages;
        }


        [HttpGet("{id}")]
        public Message Details(int? id) // get a specific message by Id
        {
            return _messages.Where(x => x.Id == id).FirstOrDefault();
        }

        [HttpPost]
        public void Create([Bind("Body")] Message message) // Add a new message 
        {
            _messages.Add(message);
        }
    }
}
