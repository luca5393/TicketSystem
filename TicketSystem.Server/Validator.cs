using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using TicketSystem.Server.Controllers;
using Supabase;
using Supabase.Postgrest.Attributes;
using Supabase.Postgrest.Models;

public class Validator
{
    private readonly string _supabaseJwtSecret = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJpc3MiOiJzdXBhYmFzZSIsInJlZiI6InVkcnp5bWhndm9pbmJucm9tYnVqIiwicm9sZSI6InNlcnZpY2Vfcm9sZSIsImlhdCI6MTczMDQ0NTgxMCwiZXhwIjoyMDQ2MDIxODEwfQ.X0xb6y_oVhXoHQmDHBbQfOnJVFSGt2m3sNmFzrwr0F8"; // Your secret key

    private Supabase.Client _supabaseClient = new SupabaseConnector().GetSupabaseClient();

    // Method to validate token and retrieve user
    public async Task<User> validateTokenAndGetUser(string token)
    {
        Supabase.Gotrue.User user = await _supabaseClient.AdminAuth(_supabaseJwtSecret).GetUser(token);
        var result = await _supabaseClient.From<User>().Where(x => x.Id == user.Id).Get();
        return result.Models[0];
    }
}

public class UserViewModel
{
    public string Id { get; set; }
    public int Role_id { get; set; }
    public string Username { get; set; }

}

[Table("users")]
public class User : BaseModel
{
    [PrimaryKey("id", false)]
    public string Id { get; set; }

    [Column("role")]
    public int Role_id { get; set; }

    [Column("username")]
    public string Username { get; set; }

}