using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Data;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApplication1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InvitationsController : ControllerBase
    {
        private readonly WebApplicationContext _context;

        public InvitationsController(WebApplicationContext context)
        {
            _context = context;
        }
        public class Body
        {
            public string? from { get; set; }
            public string? to { get; set; }
            public string? server { get; set; }
        }

        // POST api/<InvitationsController>
        [HttpPost]
        public async Task<ActionResult> Index([FromBody] Body value)
        {
            var id = value.to;
            var user = await _context.Users.FindAsync(id);

            if (user == null)
            {
                return NotFound();
            }
            Contact newcontact = new Contact() { contactid = value.from, username = value.to, name = value.from, server = value.server };
            _context.Contacts.Add(newcontact);
            await _context.SaveChangesAsync();
            return StatusCode(201);
        }
    }
}
