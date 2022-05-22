using Microsoft.AspNetCore.Mvc;
using WebApplication.Data;
using Microsoft.EntityFrameworkCore;

namespace WebApplication1.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ContactsController : ControllerBase
    {

          private readonly WebApplicationContext _context;
          public ContactsController(WebApplicationContext context)
           {
              _context = context;
          }



        //  private static List<Contact> _contacts = new List<Contact>() { new Contact() { UserName="Shai Fisher",NickNameGiven="shai my friend", Last_Date=new DateTime(2022,04,30), Last_Message="last message shai", ServerAdress="8000"} ,
        //                                                                     new Contact() { UserName = "Mor Siman Tov", NickNameGiven = "mor my bff", Last_Date = new DateTime(2022, 04, 24), Last_Message = "last message mor", ServerAdress = "8000"},
        //                                                                     new Contact() { UserName = "Emma Willson", NickNameGiven = "emma my sister", Last_Date = new DateTime(2022, 03, 28), Last_Message = "last message emma", ServerAdress = "8000" }};
        // [HttpGet]
        //    public IEnumerable<Contact> Index() //get all messages list
        // {
        //        return _contacts;
        //  }
        [HttpGet]
        public async Task<IActionResult> Index(string UserName)
        {
            var contacts = await _context.Contacts.Where(contact => contact.UserName == UserName).ToListAsync();
            return Ok(contacts);

        }


     //   [HttpGet("{username}")]
     //   public Contact Details(string? username) // get a specific Contact by Id
     //   {
     //       return _contacts.Where(x => x.UserName == username).FirstOrDefault();
     //   }

     //   [HttpPost]
     //   public void Create([Bind("UserName")] Contact contact)
     //   {
     //       _contacts.Add(contact);
     //   }




    }
}
