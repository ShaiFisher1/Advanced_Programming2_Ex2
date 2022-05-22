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

        [HttpGet]
        public async Task<IActionResult> Index(string UserName)
        {
            var contacts = await _context.Contacts.Where(contact => contact.id == UserName).ToListAsync();
            return Ok(contacts);

        }


     //   [HttpGet("{username}")]
      //  public Contact Details(string? username) // get a specific Contact by Id
      //  {
      //      return _contacts.Where(x => x.UserName == username).FirstOrDefault();
     //   }

     //   [HttpPost]
     //   public void Create([Bind("UserName")] Contact contact)
     //   {
     //       _contacts.Add(contact);
     //   }




    }
}
