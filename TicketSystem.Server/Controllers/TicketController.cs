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

        // POST
        [HttpPost("createTicket")]
        public async Task<IActionResult> CreateTicket([FromBody] Ticket ticket, [FromHeader(Name = "Authorization")] string authHeader)
        {
            try
            {

                // Retrieve the user's information
                User user = await _validator.validateTokenAndGetUser(authHeader.Substring("Bearer ".Length).Trim());
                if (user == null) 
                {
                    return Unauthorized(new { message = "Could not get user." });
                }
                if (user.Role_id == 0)
                {
                    return Unauthorized(new { message = "No permission" });
                }

                var model = new Ticket
                {
                    Creator_id = ticket.Creator_id,       //user.Id Example value for creator ID
                    Role_id = ticket.Role_id,          // Example value for role ID
                    Product_id = ticket.Product_id,       // Example value for product ID
                    Priority = ticket.Priority,         // Example value for priority
                    Title = ticket.Title,  // Your title value
                    Desc = ticket.Desc, // Example body text
                };


                await _supabaseClient.From<Ticket>().Insert(model);

                // Return the access token and user details
                return Ok(new
                {
                    message = "Successful",
                });
            }
            catch (Exception ex)
            {
                return Unauthorized(new { message = "Invalid login credentials or an error occurred." + ex });
            }
        }

        // POST
        [HttpPost("removeTicket")]
        public async Task<IActionResult> RemoveTicket([FromBody] Ticket ticket, [FromHeader(Name = "Authorization")] string authHeader)
        {
            /*
            // Retrieve the user's information
            User user = await _validator.validateTokenAndGetUser(authHeader.Substring("Bearer ".Length).Trim());
            if (user == null)
            {
                return Unauthorized(new { message = "Could not get user." });
            }
            if (user.Role_id == 0)
            {
                return Unauthorized(new { message = "No permission" });
            }
            */
            //TODO: add so own user can delete ticket

            await _supabaseClient
              .From<Ticket>()
              .Where(x => x.Id == ticket.Id)
              .Delete();

            // Perform some action with the authenticated user
            return Ok(new { message = "Data received from authenticated user" });
        }

        // POST
        [HttpPost("changeTicket")]
        public async Task<IActionResult> ChangeTickete([FromBody] Ticket ticket)//, [FromHeader(Name = "Authorization")] string authHeader)
        {
            /*
            // Retrieve the user's information
            User user = await _validator.validateTokenAndGetUser(authHeader.Substring("Bearer ".Length).Trim());
            if (user == null)
            {
                return Unauthorized(new { message = "Could not get user." });
            }
            if (user.Role_id == 0)
            {
                return Unauthorized(new { message = "No permission" });
            }
            */
            //TODO: ADD CONFIRMATION THAT THE Ticket is the actual ticket


            //TODO: Add so it updates all of the tickets table not just role
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


            // Perform some action with the authenticated user
            return Ok(new { message = "Data received from authenticated user" });
        }

        // POST
        [HttpPost("ticketToQNA")]
        public async Task<IActionResult> TicketToQNA([FromHeader(Name = "Authorization")] string authHeader)
        {
            /*
            // Retrieve the user's information
            User user = await _validator.validateTokenAndGetUser(authHeader.Substring("Bearer ".Length).Trim());
            if (user == null)
            {
                return Unauthorized(new { message = "Could not get user." });
            }
            if (user.Role_id == 0)
            {
                return Unauthorized(new { message = "No permission" });
            }
            */
            //QNA CONVERT LOGIC




            // Perform some action with the authenticated user
            return Ok(new { message = "Data received from authenticated user" });
        }

        // GET
        [HttpGet("roleTicketList")]
        public async Task<IActionResult> RoleTicketList([FromHeader(Name = "Authorization")] string authHeader)
        {
            User user = _validator.validateTokenAndGetUser(authHeader.Substring("Bearer ".Length).Trim());
            if (user == null)
            {
                return Unauthorized(new { message = "Could not get user." });
            }

            if (user.Role_id != 0)
            {
                var result = await _supabaseClient.From<Ticket>().Where(x => x.Role_id == user.Role_id).Order("priority", Supabase.Postgrest.Constants.Ordering.Ascending).Get();
                var ticketList = result.Models.Select(ticket => new Ticket
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

            return Ok(new { message = "No tickets assigned to your role" });
        }

        [HttpGet("userTicketList")]
        public async Task<IActionResult> UserTicketList([FromHeader(Name = "Authorization")] string authHeader)
        {

            // Retrieve the user's information
            User user = await _validator.validateTokenAndGetUser(authHeader.Substring("Bearer ".Length).Trim());
            if (user == null)
            {
                return Unauthorized(new { message = "Could not get user." });
            }

            if (user.Role_id != 0)
            {
                var result = await _supabaseClient.From<Ticket>().Where(x => x.Creator_id == user.Id).Order("created_at", Supabase.Postgrest.Constants.Ordering.Ascending).Get();
                var ticketList = result.Models.Select(ticket => new Ticket
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

            return Ok(new { message = "No tickets assigned to your role" });
        }

        [HttpGet("ticket")]
        public async Task<IActionResult> Ticket(int id)//, [FromHeader(Name = "Authorization")] string authHeader)
        {
            /*
            // Retrieve the user's information
            User user = _validator.validateTokenAndGetUser(authHeader.Substring("Bearer ".Length).Trim());
            if (user == null)
            {
                return Unauthorized(new { message = "Could not get user." });
            }
            */
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

            return Ok(new { message = "Success", tickets = ticketList });
        }

        //TODO - MAKE A METHOD TO FETCH A SINGLE TICKET TO EDIT IT
    }

    public class TicketViewModel
    {
        public int Id { get; set; }
        public string Creator_id { get; set; }
        public int Role_id { get; set; }
        public int Product_id { get; set; }
        public int Priority { get; set; }
        public string Title { get; set; }
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

        [Column("desc")]
        public string Desc { get; set; }

        [Column("answer")]
        public string Answer { get; set; }
    }

}
