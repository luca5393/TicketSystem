﻿using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Supabase;
using Supabase.Postgrest.Attributes;
using Supabase.Postgrest.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Reflection;
using System.Security.Claims;
using System.Text;
using Newtonsoft.Json;
using System.Threading.Tasks;

namespace TicketSystem.Server.Controllers
{

    [ApiController]
    [Route("[controller]")]
    [EnableCors("AllowVueApp")]
    public class UserController : ControllerBase
    {
        private readonly string _supabaseJwtSecret = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJpc3MiOiJzdXBhYmFzZSIsInJlZiI6InVkcnp5bWhndm9pbmJucm9tYnVqIiwicm9sZSI6InNlcnZpY2Vfcm9sZSIsImlhdCI6MTczMDQ0NTgxMCwiZXhwIjoyMDQ2MDIxODEwfQ.X0xb6y_oVhXoHQmDHBbQfOnJVFSGt2m3sNmFzrwr0F8";

        private Supabase.Client _supabaseClient = new SupabaseConnector().GetSupabaseClient();

        private Validator _validator = new Validator();

        // GET
        [HttpGet("userList")]
        public async Task<IActionResult> UserList([FromHeader(Name = "Authorization")] string authHeader)
        {
            try
            {
                User user = await _validator.validateTokenAndGetUser(authHeader.Substring("Bearer ".Length).Trim());
                if (user == null)
                {
                    return Unauthorized(new { message = "Could not get user." });
                }
                if (user.Role_id == 0)
                {
                    return Unauthorized(new { message = "No permission" });
                }
                
                var result = await _supabaseClient.From<UserData>().Order("username", Supabase.Postgrest.Constants.Ordering.Ascending).Get();
                var userList = result.Models.Select(user => new UserDataViewModel
                {
                    Id = user.Id,
                    Username = user.Username
                }).ToList();

                return Ok(new { message = "Success", users = userList });
            }
            catch (Exception ex)
            {
                return Unauthorized(new { message = "There was an error trying to validate the token or getting the user", error = ex.Message });
            }
        }

        [HttpPost("roleNameList")]
        public async Task<IActionResult> RoleNameList(List<int> id)
        {
            List<RoleViewModel> role = new List<RoleViewModel>();

            foreach (int i in id)
            {
                var result = await _supabaseClient.From<Role>().Where(x => x.Id == i).Get();
                role.AddRange(result.Models.Select(role => new RoleViewModel
                {
                    Id = role.Id,
                    Name = role.Name
                }).ToList());
            }

            return Ok(new { message = "Success", role = role });
        }
    }

    public class UserDataViewModel
    {
        public string Id { get; set; }
        public string Username { get; set; }
    }

    [Table("users")]
    public class UserData : BaseModel
    {
        [PrimaryKey("id", false)]
        public string Id { get; set; }

        [Column("username")]
        public string Username { get; set; }
    }

    public class RoleViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Desc { get; set; }
    }

    [Table("roles")]
    public class Role : BaseModel
    {
        [PrimaryKey("id", true)]
        public int Id { get; set; }

        [Column("name")]
        public string Name { get; set; }

        [Column("desc")]
        public string Desc { get; set; }
    }
}
