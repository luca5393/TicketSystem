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
using System.Text.Json;
using System.Threading.Tasks;

namespace TicketSystem.Server.Controllers
{

    [ApiController]
    [Route("[controller]")]
    [EnableCors("AllowVueApp")]
    public class TicketController : ControllerBase
    {
        private readonly string _supabaseJwtSecret = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJpc3MiOiJzdXBhYmFzZSIsInJlZiI6InVkcnp5bWhndm9pbmJucm9tYnVqIiwicm9sZSI6InNlcnZpY2Vfcm9sZSIsImlhdCI6MTczMDQ0NTgxMCwiZXhwIjoyMDQ2MDIxODEwfQ.X0xb6y_oVhXoHQmDHBbQfOnJVFSGt2m3sNmFzrwr0F8";

        private Supabase.Client _supabaseClient = new SupabaseConnector().GetSupabaseClient();

        private Validator _validator = new Validator();

        [HttpPost("createTicket")]
        public async Task<IActionResult> CreateTicket([FromBody] Ticket ticket, [FromHeader(Name = "Authorization")] string authHeader)
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

                try
                {
                    var model = new Ticket
                    {
                        Creator_id = ticket.Creator_id, 
                        Role_id = ticket.Role_id,
                        Product_id = ticket.Product_id,
                        Priority = ticket.Priority,
                        Title = ticket.Title,
                        Desc = ticket.Desc,
                    };

                    await _supabaseClient.From<Ticket>().Insert(model);

                    return Ok(new { message = "Ticket created successfully" });
                }
                catch (Exception ex)
                {
                    return Unauthorized(new { message = "There was an error creating the ticket", error = ex.Message });
                }
            }
            catch (Exception ex)
            {
                return Unauthorized(new { message = "There was an error trying to validate the token or getting the user", error = ex.Message });
            }
        }

        [HttpPost("removeTicket")]
        public async Task<IActionResult> RemoveTicket(int id, [FromHeader(Name = "Authorization")] string authHeader)
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
            
                try
                {
                    await _supabaseClient
                    .From<Ticket>()
                    .Where(x => x.Id == id && (x.Creator_id == user.Id || user.Role_id > 0))
                    .Delete();

                    return Ok(new { message = "Ticket removed successfully" });
                }
                catch (Exception ex)
                {
                    return Unauthorized(new { message = "There was an error editing the ticket", error = ex });
                }
            }
            catch (Exception ex)
            {
                return Unauthorized(new { message = "There was an error trying to validate the token or getting the user", error = ex.Message });
            }
        }

        [HttpPost("changeTicket")]
        public async Task<IActionResult> ChangeTicket([FromBody] Ticket ticket, [FromHeader(Name = "Authorization")] string authHeader)
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
            
                try
                {
                    var update = await _supabaseClient
                      .From<Ticket>()
                      .Where(x => x.Id == ticket.Id)
                      .Set(x => x.Creator_id, ticket.Creator_id)
                      .Set(x => x.Role_id, ticket.Role_id)
                      .Set(x => x.Product_id, ticket.Product_id)
                      .Set(x => x.Priority, ticket.Priority)
                      .Set(x => x.Title, ticket.Title)
                      .Set(x => x.Desc, ticket.Desc)
                      .Set(x => x.Answer, ticket.Answer)
                      .Update();

                    return Ok(new { message = "Ticket updated successfully" });
                }
                catch (Exception ex)
                {
                    return Unauthorized(new { message = "There was an error editing the ticket", error = ex.Message });
                }
            }
            catch (Exception ex)
            {
                return Unauthorized(new { message = "There was an error trying to validate the token or getting the user", error = ex.Message });
            }
        }

        [HttpPost("ticketToQNA")]
        public async Task<IActionResult> TicketToQNA(int id, [FromHeader(Name = "Authorization")] string authHeader)
        {
            try
            {
                User user = await _validator.validateTokenAndGetUser(authHeader.Substring("Bearer ".Length).Trim());
                if (user == null)
                {
                    return Unauthorized(new { message = "Could not get user." });
                }
                
                try
                {
                    var result = await _supabaseClient.From<Ticket>().Where(x => x.Id == id).Get();
                    var ticketList = result.Models.Select(ticket => new TicketViewModel
                    {
                        Id = ticket.Id,
                        Creator_id = ticket.Creator_id,
                        Role_id = ticket.Role_id,
                        Product_id = ticket.Product_id,
                        Priority = ticket.Priority,
                        Title = ticket.Title,
                        Desc = ticket.Desc,
                        Answer = ticket.Answer,
                        Status = ticket.Status
                    }).ToList();

                    if (ticketList.First().Status == "open")
                    {
                        return Unauthorized(new { message = "This ticket is not closed and an therefore not be made into a qna" });
                    }
                    
                    if (user.Role_id != ticketList.First().Role_id)
                    {
                        return Unauthorized(new { message = "You do not have permissions to fetch this ticket" });
                    }
                    
                    var model = new QNA
                    {
                        Product_id = ticketList.First().Product_id,
                        Title = ticketList.First().Title,
                        Question = ticketList.First().Desc,
                        Answer = ticketList.First().Answer
                    };

                    await _supabaseClient.From<QNA>().Insert(model);
                    
                    await _supabaseClient
                        .From<Ticket>()
                        .Where(x => x.Id == id)
                        .Delete();

                    return Ok(new { message = "Ticked moved to qna successfully", tickets = ticketList });
                }
                catch (Exception ex)
                {
                    return Unauthorized(new { message = "There was an error fetching the ticketlist", error = ex.Message });
                }
            }
            catch (Exception ex)
            {
                return Unauthorized(new { message = "There was an error trying to validate the token or getting the user", error = ex.Message });
            }
        }

        [HttpGet("roleTicketList")]
        public async Task<IActionResult> RoleTicketList([FromHeader(Name = "Authorization")] string authHeader)
        {
            try
            {
                
                User user = await _validator.validateTokenAndGetUser(authHeader.Substring("Bearer ".Length).Trim());
                if (user == null)
                {
                    return Unauthorized(new { message = "Could not get user." });
                }

                if (user.Role_id != 0)
                {
                    try
                    {
                        var result = await _supabaseClient.From<Ticket>().Where(x => x.Role_id == user.Role_id).Order("priority", Supabase.Postgrest.Constants.Ordering.Ascending).Get();
                        var ticketList = result.Models.Select(ticket => new TicketViewModel
                        {
                            Id = ticket.Id,
                            Creator_id = ticket.Creator_id,
                            Role_id = ticket.Role_id,
                            Product_id = ticket.Product_id,
                            Priority = ticket.Priority,
                            Title = ticket.Title,
                            Desc = ticket.Desc,
                            Answer = ticket.Answer
                        }).ToList();

                        return Ok(new { message = "Success", tickets = ticketList });
                    }
                    catch (Exception ex)
                    {
                        return Unauthorized(new { message = "There was an error fetching the ticketlist", error = ex.Message });
                    }
                }

                return Ok(new { message = "A base user cannot view tickets that aren't your own" });
            }
            catch (Exception ex)
            {
                return Unauthorized(new { message = "There was an error trying to validate the token or getting the user", error = ex.Message });
            }
        }

        [HttpGet("userTicketList")]
        public async Task<IActionResult> UserTicketList([FromHeader(Name = "Authorization")] string authHeader)
        {
            try
            {
                User user = await _validator.validateTokenAndGetUser(authHeader.Substring("Bearer ".Length).Trim());
                if (user == null)
                {
                    return Unauthorized(new { message = "Could not get user." });
                }

                try
                {
                    var result = await _supabaseClient.From<Ticket>().Where(x => x.Creator_id == user.Id).Order("created_at", Supabase.Postgrest.Constants.Ordering.Ascending).Get();
                    var ticketList = result.Models.Select(ticket => new TicketViewModel
                    {
                        Id = ticket.Id,
                        Creator_id = ticket.Creator_id,
                        Role_id = ticket.Role_id,
                        Product_id = ticket.Product_id,
                        Priority = ticket.Priority,
                        Title = ticket.Title,
                        Desc = ticket.Desc,
                        Answer = ticket.Answer
                    }).ToList();

                    if (ticketList.Count == 0)
                    {
                        return Ok(new { message = "You have no tickets", tickets = ticketList });
                    }

                    return Ok(new { message = "Fetched tickets successfully", tickets = ticketList });
                }
                catch (Exception ex)
                {
                    return Unauthorized(new { message = "There was an error fetching the ticketlist", error = ex.Message });
                }
            }
            catch (Exception ex)
            {
                return Unauthorized(new { message = "There was an error trying to validate the token or getting the user", error = ex.Message });
            }
        }

        [HttpGet("ticket")]
        public async Task<IActionResult> Ticket(int id, [FromHeader(Name = "Authorization")] string authHeader)
        {
            try
            {
                User user = await _validator.validateTokenAndGetUser(authHeader.Substring("Bearer ".Length).Trim());
                if (user == null)
                {
                    return Unauthorized(new { message = "Could not get user." });
                }

                try
                {
                    var result = await _supabaseClient.From<Ticket>().Where(x => x.Id == id).Get();
                    var ticketList = result.Models.Select(ticket => new TicketViewModel
                    {
                        Id = ticket.Id,
                        Creator_id = ticket.Creator_id,
                        Role_id = ticket.Role_id,
                        Product_id = ticket.Product_id,
                        Priority = ticket.Priority,
                        Title = ticket.Title,
                        Desc = ticket.Desc,
                        Answer = ticket.Answer
                    }).ToList();

                    if (user.Id != ticketList.First().Creator_id)
                    {
                        if (user.Role_id != ticketList.First().Role_id)
                        {
                            return Unauthorized(new { message = "You do not have permissions to fetch this ticket" });
                        }
                    }

                    return Ok(new { message = "Fetched tickets successfully", tickets = ticketList });
                }
                catch (Exception ex)
                {
                    return Unauthorized(new { message = "There was an error fetching the ticketlist", error = ex.Message });
                }
            }
            catch (Exception ex)
            {
                return Unauthorized(new { message = "There was an error trying to validate the token or getting the user", error = ex.Message });
            }
        }
    }

    public class TicketViewModel
    {
        public int Id { get; set; }
        public string Creator_id { get; set; }
        public int Role_id { get; set; }
        public int Product_id { get; set; }
        public int Priority { get; set; }
        public string Title { get; set; }
        public string Status { get; set; }
        public string Desc { get; set; }
        public string Answer { get; set; }
    }

    [Table("tickets")]
    public class Ticket : BaseModel
    {
        [PrimaryKey("id", false)]
        public int Id { get; set; }

        [Column("creator_id")]
        public string Creator_id { get; set; }

        [Column("role_id")]
        public int Role_id { get; set; }

        [Column("product_id")]
        public int Product_id { get; set; }

        [Column("priority")]
        public int Priority { get; set; }

        [Column("title")]
        public string Title { get; set; }

        [Column("status")]
        public string Status { get; set; }

        [Column("desc")]
        public string Desc { get; set; }

        [Column("answer")]
        public string Answer { get; set; }
    }
}
