using Fastwifi.DTO;
using System.Net;
using System.Net.Mail;
using System.Net.Mime;
using static Fastwifi.Models.ProjectEnum;

namespace Fastwifi
{
    public interface IEmailService
    {
        public Task<bool> TestSendEmailAsync(string toEmail, string subject, string body);
        public Task<bool> SendEmailAsync(string email, string subject, ProgressNoteDto progressNoteDto);
        public Task<bool> SendEmailToUser(string Toemail, string body);
        public Task<bool> SendJobApplicationEmailAsync(string recipientEmail, string subject, JobApplicationDto jobApplication);
        Task<bool> SendContactMessageAsync(ContactMessageDto dto);
    }
    public class EmailService : IEmailService
    {
        private readonly IConfiguration _configuration;
        public EmailService(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public async Task<bool>TestSendEmailAsync(string email, string subject, string message)
        {
            try
            {

              string smtpSettings =SmtpSettings.SmtpSettings.ToString();
                string host = _configuration[smtpSettings + ":" + SmtpSettings.Host.ToString()];
                int port = Convert.ToInt32(_configuration[smtpSettings + ":" + SmtpSettings.Port.ToString()]);
                string userName = _configuration[smtpSettings + ":" + SmtpSettings.UserName.ToString()];
                string password = _configuration[smtpSettings + ":" + SmtpSettings.Password.ToString()];
              var client = new SmtpClient(host)
              {
                  Port = port,
                    Credentials = new NetworkCredential(userName, password),
                    EnableSsl = true
                };

                var emailMessage = new MailMessage
                {
                    From = new MailAddress(userName, "Fast Wifi"),
                    Subject = subject,
                    Body = message,
                    IsBodyHtml = true
                };

                emailMessage.To.Add(email);
                await client.SendMailAsync(emailMessage);
               
                return true;

            }
            catch (Exception ex)
            {
                throw new InvalidOperationException(ex.Message);
            }
        }

        public async Task<bool> SendEmailAsync(string email, string subject, ProgressNoteDto progressNoteDto)
        {
            try
            {
                string smtpSettings = SmtpSettings.SmtpSettings.ToString();
                string host = _configuration[smtpSettings + ":" + SmtpSettings.Host.ToString()];
                int port = Convert.ToInt32(_configuration[smtpSettings + ":" + SmtpSettings.Port.ToString()]);
                string userName = _configuration[smtpSettings + ":" + SmtpSettings.UserName.ToString()];
                string password = _configuration[smtpSettings + ":" + SmtpSettings.Password.ToString()];

                var client = new SmtpClient(host)
                {
                    Port = port,
                    Credentials = new NetworkCredential(userName, password),
                    EnableSsl = true
                };
                            

             string messageBody = "<img src='/images/logoemail.png' alt='Fast Wifi Company'/>" +
                           "<br/><br/>" + $@"
            <p>Dear Reviewer ,</p>
            <p>I hope this message finds you well. Below are the details submitted by <b>{progressNoteDto.UserName}</b> of the recent intervention report for consumer <b>{progressNoteDto.ConsumerName}</b>:</p>

            <ul>
                <li><b>Consumer Name:</b> {progressNoteDto.ConsumerName}</li>
                <li><b>Provider Name:</b> {progressNoteDto.ProviderName}</li>
                <li><b>CPSW County:</b> {progressNoteDto.CPSWCounty}</li>
                <li><b>Authorized Representative:</b> {progressNoteDto.AuthorizedRep}</li>
                <li><b>Participants:</b> {progressNoteDto.Participants}</li>
                <li><b>Path Client:</b> {progressNoteDto.PathClient}</li>
                <li><b>Intervention Summary:</b></li>
                    <ul>
                        {string.Join("", progressNoteDto.InterventionSummaries.Select(i => $"<li>Date: {i.Date}, ALS: {i.ALS}, IP: {i.IP}</li>"))}
                    </ul>
                <li><b>Response:</b> {progressNoteDto.Response}</li>
                <li><b>Additional Information:</b> {progressNoteDto.AdditionalInfo}</li>
                <li><b>Signature:</b> {progressNoteDto.Signature} on {progressNoteDto.SignatureDate}</li>
                <li><b>Contact Information:</b> {progressNoteDto.ContactInfo}</li>
                <li><b>Authorized Agency Representative:</b> {progressNoteDto.Title}</li>
                <li><b>Title:</b> {progressNoteDto.Title}</li>
            </ul>
            <p>If you have any questions, feel free to contact us at {progressNoteDto.ContactInfo}.</p>
            <p>Best regards,</p>
            <p>{progressNoteDto.AuthorizedRep}</p>";

                var emailMessage = new MailMessage
                {
                    From = new MailAddress(userName, "Fast Wifi Socaially Necessary Services"),
                    Subject = subject,
                    Body = messageBody,
                    IsBodyHtml = true
                };
              
                emailMessage.To.Add(email);
                await client.SendMailAsync(emailMessage);

                return true;
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException(ex.Message);
            }
        }

        public async Task<bool> SendEmailToUser(string Toemail, string body)
        {
            try
            {
                string smtpSettings = SmtpSettings.SmtpSettings.ToString();
                string host = _configuration[smtpSettings + ":" + SmtpSettings.Host.ToString()];
                int port = Convert.ToInt32(_configuration[smtpSettings + ":" + SmtpSettings.Port.ToString()]);
                string userName = _configuration[smtpSettings + ":" + SmtpSettings.UserName.ToString()];
                string password = _configuration[smtpSettings + ":" + SmtpSettings.Password.ToString()];

                var client = new SmtpClient(host)
                {
                    Port = port,
                    Credentials = new NetworkCredential(userName, password),
                    EnableSsl = true
                };

            string subject = "Forget Password!";
            string messageBody = "<img src='/images/logoemail.png' alt='Fast Wifi Company'/>" +
                           "<br/><br/>" +
                           $"Your temporary password is: {body}<br/><br/>" +
                           "Please use this password for Login.<br/><br/>" +
                           "Please note that this email address is not monitored. " +
                            "Please do not reply to this email.";

                var emailMessage = new MailMessage
                {
                    From = new MailAddress(userName, "Fast Wifi Socaially Necessary Services"),
                    Subject = subject,
                    Body = messageBody,
                    IsBodyHtml = true
                };

                emailMessage.To.Add(Toemail);
                await client.SendMailAsync(emailMessage);

                return true;
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException(ex.Message);
            }
        }
        public async Task<bool> SendJobApplicationEmailAsync(string recipientEmail, string subject, JobApplicationDto jobApplication)
        {
            try
            {
                // Fetch SMTP settings from configuration
                string smtpSettings = SmtpSettings.SmtpSettings.ToString();
                string host = _configuration[smtpSettings + ":" + SmtpSettings.Host.ToString()];
                int port = Convert.ToInt32(_configuration[smtpSettings + ":" + SmtpSettings.Port.ToString()]);
                string userName = _configuration[smtpSettings + ":" + SmtpSettings.UserName.ToString()];
                string password = _configuration[smtpSettings + ":" + SmtpSettings.Password.ToString()];

                // Setup SMTP client
                var client = new SmtpClient(host)
                {
                    Port = port,
                    Credentials = new NetworkCredential(userName, password),
                    EnableSsl = true
                };

                // Build the email body with job application details
                string messageBody = "<img src='/images/logoemail.png' alt='Fast Wifi Company'/>" +
                                     "<br/><br/>" + $@"
            <p>Dear HR Team,</p>
            <p>We have received a new job application from <b>{jobApplication.Name}</b> for the position of <b>{jobApplication.Position}</b>. Below are the application details:</p>

            <ul>
                <li><b>Name:</b> {jobApplication.Name}</li>
                <li><b>Email:</b> {jobApplication.Email}</li>
                <li><b>Phone:</b> {jobApplication.Phone}</li>
                <li><b>Position:</b> {jobApplication.Position}</li>
                <li><b>Message:</b> {jobApplication.Message}</li>
            </ul>
            <p>If you have any questions or need additional information, feel free to contact the applicant directly.</p>
            <p>Best regards,</p>
            <p>FastWifi HR Team</p>";

                // Create the email message
                var emailMessage = new MailMessage
                {
                    From = new MailAddress(userName, "Fast Wifi HR"),
                    Subject = subject,
                    Body = messageBody,
                    IsBodyHtml = true
                };

                // Add recipient email (HR team's email or any other recipient)
                emailMessage.To.Add(recipientEmail);

                // Send the email asynchronously
                await client.SendMailAsync(emailMessage);

                return true; // Email sent successfully
            }
            catch (Exception ex)
            {
                // Handle or log the exception
                throw new InvalidOperationException($"Error sending job application email: {ex.Message}");
            }
        }

       

     

        public async Task<bool> SendContactMessageAsync(ContactMessageDto dto)
        {
            try
            {
                // Fetch SMTP settings from configuration
                string smtpSettings = SmtpSettings.SmtpSettings.ToString();
                string host = _configuration[smtpSettings + ":" + SmtpSettings.Host.ToString()];
                int port = Convert.ToInt32(_configuration[smtpSettings + ":" + SmtpSettings.Port.ToString()]);
                string userName = _configuration[smtpSettings + ":" + SmtpSettings.UserName.ToString()];
                string password = _configuration[smtpSettings + ":" + SmtpSettings.Password.ToString()];

                var client = new SmtpClient(host, port)
                {
                    Credentials = new NetworkCredential(userName, password),
                    EnableSsl = true
                };

                string messageBody = $@"
                <p>Dear Admin,</p>
                <p>A new contact message has been received:</p>
                <p><b>Name:</b> {dto.Name}</p>
                <p><b>Email:</b> {dto.Email}</p>
                <p><b>Message:</b> {dto.Message}</p>";

                var mailMessage = new MailMessage
                {
                    From = new MailAddress(userName, "Fastwifi Company"),
                    Subject = "New Contact Message",
                    Body = messageBody,
                    IsBodyHtml = true
                };

                mailMessage.To.Add("dineshg822@gmail.com"); // Admin email

                await client.SendMailAsync(mailMessage);
                return true;
            }
            catch (Exception ex)
            {
                // Log the error and return false
                Console.WriteLine($"Error sending email: {ex.Message}");
                return false;
            }
        }
    }
    }

