using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Supabase;
using Supabase.Postgrest.Attributes;
using Supabase.Postgrest.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Reflection;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace TicketSystem.Server.Controllers
{

    [ApiController]
    [Route("[controller]")]
    [EnableCors("AllowVueApp")]
    public class ApiController : ControllerBase
    {
        private readonly string _supabaseJwtSecret = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJpc3MiOiJzdXBhYmFzZSIsInJlZiI6InVkcnp5bWhndm9pbmJucm9tYnVqIiwicm9sZSI6InNlcnZpY2Vfcm9sZSIsImlhdCI6MTczMDQ0NTgxMCwiZXhwIjoyMDQ2MDIxODEwfQ.X0xb6y_oVhXoHQmDHBbQfOnJVFSGt2m3sNmFzrwr0F8";

        private Supabase.Client _supabaseClient = new SupabaseConnector().GetSupabaseClient();

<<<<<<< HEAD
        [HttpPost("signup")]
        public async Task<IActionResult> Signup([FromBody] UserSignIn user)
=======
        private Validator _validator = new Validator();
        /*
        // POST api/login
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest loginRequest)
>>>>>>> main
        {
            if (user == null || string.IsNullOrEmpty(user.password) || string.IsNullOrEmpty(user.username) || string.IsNullOrEmpty(user.email))
            {
                return BadRequest(new { message = "Invalid user data." });
            }
            var session = await _supabaseClient.Auth.SignUp(user.email, user.password);
            if (session == null) 
            {
                return BadRequest(new { message = "Database fail" });
            }

            var id = session.User.Id;
            var newUser = new User
            {
                id = id,
                role = 0,
                username = user.username,
            };

            await _supabaseClient.From<User>().Insert(newUser);
            return Ok(new { message = "User created successfully", userId = user.username });
        }



        [Table("users")]
        public class User : BaseModel
        {
            [PrimaryKey("id", false)]
            public string id { get; set; }

            [Column("role")]
            public int role { get; set; }

<<<<<<< HEAD
            [Column("username")]
            public string username { get; set; }
=======
            // Validate the JWT token manually
            var validatedToken = _validator.ValidateToken(token);
            if (validatedToken == null)
            {
                return Unauthorized(new { message = "Invalid or expired token." });
            }

            // Retrieve the user's information
            var user = await _supabaseClient.Auth.GetUser(_supabaseJwtSecret);
            if (user == null)
            {
                return Unauthorized(new { message = "Supabase user not found." });
            }

            return Ok(new { message = "Authenticated user", userEmail = user.Email });
>>>>>>> main
        }

        public class UserSignIn
        {
<<<<<<< HEAD
            public int role { get; set; }
            public string username { get; set; }
            public string password { get; set; }
            public string email { get; set; }
        }

        // Login request model
        public class LoginRequest
        {
            public string Email { get; set; }
            public string Password { get; set; }
        }
=======
            if (string.IsNullOrEmpty(authHeader) || !authHeader.StartsWith("Bearer "))
            {
                return Unauthorized(new { message = "Missing or invalid Authorization header." });
            }

            var token = authHeader.Substring("Bearer ".Length).Trim();

            // Validate the JWT token manually
            var validatedToken = _validator.ValidateToken(token);
            if (validatedToken == null)
            {
                return Unauthorized(new { message = "Invalid or expired token." });
            }

            // Retrieve the user's information
            var user = await _supabaseClient.Auth.GetUser(_supabaseJwtSecret);
            if (user == null)
            {
                return Unauthorized(new { message = "Supabase user not found." });
            }

            // Perform some action with the authenticated user
            return Ok(new { message = "Data received from authenticated user", userEmail = user.Email });
        }
        */
>>>>>>> main
    }
}
