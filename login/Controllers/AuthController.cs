using login.models;
using Microsoft.AspNetCore.Mvc;

namespace login.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly LoginDbContext _context;

        public AuthController(LoginDbContext context)
        {
            _context = context;
        }

        [HttpPost("register")]
        public async Task<IActionResult> RegisterUser([FromBody] User user)
        {
            if (user == null)
            {
                return BadRequest("Invalid user data");
            }

            // Check if username or email already exists
            if (_context.User.Any(u => u.Username == user.Username || u.Email == user.Email))
            {
                return Conflict("Username or email already exists");
            }

            _context.User.Add(user);
            await _context.SaveChangesAsync();

            return Ok("User registered successfully");
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginCredentials credentials)
        {
            if (credentials == null)
            {
                return BadRequest("Invalid login credentials");
            }

            var user = _context.User.FirstOrDefault(u => u.Username == credentials.Username && u.Password == credentials.Password);
            if (user == null)
            {
                return Unauthorized("Invalid username or password");
            }

            // For demonstration purposes, generating a simple token
            var token = Guid.NewGuid().ToString();

            return Ok(new { Token = token });
        }
    }
}
