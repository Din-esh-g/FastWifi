using System.Net.Mail;
using System.Net;

namespace Fastwifi.Helper
{
    public class SendEmail
    {
        public static Task SendEmailToUser33(string Toemail, string body)
        {

            var MailAddress = "fastwif@outlook.com";
            var Password = "Beltar@43";
            string Subject = "Password Reset Request!";
            string body1 = "Your password is: " + body;

            var client = new SmtpClient("smtp-mail.outlook.com", 587)
            {
                EnableSsl = true,
                Credentials = new NetworkCredential(MailAddress, Password)
            };
            try
            {
                return client.SendMailAsync(
                                                  new MailMessage(from: MailAddress,
                                                                                to: Toemail,
                                                                                                              Subject,
                                                                                                                                            body1)
                                                  {
                                                      IsBodyHtml = true
                                                  });
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }
        public static async Task SendEmailToUser(string Toemail, string body)
        {
            IConfiguration config = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .Build();
            // Get email settings from appsettings.json
            string MailAddress = config.GetSection("EmailSettings")["FromEmail"];
            string Password = config.GetSection("EmailSettings")["Password"];

            string Subject = "Forget Password!";
            string body1 = "<img src='/images/e_logo.png' alt='Fast Wifi Company'/>" +
                           "<br/><br/>" +
                           $"Your temporary password is: {body}<br/><br/>" +
                           "Please use this password for Login.<br/><br/>" +
                           "Please note that this email address is not monitored. " +
                            "Please do not reply to this email.";

            var client = new SmtpClient("smtp-mail.outlook.com", 587)
            {
                EnableSsl = true,
                Credentials = new NetworkCredential(MailAddress, Password)
            };

            try
            {
                await client.SendMailAsync(
                    new MailMessage(from: MailAddress, to: Toemail, subject: Subject, body: body1)
                    {
                        IsBodyHtml = true
                    });
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public static async Task SendEmailToManager3(string Toemail, string body)
        {
            IConfiguration config = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .Build();
            // Get email settings from appsettings.json
            string MailAddress = config.GetSection("EmailSettings")["FromEmail"];
            string Password = config.GetSection("EmailSettings")["Password"];

            string Subject = "FW Progess Notes is ready for your review!";
            string body1 = "<img src='/images/e_logo.png' alt='Fast Wifi Company'/>" +
                           "<br/><br/>" +
                           $"Progrss note has been submitted  for review.<br/><br/><br/>" +
                            "Details<br/><br/>" +
                            "{body}<br/><br/>" +
                           "Please note that this email address is not monitored. " +
                            "Please do not reply to this email.";

            var client = new SmtpClient("smtp-mail.outlook.com", 587)
            {
                EnableSsl = true,
                Credentials = new NetworkCredential(MailAddress, Password)
            };

            try
            {
                await client.SendMailAsync(
                    new MailMessage(from: MailAddress, to: Toemail, subject: Subject, body: body1)
                    {
                        IsBodyHtml = true
                    });
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public static async Task SendEmailToManager(string Toemail, string body)
        {
            try
            {
                SmtpClient client = new SmtpClient("smtp-mail.outlook.com");
                client.Port = 587;
                client.EnableSsl = true;
                client.UseDefaultCredentials = false;
                System.Net.NetworkCredential cre = new System.Net.NetworkCredential("fastwif@outlook.com", "Beltar@43");
                client.Credentials = cre;
                client.DeliveryMethod = SmtpDeliveryMethod.Network;

                MailMessage msg = new MailMessage();
                msg.To.Add(Toemail);
                msg.From = new MailAddress("fastwif@outlook.com");
                msg.Subject = "FW Progess Notes is ready for your review!";
                string body1 = "<img src='/images/e_logo.png' alt='Fast Wifi Company'/>" +
                           "<br/><br/>" +
                           $"Progrss note has been submitted  for review.<br/><br/><br/>" +
                            "Details<br/><br/>" +
                            "{body}<br/><br/>" +
                           "Please note that this email address is not monitored. " +
                            "Please do not reply to this email.";
                msg.Body = body1;
                msg.IsBodyHtml = true;
                await client.SendMailAsync(msg);

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
           
        }
    }
}
