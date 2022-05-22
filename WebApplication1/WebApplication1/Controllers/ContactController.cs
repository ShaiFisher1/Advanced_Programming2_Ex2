using Microsoft.AspNetCore.Mvc;

namespace WebApplication1.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ContactController : ControllerBase
    {

        private static List<Contact> _contacts = new List<Contact>() { new Contact() { Id="shaifisher1",Name="Shai Fisher", Last_Date=new DateTime(2022,04,30), Last_Message="last message shai", Server="8000"} ,
                                                                       new Contact() { Id = "morsimantov5", Name = "Mor Siman Tov", Last_Date = new DateTime(2022, 04, 24), Last_Message = "last message mor", Server = "8000"},
                                                                       new Contact() { Id = "emmawilsonQueen", Name = "Emma Willson", Last_Date = new DateTime(2022, 03, 28), Last_Message = "last message emma", Server = "8000" }};
        [HttpGet]
        public IEnumerable<Contact> Index() //get all messages list
        {
            return _contacts;
        }


        [HttpGet("{id}")]
        public Contact Details(string? id) // get a specific Contact by Id
        {
            return _contacts.Where(x => x.Id == id).FirstOrDefault();
        }

        [HttpPost]
        public void Create([Bind("Body")] Contact contact)
        {
            _contacts.Add(contact);
        }




    }
}
