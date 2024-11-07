using Fastwifi.DataModels;
using Fastwifi.DTO;
using Fastwifi.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace Fastwifi.Controllers
{
    [Route("api/contacts")]    
    [ApiController]
    public class ContactsController : ControllerBase
    {
        private readonly IEmailService _emailService;
        private readonly Context _context;

        public ContactsController(IEmailService emailService, Context dbContext)
        {
            _emailService = emailService ?? throw new ArgumentNullException(nameof(emailService));
            _context = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }

        // POST: api/contact
        [HttpPost]
        public async Task<IActionResult> SendMessage([FromBody] ContactMessageDto dto)
        {
            if (dto == null || string.IsNullOrEmpty(dto.Email) || string.IsNullOrEmpty(dto.Message))
            {
                return BadRequest("Invalid contact message");
            }

            // Save the contact message to the database
            var contactMessage = new Contact
            {
                Name = dto.Name,
                Email = dto.Email,
                Message = dto.Message
            };

            _context.Contacts.Add(contactMessage);
            await _context.SaveChangesAsync();

            // Send an email notification
            bool emailSent = await _emailService.SendContactMessageAsync(dto);
            if (!emailSent)
            {
                return StatusCode(500, "Failed to send email notification.");
            }

            return Ok(new { Message = "Contact message sent successfully!" });
        }

        // GET: api/contact
        [HttpGet("all")]
        public IActionResult GetContactMessages()
        {
            var contactMessages = _context.Contacts.ToList();
            return Ok(contactMessages);
        }

        // GET: api/contact/count
        [HttpGet("count")]
        public IActionResult GetContactMessagesCount()
        {
            var contactMessagesCount = _context.Contacts.Count();
            return Ok(contactMessagesCount);
        }
    }
}
