using Fastwifi.DataModels;
using Fastwifi.DTO;
using Fastwifi.Helper;
using Fastwifi.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Fastwifi.Controllers
{
    //[Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private Context _context;

        public UserController(Context context)
        {
            _context = context;
        }

        // GET: api/Users
        [HttpGet]
        public IEnumerable<ReturnUser> GetUser()
        {
            var users = _context.Users;
            // Project each User object to a ReturnUser object
            var returnUsers = users.Select(user => new ReturnUser
            {
                ID = user.ID,
                Username = user.Username,
                Fullname = user.Fullname,
                Email = user.Email,
                Phone = user.Phone,
                Address = user.Address,
                Role = user.Role,
                Status = user.Status,
                UpdatedBy = user.UpdatedBy,
                UpdatedOn = user.UpdatedOn,
                CreatedOn = user.CreatedOn
            });

            return returnUsers;
        }

        // GET: api/Users/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetUser([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var user = await _context.Users.FindAsync(id);

            if (user == null)
            {
                return NotFound();
            }

            return Ok(user);
        }




       // POST: api/User/UpdateStatus
        [HttpPost("UpdateStatus")]
        public async Task<IActionResult> UpdateUser([FromBody] UpdateUser updateUser)
        {
            int userid = updateUser.ID;
            if (userid == 0)
            {
                return BadRequest("Employee ID is required");
            }


            var user = _context.Users.Where(u => u.ID == userid).FirstOrDefault();
            if (user == null)
            {
                return NotFound();
            }

            if (updateUser.Status == "Active" && user.Status == "Active")
            {
                user.Status = "Active";
                user.UpdatedBy = updateUser.UserName;
                user.UpdatedOn = DateTime.Now;
                user.Role = updateUser.Role;

                //user.Status = "Inactive";
            }
            else if (updateUser.Status == "Inactive" && user.Status == "Inactive")
            {
                user.Status = "Inactive";
                user.UpdatedBy = updateUser.UserName;
                user.UpdatedOn = DateTime.Now;
                user.Role = updateUser.Role;
            }
            else if (updateUser.Status == "Inactive" && user.Status == "Active")
            {
                user.Status = "Inactive";
                user.UpdatedBy = updateUser.UserName;
                user.UpdatedOn = DateTime.Now;
                user.Role = updateUser.Role;
            }
            else if (updateUser.Status == "Active" && user.Status == "Inactive")
            {
                user.Status = "Active";
                user.Password = PasswordManager.HashPassword("Test@123");
                user.UpdatedBy = updateUser.UserName;
                user.UpdatedOn = DateTime.Now;
                user.Role = updateUser.Role;
            }


            try
            {
                _context.Entry(user).State = EntityState.Modified;
                await _context.SaveChangesAsync();
                return Ok("User Updated Sucessfully");
            }
            catch (DbUpdateConcurrencyException)
            {
                return BadRequest("User not updated");
            }


        }

        // POST: api/Users
        [HttpPost]
        public async Task<IActionResult> PostUser([FromBody] UserRegister userr)
        {

            if (userr.name == null)
            {
                return BadRequest("Name is required");
            }
            if (userr.email == null)
            {
                return BadRequest("Email is required");
            }

            //validate userr.employeeId is already exist
            var user1 = _context.Users.Where(u => u.Email.ToLower() == userr.email.ToLower()).FirstOrDefault();

            if (userr.office == null)
            {
                return BadRequest("Office is required");
            }
            if (userr.password == null)
            {
                return BadRequest("Password is required");
            }
            if (user1 != null)
            {
                return BadRequest("User with email address is already exist");
            }
            User user = new User
            {
                Fullname = userr.name,
                Email = userr.email,
                Address = userr.office,
                Role = "User",
                Username = userr.email.Trim(),
                Password = PasswordManager.HashPassword(userr.password),
                Phone = userr.phone,
                Status = "Inactive",
                CreatedOn = DateTime.Now,
                UpdatedOn = DateTime.Now,
                UpdatedBy = "User"




            };
            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetUser", new { id = user.ID }, user.Fullname);
        }

        // DELETE: api/Users/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var user = await _context.Users.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            _context.Users.Remove(user);
            await _context.SaveChangesAsync();

            return Ok(user);
        }

        private bool UserExists(int id)
        {
            return _context.Users.Any(e => e.ID == id);
        }

        //method to count active and inactive users
        [HttpGet("count")]
        public IActionResult GetUsersCount()
        {
            var activeUsers = _context.Users.Where(u => u.Status == "Active").Count();
            var inactiveUsers = _context.Users.Where(u => u.Status == "Inactive").Count();
            return Ok(new { ActiveUsers = activeUsers, InactiveUsers = inactiveUsers });
        }

    }
}
