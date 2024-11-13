using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Supabase;
using System.IdentityModel.Tokens.Jwt;
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

        private Validator _validator = new Validator();
        /*
        // POST api/login
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest loginRequest)
        {
            try
            {
                // Sign in the user with Supabase Auth
                var authResponse = await _supabaseClient.Auth.SignIn(loginRequest.Email, loginRequest.Password);

                if (authResponse.User == null)
                {
                    return Unauthorized(new { message = "Invalid login credentials." });
                }

                // Return the access token and user details
                return Ok(new
                {
                    message = "Login successful",
                    accessToken = authResponse.AccessToken,     
                    refreshToken = authResponse.RefreshToken,
                    userEmail = authResponse.User?.Email
                });
            }
            catch (Exception ex)
            {
                return Unauthorized(new { message = "Invalid login credentials or an error occurred." });
            }
        }

        // GET api/user
        [HttpGet("user")]
        public async Task<IActionResult> GetUserData([FromHeader(Name = "Authorization")] string authHeader)
        {
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

            return Ok(new { message = "Authenticated user", userEmail = user.Email });
        }

        // POST api/user/data
        [HttpPost("user/data")]
        public async Task<IActionResult> PostUserData([FromHeader(Name = "Authorization")] string authHeader)
        {
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
    }

    // Login request model
    public class LoginRequest
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
