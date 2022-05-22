using Microsoft.AspNetCore.Mvc;

namespace WebApplication1.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ChatController : ControllerBase
    {

        private static Contact Shai = new Contact() { UserName = "Shai Fisher", NickNameGiven = "shai my friend", Last_Date = new DateTime(2022, 04, 30), Last_Message = "last message shai", ServerAdress = "8000" };
        private static Contact Mor = new Contact() { UserName = "Mor Siman Tov", NickNameGiven = "mor my bff", Last_Date = new DateTime(2022, 04, 24), Last_Message = "last message mor", ServerAdress = "8000" };
        private static Contact Emma = new Contact() { UserName = "Emma Willson", NickNameGiven = "emma my sister", Last_Date = new DateTime(2022, 03, 28), Last_Message = "last message emma", ServerAdress = "8000" };
        
        private static List<Chat> _chats = new List<Chat>() { new Chat() { Id = 1, UserName="Shai Fisher",Contact=Mor} , // shai has chat with Mor and with Emma
                                                              new Chat() { Id = 2, UserName="Shai Fisher",Contact=Emma},
                                                              new Chat() { Id = 3, UserName="Mor Siman Tov",Contact=Shai}, // Mor has chat only with Shai
                                                              new Chat() { Id = 4, UserName="Emma Willson",Contact=Shai},
                                                              new Chat() { Id = 5, UserName="Emma Willson",Contact=Mor}}; // Emma has chat with Shai and Mor
        [HttpGet]
        public IEnumerable<Chat> Index() // get all Chats 
        {
            return _chats;
        }


        [HttpGet("{id}")]
        public Chat Details(int? id) // get a specific chat information by Id
        {
            return _chats.Where(x => x.Id == id).FirstOrDefault();
        }

        [HttpPost]
        public void Create([Bind("User_id")] Chat chat) // todo not sure what to put in Bind here
        {
            _chats.Add(chat);
        }
    }
}
