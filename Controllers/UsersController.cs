using EduCoreSuite.Models; //To be able to access the User model
using EduCoreSuite.Data; //To access the ApplicationDbContext
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore; 
using Microsoft.AspNetCore.Identity; //For password hashing and user management

namespace EduCoreSuite.Controllers
{
    [ApiController]
    [Route("api/[controller]")] 
    public class UsersController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        public UsersController(ApplicationDbContext context)
        {
            _context = context; //Instance of the database context to interact with the database
        }
        [HttpPost("register")] 
        public async Task<IActionResult> Register(User user) //returns HTTP responses. It has a method that accepts JSON data 
        {
            if (!ModelState.IsValid) //Check if the incoming data is valid
            {
                return BadRequest(ModelState); //If not valid, return a bad request with the model state errors
            }

            if (await _context.Users.AnyAsync(u => u.Email == user.Email))
                return BadRequest(new { message = "Email already exists." }); //Error message that email already exists

            if (await _context.Users.AnyAsync(u => u.Username == user.Username))
                return BadRequest(new { message = "Username already exists." }); //Error message that username already exists

            //Hashing the password
            var hasher = new PasswordHasher<User>();
            user.Password = hasher.HashPassword(user, user.Password); //Hash the password before saving it to the database

            _context.Users.Add(user); 
            await _context.SaveChangesAsync(); //Save changes to the database
            return CreatedAtAction(nameof(Register), new { id = user.ID }, new
            {
                user.ID,
                user.Username,
                user.FirstName,
                user.LastName,
                user.Email,
                Role = user.Role?.Name //Return the role name if it exists
            }); 
        }

    }
}
