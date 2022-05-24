using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Data;

namespace WebApplication1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContactsController : ControllerBase
    {
        private readonly WebApplicationContext _context;

        public ContactsController(WebApplicationContext context)
        {
            _context = context;
        }
        public class newContact
        {
            public string? contactid { get; set; }
            public string? username { get; set; }
            public string? name { get; set; }
            public string? server { get; set; }
        }

        public class editedContact
        {
            public string? username { get; set; }
            public string? name { get; set; }
            public string? server { get; set; }
        }


        // get all contacts of current user
        // GET: api/Contacts
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Contact>>> GetContacts(string username)
        {

            try
            {
                return await _context.Contacts.Where(contact => contact.username == username).ToListAsync();
            }
            catch
            {
                return BadRequest();
            }
        }

        // get detailes of contact with a specific id
        // GET: api/Contacts/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Contact>> GetContact(string id, string username)
        {
            var contact = await _context.Contacts.FindAsync(id, username);

            if (contact == null)
            {
                return NotFound();
            }

            return contact;
        }

        // edit a contact of the current user acorrding to a specific contact id
        // PUT: api/Contacts/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutContact(string id, [FromBody] editedContact editedContact)
        {
            var contact = await _context.Contacts.FindAsync(id, editedContact.username);
            //if (id != contact.contactid)
            //{
            //    return BadRequest();
            //}
            contact.name = editedContact.name;
            contact.server = editedContact.server;
            _context.Entry(contact).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ContactExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // add a new contact to the contacts of the current user
        // POST: api/Contacts
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult> PostContact([FromBody] newContact newContact)
        {
            if (newContact.contactid == newContact.username)   // if contact is the same as the user
            {
                return BadRequest();
            }
            Contact contact = new Contact() { contactid = newContact.contactid, username = newContact.username, name = newContact.name, server = newContact.server };
            _context.Contacts.Add(contact);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (ContactExists(contact.contactid))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }
            return StatusCode(201);
            //return CreatedAtAction("GetContact", new { id = contact.contactid }, contact);
        }

        // delete a contact of the current user acorrding to a specific contact id
        // DELETE: api/Contacts/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteContact(string id, [FromBody] string username)
        {
            var contact = await _context.Contacts.FindAsync(id, username);
            //var contact = await _context.Contacts.FindAsync(id);
            if (contact == null)
            {
                return NotFound();
            }

            _context.Contacts.Remove(contact);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ContactExists(string id)
        {
            return _context.Contacts.Any(e => e.contactid == id);
        }
    }
}
