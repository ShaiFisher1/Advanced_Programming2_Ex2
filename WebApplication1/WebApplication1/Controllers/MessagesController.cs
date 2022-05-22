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

        private static List<Message> _messages = new List<Message>() { new Message() { id = 1,  created= new DateTime(2022, 05, 22), From=1, content="Hello", ChatId=1,sent=true} ,
                                                                       new Message() { id = 2,  created = new DateTime(2022, 05, 22), From = 0 , content = "Hey whats up", ChatId=1,sent=true},
                                                                       new Message() { id = 3,  created = new DateTime(2022, 05, 22), From = 1 , content = "I'm good hbu?", ChatId=1,sent=true },
                                                                       new Message() { id = 4,  created = new DateTime(2022, 05, 22), From = 0 , content = "me too", ChatId=1,sent=true }};
        [HttpGet]
        public IEnumerable<Message> Index() //get all messages list
        {
            return _messages;
        }


        [HttpGet("{id}")]
        public Message Details(int? id) // get a specific message by Id
        {
            return _messages.Where(x => x.id == id).FirstOrDefault();
        }

        [HttpPost]
        public void Create([Bind("content")] Message message) // Add a new message 
        {
            _messages.Add(message);
        }
    }
}
