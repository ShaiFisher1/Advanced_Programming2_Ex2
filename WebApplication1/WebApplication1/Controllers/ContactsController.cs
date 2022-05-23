using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication.Data;
using WebApplication1;

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

        // GET: api/Contacts
       // [HttpGet]
       // public async Task<ActionResult<IEnumerable<Contact>>> GetContacts()
        //{
        //    return await _context.Contacts.ToListAsync();
        //}

        public async Task<ActionResult<IEnumerable<Contact>>> GetContacts(string id)
        {
            return await _context.Contacts.Where(contact => contact.username == id).ToListAsync();
        }

        // GET: api/Contacts/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Contact>> GetContact(string id)
        {
            var contact = await _context.Contacts.FindAsync(id);

            if (contact == null)
            {
                return NotFound();
            }

            return contact;
        }

        // PUT: api/Contacts/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutContact(string id, Contact contact)
        {
            if (id != contact.id)
            {
                return BadRequest();
            }

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

        // POST: api/Contacts
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Contact>> PostContact(string id, string name, string server, string username)
        {
            Contact contact = new Contact() { id = id, name = name, server = server, username = username };
            _context.Contacts.Add(contact);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (ContactExists(contact.id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetContact", new { id = contact.id }, contact);
        }

        // DELETE: api/Contacts/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteContact(string id)
        {
            var contact = await _context.Contacts.FindAsync(id);
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
            return _context.Contacts.Any(e => e.id == id);
        }
    }
}
