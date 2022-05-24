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
        [HttpGet("{id}")]
        public async Task<ActionResult<IEnumerable<Contact>>> GetContacts(string id)
        {
            return await _context.Contacts.Where(contact => contact.username == id).ToListAsync();
        }


        // GET: api/Contacts/5
        //[HttpGet("{id}")]
        //public async Task<ActionResult<Contact>> GetContact(string id)
        //{
        //    var contact = await _context.Contacts.FindAsync(id);

        //    if (contact == null)
        //    {
        //        return NotFound();
        //    }

        //    return contact;
        //}

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
        //[HttpPost]
        //public async Task<ActionResult<Contact>> PostContact(string id, string name, string server, string username)
        //{
        //    Contact contact = new Contact() { id = id, name = name, server = server, username = username };
        //    _context.Contacts.Add(contact);
        //    try
        //    {
        //        await _context.SaveChangesAsync();
        //    }
        //    catch (DbUpdateException)
        //    {
        //        if (ContactExists(contact.id))
        //        {
        //            return Conflict();
        //        }
        //        else
        //        {
        //            throw;
        //        }
        //    }

        //    return CreatedAtAction("GetContact", new { id = contact.id }, contact);
        //}

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

        // messges functions:

        // get all messages given a user and a contact --- id is the contact 
        [HttpGet("{id}/Messages"), ActionName("Messages")]
        public async Task<ActionResult<IEnumerable<Message>>> getMessages(string userName, string id)
        {
            User currentUser = _context.Users.Where(u => u.id == userName).FirstOrDefault();
            Contact currentcontact = _context.Contacts.Where(e => e.id == id && e.username==userName).FirstOrDefault();
            Chat wantedChat = _context.Chat.Where(c => c.userid == currentUser.id && c.contact.id == id).FirstOrDefault();
            return await _context.Messages.Where(e => e.ChatId == wantedChat.id).ToListAsync();
        }

        // post create a new message between contact and current user --- id is the contact
        [HttpPost("{id}/Messages"), ActionName("Messages")]
        public async Task<ActionResult<Message>> PostMessage(string userName, string id, string content)
        {
            Contact currentContact = _context.Contacts.Where(e => e.id == id && e.username == userName).FirstOrDefault();
            DateTime msgDate = DateTime.Now; // todo tostring? 
            int chatId = _context.Chat.Where(c => c.userid == userName && c.contact.id == id).FirstOrDefault().id;
            int followingId;
            if(_context.Messages.Count() !=0)
            {
                followingId = _context.Messages.Max(e => e.id)+1;
            }
            else
            {
                followingId = 1;
            }
            Message newmsg = new Message() { id=followingId,content=content, created= msgDate, sent=false, ChatId=chatId};
            currentContact.last = newmsg.content;
            currentContact.lastdate = newmsg.created;
            _context.Messages.Add(newmsg);
            await _context.SaveChangesAsync();
            return StatusCode(201);
        }

        // get a specific message details --- id is contact and id2 is message Id
        [HttpGet("{id}/Messages{id2}"), ActionName("Messages")]
        public async Task<ActionResult<Message>> GetMessage(string userName, string id, int id2)
        {
            User currentUser = _context.Users.Where (u => u.id == userName).FirstOrDefault();
            Contact currentContact = _context.Contacts.Where(e => e.id == id && e.username == userName).FirstOrDefault();
            Chat wantedChat = _context.Chat.Where(c => c.userid == currentUser.id && c.contact.id == id).FirstOrDefault();
            return _context.Messages.Where(m => m.ChatId == wantedChat.id && m.id==id2).FirstOrDefault();
        }

        // put update a message --- id is contact and id2 is message Id

        [HttpPut("{id}/Messages{id2}"), ActionName("Messages")]
        public async Task<IActionResult> PutMessage(string userName, string content, string id, int id2)
        {
            //todo what if the message edited is the last message of the contact
            Message message = _context.Messages.Where(m => m.id == id2).FirstOrDefault();
            if (message != null)
            {
                message.content = content;
                message.created = DateTime.Now;
                await _context.SaveChangesAsync();
                return StatusCode(204);
            }
            else
            {
                return NotFound();
            }
        }

            

        // delete a message --- id is contact and id2 is message Id
        [HttpDelete("{id}/Messages{id2}"), ActionName("Messages")]
        public async Task<ActionResult> DeleteMessage(string userName, string id, int id2)
        {
            User currentUser = _context.Users.Where(u => u.id == userName).FirstOrDefault();
            Contact currentContact = _context.Contacts.Where(e => e.id == id && e.username == userName).FirstOrDefault();
            Chat wantedChat = _context.Chat.Where(c => c.userid == currentUser.id && c.contact.id == id).FirstOrDefault();
            Message currentMessage = _context.Messages.Where(m => m.ChatId == wantedChat.id && m.id == id2).FirstOrDefault();
            if(currentMessage == null)
            {
                return NotFound();
            }
            _context.Messages.Remove(currentMessage);
            await _context.SaveChangesAsync();
            return NoContent();
        }

        


        

    }
}