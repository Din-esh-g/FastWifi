using Fastwifi.DataModels;
using Fastwifi.DTO;
using Fastwifi.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Fastwifi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SubscribesController : ControllerBase
    {
        private readonly IEmailService _emailService;
        private readonly Context _context;

        public SubscribesController(IEmailService emailService, Context dbContext)
        {
            _emailService = emailService;
            _context = dbContext;
        }

        // POST: api/subscribes
        [HttpPost]
        public async Task<IActionResult> Subscribe([FromBody] SuscribeDTO dto)
        {
            if (dto == null || string.IsNullOrEmpty(dto.Email))
            {
                return BadRequest("Invalid subscribe email.");
            }

            // Save the subscribe email to the database
            var subscribe = new SuscribeList
            {
                Email = dto.Email
            };

            try
            {
                _context.SuscribeLists.Add(subscribe);
                await _context.SaveChangesAsync();
            }
            catch (Exception)
            {
                return StatusCode(500, "Failed to save subscribe email to the database.");
            }

            return Ok(new { Message = "Subscribe email saved successfully!" });
        }

        // GET: api/subscribes
        [HttpGet("list")]
        public IActionResult GetSubscribers()
        {
            var subscribes = _context.SuscribeLists.ToList();
            return Ok(subscribes);
        }

        // GET: api/subscribes/count
        [HttpGet("count")]
        public IActionResult GetSubscribersCount()
        {
            var subscriberCount = _context.SuscribeLists.Count();
            return Ok(subscriberCount);
        }

        // DELETE: api/subscribes
        [HttpDelete]
        public IActionResult DeleteSubscriber([FromBody] SuscribeDTO dto)
        {
            if (dto == null || string.IsNullOrEmpty(dto.Email))
            {
                return BadRequest("Invalid subscribe email.");
            }

            var subscriber = _context.SuscribeLists.FirstOrDefault(s => s.Email == dto.Email);
            if (subscriber == null)
            {
                return NotFound();
            }

            _context.SuscribeLists.Remove(subscriber);
            _context.SaveChanges();

            return Ok(new { Message = "Subscribe email deleted successfully!" });
        }
        // DELETE: api/subscribes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSubscriber(int id)
        {
            var subscriber = await _context.SuscribeLists.FindAsync(id);
            if (subscriber == null)
            {
                return NotFound();
            }

            _context.SuscribeLists.Remove(subscriber);
            await _context.SaveChangesAsync();

            return Ok(new { Message = "Subscribe email deleted successfully!" });
        }
        
    }
}
