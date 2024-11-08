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
    [EnableCors("AllowAll")]
    public class ApiController : ControllerBase
    {
        private readonly string _supabaseUrl = "https://udrzymhgvoinbnrombuj.supabase.co"; // Replace with your Supabase URL
        private readonly string _supabaseKey = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJpc3MiOiJzdXBhYmFzZSIsInJlZiI6InVkcnp5bWhndm9pbmJucm9tYnVqIiwicm9sZSI6ImFub24iLCJpYXQiOjE3MzA0NDU4MTAsImV4cCI6MjA0NjAyMTgxMH0.8e3KcbTlAyrfFMZmdvSYqV_AvcGVCXp2CCllPdWfYWk"; // Replace with your Supabase API Key
        private readonly string _supabaseJwtSecret = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJpc3MiOiJzdXBhYmFzZSIsInJlZiI6InVkcnp5bWhndm9pbmJucm9tYnVqIiwicm9sZSI6InNlcnZpY2Vfcm9sZSIsImlhdCI6MTczMDQ0NTgxMCwiZXhwIjoyMDQ2MDIxODEwfQ.X0xb6y_oVhXoHQmDHBbQfOnJVFSGt2m3sNmFzrwr0F8"; // Your Supabase JWT secret
        
        private Supabase.Client _supabaseClient;
         
        public ApiController()
        {
            // Initialize Supabase client 
            _supabaseClient = new Supabase.Client(_supabaseUrl, _supabaseKey);
        }

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
            var validatedToken = ValidateToken(token);
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
            var validatedToken = ValidateToken(token);
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

        private ClaimsPrincipal ValidateToken(string token)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.UTF8.GetBytes(_supabaseJwtSecret); // Use the secret key for token validation

            try
            {
                var validationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,  // Set to true and specify Issuer if necessary
                    ValidateAudience = false, // Set to true and specify Audience if necessary
                    ValidateLifetime = true,  // Ensure the token has not expired
                    ClockSkew = TimeSpan.Zero // Optionally, remove clock skew for immediate expiration
                };

                var principal = tokenHandler.ValidateToken(token, validationParameters, out SecurityToken validatedToken);

                // Check if token is expired by comparing ValidTo with current UTC time
                if (validatedToken.ValidTo < DateTime.UtcNow)
                {
                    return null;  // Token has expired
                }

                return principal;
            }
            catch
            {
                return null;  // Token validation failed
            }
        }
    }

    // Login request model
    public class LoginRequest
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
