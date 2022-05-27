﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Data;
using WebApplication1.Services;

namespace WebApplication1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContactsController : ControllerBase
    {
        private readonly ContactService _service;

        public ContactsController(ContactService service)
        {
            _service = service;
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
                var result = await _service.GetContacts(username);
                return Ok(result);
            }
            catch
            {
                return BadRequest(); //to check what to return
            }
        }

        // get detailes of contact with a specific id
        // GET: api/Contacts/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Contact>> GetContact(string id, string username)
        {
            var contact = await _service.GetContact(id, username);
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
            var contact = GetContact(id, editedContact.username);
            if (contact == null)
            {
                return NotFound();
            }
            await _service.SetContact(id, editedContact.username, editedContact.name, editedContact.server);
            return StatusCode(204);
        }

        // add a new contact to the contacts of the current user
        // POST: api/Contacts
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult> PostContact([FromBody] newContact newContact)
        {

            if (newContact.contactid == newContact.username)
            {
                return BadRequest();
            }
            var contact = await _service.AddContact(newContact.contactid, newContact.username, newContact.name, newContact.server);
            if(contact != null)
            {
                return StatusCode(201);
            }
            return NotFound();


            //if (newContact.contactid == newContact.username)   // if contact is the same as the user
            //{
            //    return BadRequest();
            //}
            //Contact contact = new Contact() { contactid = newContact.contactid, username = newContact.username, name = newContact.name, server = newContact.server };
            //_context.Contacts.Add(contact);
            //try
            //{
            //    await _context.SaveChangesAsync();
            //}
            //catch (DbUpdateException)
            //{
            //    if (ContactExists(contact.contactid))
            //    {
            //        return Conflict();
            //    }
            //    else
            //    {
            //        throw;
            //    }
            //}
            //return StatusCode(201);
            ////return CreatedAtAction("GetContact", new { id = contact.contactid }, contact);
        }

        // delete a contact of the current user acorrding to a specific contact id
        // DELETE: api/Contacts/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteContact(string id, [FromBody] string username)
        {
            var contact = GetContact(id, username);
            if (contact == null)
            {
                return NotFound();
            }
            await _service.DeleteContact(id, username);
            return StatusCode(204);
        }

        //        private bool ContactExists(string id)
        //        {
        //            return _context.Contacts.Any(e => e.contactid == id);
        //        }


        //        // messges functions:
        //        public class NewMessageObj
        //        {
        //            public string? userName { get; set; }
        //            public string? content { get; set; }
        //        }

        //        // get all messages given a user and a contact --- id is the contact 
        //        [HttpGet("{id}/Messages"), ActionName("Messages")]
        //        public async Task<ActionResult<IEnumerable<Message>>> getMessages(string userName, string id)
        //        {
        //            //User currentUser = _context.Users.Where(u => u.id == userName).FirstOrDefault();
        //            Contact currentcontact = _context.Contacts.Where(e => e.contactid == id && e.username == userName).FirstOrDefault();
        //            if (currentcontact == null)
        //            {
        //                return NotFound();
        //            }
        //            //Chat wantedChat = _context.Chat.Where(c => c.userid == currentUser.id && c.contactid == id || c.userid == id && c.contactid==currentUser.id).FirstOrDefault();
        //            Chat wantedChat = _context.Chat.Where(c => c.userid == userName && c.contactid == id).FirstOrDefault();
        //            if(wantedChat == null)
        //            {
        //                return Ok(new List<Message>());
        //            }
        //            var response = await _context.Messages.Where(e => e.ChatId == wantedChat.id).ToListAsync();
        //            return response; 
        //        }

        //        // post create a new message between contact and current user --- id is the contact
        //        [HttpPost("{id}/Messages"), ActionName("Messages")]
        //        public async Task<ActionResult<Message>> PostMessage(string id, [FromBody] NewMessageObj newmsgobj)
        //        {
        //            var currentContact = _context.Contacts.Where(e => e.contactid == id && e.username == newmsgobj.userName).FirstOrDefault();
        //            if (currentContact == null)
        //            {
        //                return NotFound();
        //            }
        //            DateTime msgDate = DateTime.Now; // todo string? 
        //            int newChatId1, newChatId2;
        //            int followingId;
        //            // server same check
        //            if (_context.Users.Where(e=> e.id == newmsgobj.userName).FirstOrDefault().server == currentContact.server)
        //            {
        //                if (_context.Chat.Where(c => c.userid == newmsgobj.userName && c.contactid == id).FirstOrDefault() == null)
        //                {
        //                    if (_context.Chat.Count() == 0)
        //                    {
        //                        newChatId1 = 1;
        //                    }
        //                    else
        //                    {
        //                        newChatId1 = _context.Chat.Max(c => c.id) + 1;
        //                    }
        //                    newChatId2 = newChatId1 + 1;
        //                    Chat chat1 = new Chat() { id = newChatId1, contactid = currentContact.contactid, userid = newmsgobj.userName };
        //                    Chat chat2 = new Chat() { id = newChatId2, contactid = newmsgobj.userName, userid = currentContact.contactid };
        //                    _context.Chat.Add(chat1);
        //                    _context.Chat.Add(chat2);
        //                    await _context.SaveChangesAsync();
        //                }

        //                else
        //                {
        //                    newChatId1 = _context.Chat.Where(c => c.userid == newmsgobj.userName && c.contactid == id).FirstOrDefault().id;
        //                    newChatId2 = _context.Chat.Where(c => c.userid == id && c.contactid == newmsgobj.userName).FirstOrDefault().id;

        //                }

        //                if (_context.Messages.Count() != 0)
        //                {
        //                    followingId = _context.Messages.Max(e => e.id) + 1;
        //                }
        //                else
        //                {
        //                    followingId = 1;
        //                }
        //                Message newmsg1 = new Message() { id = followingId, content = newmsgobj.content, created = msgDate, sent = true, ChatId = newChatId1 };
        //                Message newmsg2 = new Message() { id = followingId+1, content = newmsgobj.content, created = msgDate, sent = false, ChatId = newChatId2 };
        //                currentContact.last = newmsg1.content;
        //                currentContact.lastdate = newmsg1.created;
        //                Contact userNameAsContact = _context.Contacts.Where(c => c.username == currentContact.contactid && c.contactid == newmsgobj.userName).FirstOrDefault();
        //                userNameAsContact.last = newmsg2.content;
        //                currentContact.lastdate = newmsg2.created;
        //                _context.Messages.Add(newmsg1);
        //                _context.Messages.Add(newmsg2);
        //;
        //            }
        //            else
        //            {
        //                int newChatId;
        //                if (_context.Chat.Where(c => c.userid == newmsgobj.userName && c.contactid == id).FirstOrDefault() == null)
        //                {
        //                    if (_context.Chat.Count() == 0)
        //                    {
        //                        newChatId = 1;
        //                    }
        //                    else
        //                    {
        //                        newChatId = _context.Chat.Max(c => c.id) + 1;
        //                    }
        //                    int updatedChatId = newChatId;
        //                    Chat chat = new Chat() { id = updatedChatId, contactid = currentContact.contactid, userid = newmsgobj.userName };
        //                    _context.Chat.Add(chat);
        //                    await _context.SaveChangesAsync();
        //                }
        //                else
        //                {
        //                    newChatId = _context.Chat.Where(c => c.userid == newmsgobj.userName && c.contactid == id).FirstOrDefault().id;
        //                }
        //                if (_context.Messages.Count() != 0)
        //                {
        //                    followingId = _context.Messages.Max(e => e.id) + 1;
        //                }
        //                else
        //                {
        //                    followingId = 1;
        //                }
        //                Message newmsg = new Message() { id = followingId, content = newmsgobj.content, created = msgDate, sent = true, ChatId = newChatId };
        //                currentContact.last = newmsg.content;
        //                currentContact.lastdate = newmsg.created;
        //                _context.Messages.Add(newmsg);
        //            }
        //            await _context.SaveChangesAsync();
        //            return StatusCode(201);


        //        }

        //        // get a specific message details --- id is contact and id2 is message Id
        //        [HttpGet("{id}/Messages/{id2}"), ActionName("Messages")]
        //        public async Task<ActionResult<Message>> GetMessage(string userName, string id, int id2)
        //        {
        //            User currentUser = _context.Users.Where(u => u.id == userName).FirstOrDefault();
        //            Contact currentContact = _context.Contacts.Where(e => e.contactid == id && e.username == userName).FirstOrDefault();
        //            Chat wantedChat = _context.Chat.Where(c => c.userid == currentUser.id && c.contactid == id).FirstOrDefault();
        //            return _context.Messages.Where(m => m.ChatId == wantedChat.id && m.id == id2).FirstOrDefault();
        //        }

        //        // put update a message --- id is contact and id2 is message Id

        //        [HttpPut("{id}/Messages/{id2}"), ActionName("Messages")]
        //        public async Task<IActionResult> PutMessage(string id, int id2, [FromBody] NewMessageObj newmsgobj)
        //        {
        //            Message message = _context.Messages.Where(m => m.id == id2).FirstOrDefault();
        //            Chat currentChat = _context.Chat.Where(c => c.id == message.ChatId).FirstOrDefault();
        //            Contact currentContact = _context.Contacts.Where(e => e.contactid == id && e.username == newmsgobj.userName).FirstOrDefault();
        //            if (currentChat.contactid != currentContact.contactid) // todo change if we change primary keys of contact
        //            {
        //                return BadRequest();
        //            }
        //            if (message != null)
        //            {
        //                message.content = newmsgobj.content;
        //                message.created = DateTime.Now;
        //                await _context.SaveChangesAsync();
        //                return StatusCode(204);
        //            }
        //            else
        //            {
        //                return NotFound();
        //            }
        //        }

        //        // delete a message --- id is contact and id2 is message Id
        //        [HttpDelete("{id}/Messages/{id2}"), ActionName("Messages")]
        //        public async Task<ActionResult> DeleteMessage([FromBody] string userName, string id, int id2)
        //        {
        //            User currentUser = _context.Users.Where(u => u.id == userName).FirstOrDefault();
        //            Contact currentContact = _context.Contacts.Where(e => e.contactid == id && e.username == userName).FirstOrDefault();
        //            Chat wantedChat = _context.Chat.Where(c => c.userid == currentUser.id && c.contactid == id).FirstOrDefault();
        //            Message currentMessage = _context.Messages.Where(m => m.ChatId == wantedChat.id && m.id == id2).FirstOrDefault();
        //            if (currentMessage == null)
        //            {
        //                return NotFound();
        //            }
        //            _context.Messages.Remove(currentMessage);
        //            await _context.SaveChangesAsync();
        //            return NoContent();
        //        }
    }
}
