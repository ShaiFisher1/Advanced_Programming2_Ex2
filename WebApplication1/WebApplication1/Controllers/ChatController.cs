using Microsoft.AspNetCore.Mvc;

namespace WebApplication1.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ChatController : ControllerBase
    {

        private static Contact Shai = new Contact() { id = "Shai Fisher", name = "shai my friend", lastdate = new DateTime(2022, 04, 30), last = "last message shai", server = "8000" };
        private static Contact Mor = new Contact() { id = "Mor Siman Tov", name = "mor my bff", lastdate = new DateTime(2022, 04, 24), last = "last message mor", server = "8000" };
        private static Contact Emma = new Contact() { id = "Emma Willson", name = "emma my sister", lastdate = new DateTime(2022, 03, 28), last = "last message emma", server = "8000" };
        
        private static List<Chat> _chats = new List<Chat>() { new Chat() { id = 1, userid="Shai Fisher",contact=Mor} , // shai has chat with Mor and with Emma
                                                              new Chat() { id = 2, userid="Shai Fisher",contact=Emma},
                                                              new Chat() { id = 3, userid="Mor Siman Tov",contact=Shai}, // Mor has chat only with Shai
                                                              new Chat() { id = 4, userid="Emma Willson",contact=Shai},
                                                              new Chat() { id = 5, userid="Emma Willson",contact=Mor}}; // Emma has chat with Shai and Mor
        [HttpGet]
        public IEnumerable<Chat> Index() // get all Chats 
        {
            return _chats;
        }


        [HttpGet("{id}")]
        public Chat Details(int? id) // get a specific chat information by Id
        {
            return _chats.Where(x => x.id == id).FirstOrDefault();
        }

        [HttpPost]
        public void Create([Bind("User_id")] Chat chat) // todo not sure what to put in Bind here
        {
            _chats.Add(chat);
        }
    }
}
