﻿using Fastwifi.DataModels;
using Fastwifi.DTO;
using Fastwifi.Helper;
using Fastwifi.Models;
using Microsoft.AspNetCore.Mvc;

namespace Fastwifi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProgressNotesController : ControllerBase

    {
        private readonly Context _context;        

       private readonly IEmailService _emailService;
        public ProgressNotesController(IEmailService emailService, Context context )
        {
            _emailService = emailService;
            _context = context;
        }
       
        // Get all notes
        [HttpGet]
        public ActionResult<IEnumerable<ProgressNote>> GetAll()
        {
          var  _progressNotes = _context.ProgressNotes.ToList();
            return Ok(_progressNotes);
        }

        // Get a specific note by Id
        [HttpGet("{id}")]
        public ActionResult<ProgressNote> Get(int id)
        {
            var progressNote = _context.ProgressNotes.FirstOrDefault(p => p.Id == id);
            if (progressNote == null)
                return NotFound();
            return Ok(progressNote);
        }

        // Create a new note
        [HttpPost]
        public async Task<ActionResult> Create([FromBody] ProgressNoteDto progressNoteDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var newProgressNote = new ProgressNote
            {
               
                ConsumerName = progressNoteDto.ConsumerName,
                ProviderName = progressNoteDto.ProviderName,
                CPSWCounty = progressNoteDto.CPSWCounty,
                PathClient = progressNoteDto.PathClient,
                Participants = progressNoteDto.Participants,
                Services = progressNoteDto.Services.Select(s => new ServiceDetail
                {
                    Date = s.Date,
                    Location = s.Location,
                    ServiceStartTime = s.ServiceStartTime,
                    ServiceStopTime = s.ServiceStopTime,
                    ITTStartTime = s.ITTStartTime,
                    ITTStopTime = s.ITTStopTime,
                    MilesTraveled = s.MilesTraveled,
                    Locations = s.Locations
                }).ToList(),
                InterventionSummaries = progressNoteDto.InterventionSummaries.Select(i => new InterventionSummary
                {
                    Date = i.Date,
                    ALS = i.ALS,
                    IP = i.IP
                }).ToList(),
                Response = progressNoteDto.Response,
                AdditionalInfo = progressNoteDto.AdditionalInfo,
                AuthorizedRep = progressNoteDto.AuthorizedRep,
                Signature = progressNoteDto.Signature,
                Title = progressNoteDto.Title,
                ContactInfo = progressNoteDto.ContactInfo,
                SignatureDate = progressNoteDto.SignatureDate,
                UserName = progressNoteDto.UserName,
                Comments = progressNoteDto.Comments.Select(c => new Comments
                {
                    providedBy = progressNoteDto.UserName,
                    Message = c.Message,
                    SentAt = DateTime.UtcNow                                   
                  

                }).ToList()

            };

            
            try
            {
              
                _context.ProgressNotes.Add(newProgressNote);
                await _context.SaveChangesAsync();
            } catch (Exception ex)
            {
                  return StatusCode(500, "Error saving note.");
            }

            bool result = await _emailService.SendEmailAsync("Intervention Report for " + progressNoteDto.ConsumerName, progressNoteDto);

            if (result)
            {
                return Ok(newProgressNote);
            }               
           
                
                return StatusCode(500, "Error sending email."); 
           

           
        }

        // Update a note
        [HttpPut("{id}")]
        public ActionResult Update(int id, [FromBody] ProgressNoteDto progressNoteDto)
        {
            var existingNote =_context.ProgressNotes.FirstOrDefault(p => p.Id == id);
            if (existingNote == null)
                return NotFound();

            existingNote.ConsumerName = progressNoteDto.ConsumerName;
            existingNote.ProviderName = progressNoteDto.ProviderName;
            existingNote.CPSWCounty = progressNoteDto.CPSWCounty;
            existingNote.PathClient = progressNoteDto.PathClient;
            existingNote.Participants = progressNoteDto.Participants;
            existingNote.Services = progressNoteDto.Services.Select(s => new ServiceDetail
            {
                Date = s.Date,
                Location = s.Location,
                ServiceStartTime = s.ServiceStartTime,
                ServiceStopTime = s.ServiceStopTime,
                ITTStartTime = s.ITTStartTime,
                ITTStopTime = s.ITTStopTime,
                MilesTraveled = s.MilesTraveled,
                Locations = s.Locations
            }).ToList();
            existingNote.InterventionSummaries = progressNoteDto.InterventionSummaries.Select(i => new InterventionSummary
            {
                Date = i.Date,
                ALS = i.ALS,
                IP = i.IP
            }).ToList();
            existingNote.Response = progressNoteDto.Response;
            existingNote.AdditionalInfo = progressNoteDto.AdditionalInfo;
            existingNote.AuthorizedRep = progressNoteDto.AuthorizedRep;
            existingNote.Signature = progressNoteDto.Signature;
            existingNote.Title = progressNoteDto.Title;
            existingNote.ContactInfo = progressNoteDto.ContactInfo;
            existingNote.SignatureDate = progressNoteDto.SignatureDate;
            existingNote.UserName = progressNoteDto.UserName;
            try { 
            _context.ProgressNotes.Update(existingNote);
            _context.SaveChanges();
                }
            catch (Exception ex)
            {
                return StatusCode(500, "Error updating note.");
            }

            return Ok(existingNote);
        }

        // Delete a note
        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            var noteToRemove = _context.ProgressNotes.FirstOrDefault(p => p.Id == id);
            if (noteToRemove == null)
                return NotFound();

            _context.ProgressNotes.Remove(noteToRemove);
            _context.SaveChanges();
            return Ok();
        }
        //create a method to count the number of notes
        [HttpGet("count")]
        public IActionResult GetProgressNotesCount()
        {
            var progressNoteCount = _context.ProgressNotes.Count();
            return Ok(progressNoteCount);
        }
    }
}
