using Fastwifi.DataModels;
using Fastwifi.DTO;
using Fastwifi.Models;
using Microsoft.AspNetCore.Mvc;
namespace Fastwifi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class JobApplicationController : ControllerBase
    {
        private readonly IWebHostEnvironment _environment;
        private readonly Context _context;
        private readonly IEmailService _emailService;

        public JobApplicationController(IWebHostEnvironment environment, Context context, IEmailService emailService)
        {
            _environment = environment; // Used for file saving
            _context = context;
            _emailService = emailService;
        }

        // GET: api/jobapplication
        [HttpGet]
        public ActionResult<IEnumerable<JobApplicationReturnDTO>> GetJobApplications()
        {
            var jobApplications = _context.JobApplications.ToList();

            // Project each JobApplication object to a JobApplicationReturnDTO object
            var returnJobApplications = jobApplications.Select(jobApplication => new JobApplicationReturnDTO
            {
                ID = jobApplication.Id,
                Name = jobApplication.Name,
                Phone = jobApplication.Phone,
                Email = jobApplication.Email,
                Position = jobApplication.Position,
                Message = jobApplication.Message,
                ResumeFileName = jobApplication.ResumeFileName
            }).ToList();

            return Ok(returnJobApplications);
        }


        // POST: api/jobapplication
        [HttpPost]
        public async Task<ActionResult> SubmitJobApplication([FromForm] JobApplicationDto dto)
        {
            if (dto == null || dto.Resume == null)
            {
                return BadRequest("Invalid job application or resume file.");
            }

            // Convert resume to byte array
            using (var memoryStream = new MemoryStream())
            {
                await dto.Resume.CopyToAsync(memoryStream);

                var jobApplication = new JobApplication
                {
                    Name = dto.Name,
                    Phone = dto.Phone,
                    Email = dto.Email,
                    Position = dto.Position,
                    Message = dto.Message,
                    ResumeData = memoryStream.ToArray(), // Save resume as byte array
                    ResumeFileName = dto.Resume.FileName, // Original file name
                    ResumeContentType = dto.Resume.ContentType // Content type
                };

                // Save to the database
                _context.JobApplications.Add(jobApplication);
                await _context.SaveChangesAsync(); // Ensure async database save
            }

            // Send email notification
            bool emailSent = await _emailService.SendJobApplicationEmailAsync("dineshg822@gmail.com", "New Job Application", dto);

            if (!emailSent)
            {
                return StatusCode(500, "Failed to send email notification.");
            }

            return Ok(new { Message = "Job application submitted successfully!" });
        }


               
        // DELETE: api/jobapplication/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteJobApplication(int id)
        {
            var jobApplication = await _context.JobApplications.FindAsync(id);
            if (jobApplication == null)
            {
                return NotFound();
            }

            _context.JobApplications.Remove(jobApplication);
            await _context.SaveChangesAsync();

            return Ok(new { Message = "Job application deleted successfully!" });
        }
        //count the number of job applications
        [HttpGet("count")]
        public IActionResult GetJobApplicationsCount()
        {
            var jobApplicationsCount = _context.JobApplications.Count();
            return Ok(jobApplicationsCount);
        }
        //give me method which will download the resume file when i pass the id of the job application
        // GET: api/jobapplication/5/resume
        [HttpGet("{id}/resume")]
        public IActionResult GetResume(int id)
        {
            var jobApplication = _context.JobApplications.Find(id);
            if (jobApplication == null || jobApplication.ResumeData == null)
            {
                return NotFound("Resume not found for this job application.");
            }

            // Return the resume as a file download
            return File(jobApplication.ResumeData, jobApplication.ResumeContentType, jobApplication.ResumeFileName);
        }
    }
}
