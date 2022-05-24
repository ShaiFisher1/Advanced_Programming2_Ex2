using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Data;

namespace WebApplication1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransferController : ControllerBase
    {
        private readonly WebApplicationContext _context;

        public TransferController(WebApplicationContext context)
        {
            _context = context;
        }
        public class bodyTransfer
        {
            public string? from { get; set; }  // contact id
            public string? to { get; set; }   // user id
            public string? content { get; set; }
        }        

        // POST api/<TransferController>
        [HttpPost]
        public async Task<ActionResult> Index([FromBody] bodyTransfer value)
        {
            string id = value.to;   // user id
            string contactid = value.from; // contact
            User user = _context.Users.Where(u => u.id == id).FirstOrDefault();

            if (user == null)
            {

                return NotFound();
            }

            Contact contact = _context.Contacts.Where(c => c.contactid == contactid).FirstOrDefault();
            if (contact == null)
            {
                return NotFound();
            }
            if (_context.Chat.Where(c => c.userid == id && c.contactid == contactid).FirstOrDefault() == null)
            {
                int newChatId = _context.Chat.Max(c => c.id) + 1;
                Chat chat = new Chat() { id = newChatId, contactid = contactid, userid = id };
                _context.Chat.Add(chat);
                await _context.SaveChangesAsync();
            }

            int followingId;

            if (_context.Messages.Count() != 0)
            {
                followingId = _context.Messages.Max(e => e.id) + 1;
            }
            else
            {
                followingId = 1;
            }

            int chatId = _context.Chat.Where(c => c.userid == id && c.contactid == contactid).FirstOrDefault().id;
            DateTime msgDate = DateTime.Now;
            Message newMessage = new Message() { id = followingId, content = value.content, sent = true, created = msgDate, ChatId = chatId };
            contact.last = newMessage.content;
            contact.lastdate = newMessage.created;
            _context.Messages.Add(newMessage);
            await _context.SaveChangesAsync();
            return StatusCode(201);
        }
    }
}
