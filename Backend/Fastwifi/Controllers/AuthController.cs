using Fastwifi.DataModels;
using Fastwifi.DTO;
using Fastwifi.Helper;
using Fastwifi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Fastwifi.Controllers
{

    [Route("api/[controller]")]
        [ApiController]
        public class AuthController : ControllerBase
        {
            private Context _context;
            private readonly IConfiguration _configuration;
            // Token blacklist (in-memory list for demonstration)
            private static List<string> _blacklistedTokens = new List<string>();
        private readonly IEmailService _emailService;
            public AuthController(Context context, IConfiguration configuration,IEmailService emailService)
            {
                this._context = context;
                this._configuration = configuration;
            _emailService = emailService;
            }

            // POST: api/Auth
            [HttpPost]
            public IActionResult Post([FromBody] Login login)
            {
                var query = _context.Users.AsQueryable();
                if (login.username == null || login.password == null)
                {
                    return BadRequest();
                }
                query = query.Where(u => u.Username.ToLower() == login.username.ToLower());
                var user = query.FirstOrDefault();

                if (user == null)
                {
                    return NotFound();
                }
                if (user.Status == "Inactive")
                {
                    return Unauthorized();
                }
                if (PasswordManager.VerifyPassword(login.password, user.Password))
                {

                    var token = GenerateJwtToken(user);
                    return Ok(new { token, user.Fullname, user.Role, user.Email, user.Username, user.Phone });
                }

                return Unauthorized();

            }

            private object GenerateJwtToken(User user)
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                var key = Encoding.ASCII.GetBytes(_configuration["Jwt:Secret"]);

                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(new Claim[]
                    {
                    new Claim(ClaimTypes.Name, user.ID.ToString(), user.Username),
                        // Add more claims if needed
                    }),
                    Expires = DateTime.UtcNow.AddDays(7), // Token expiration time
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
                };

                var token = tokenHandler.CreateToken(tokenDescriptor);
                return tokenHandler.WriteToken(token);
            }

            //Post:api/Auth/ForgotPassword
            [HttpPost("ForgotPassword")]
            public async Task<ActionResult> ForgotPasswordAsync([FromBody] Forgetpassword forgetpassword)
            {
                var query = _context.Users.AsQueryable();
                if (forgetpassword.email == null)
                {
                    return BadRequest();
                }
                query = query.Where(u => u.Email.ToLower() == forgetpassword.email.ToLower());
                var user1 = query.FirstOrDefault();
                if (user1 == null)
                {
                    return NotFound();
                }
                if (user1.Email == forgetpassword.email)
                {
                    var newpassword = PasswordManager.GenerateRandomPassword(6);
                    user1.Password = PasswordManager.HashPassword(newpassword);
                    _context.SaveChanges();
                    try
                    {
                      //  _ = SendEmail.SendEmailToUser(user1.Email, newpassword);
                     bool status= await _emailService.SendEmailToUser(user1.Email, newpassword);
                    if (status)
                    {
                        return Ok("New Password has been sent to your email. Please follow the instruction from the email.");
                    }
                       
                    }
                    catch (Exception ex)
                    {
                    return StatusCode(500, "Error sending email.");
                }
                }
                return Unauthorized();
            }

            //Post:api/Auth/ChangePassword
            [HttpPost("ChangePassword")]
            public IActionResult ChangePassword([FromBody] ChangePwd changepwd)
            {
                var query = _context.Users.AsQueryable();
                if (changepwd.email == null || changepwd.password == null || changepwd.newpassword == null)
                {
                    return BadRequest();
                }
                query = query.Where(u => u.Email.ToLower() == changepwd.email.ToLower());
                var user2 = query.FirstOrDefault();
                if (user2 == null)
                {
                    return NotFound();
                }
                if (PasswordManager.VerifyPassword(changepwd.password, user2.Password))
                {
                    user2.Password = PasswordManager.HashPassword(changepwd.newpassword);
                    _context.SaveChanges();
                    return Ok("Password Changed Successfully. Please use new password to login!");
                }
                return Unauthorized();
            }
            //Post:api/Auth/Logout
            [HttpPost("Logout")]
            public IActionResult Logout()
            {
                var token = HttpContext.Request.Headers["Authorization"].ToString().Replace("Bearer ", "");

                Console.WriteLine(token);
                // Check if the token is already blacklisted
                if (_blacklistedTokens.Contains(token))
                {
                    return BadRequest("Token already blacklisted");
                }

                // Add the token to the blacklist
                _blacklistedTokens.Add(token);

                return Ok("Logged Out Successfully");


            }
        }

 
}
