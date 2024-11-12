using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

public class Validator
{
    public ClaimsPrincipal ValidateToken(string token)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.UTF8.GetBytes("eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJpc3MiOiJzdXBhYmFzZSIsInJlZiI6InVkcnp5bWhndm9pbmJucm9tYnVqIiwicm9sZSI6InNlcnZpY2Vfcm9sZSIsImlhdCI6MTczMDQ0NTgxMCwiZXhwIjoyMDQ2MDIxODEwfQ.X0xb6y_oVhXoHQmDHBbQfOnJVFSGt2m3sNmFzrwr0F8"); // Use the secret key for token validation

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