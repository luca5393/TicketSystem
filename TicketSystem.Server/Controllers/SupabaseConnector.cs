using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Supabase;

namespace TicketSystem.Server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [EnableCors("AllowVueApp")]
    public class SupabaseConnector
    {
        private readonly string _supabaseUrl = "https://udrzymhgvoinbnrombuj.supabase.co";
        private readonly string _supabaseKey = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJpc3MiOiJzdXBhYmFzZSIsInJlZiI6InVkcnp5bWhndm9pbmJucm9tYnVqIiwicm9sZSI6ImFub24iLCJpYXQiOjE3MzA0NDU4MTAsImV4cCI6MjA0NjAyMTgxMH0.8e3KcbTlAyrfFMZmdvSYqV_AvcGVCXp2CCllPdWfYWk";

        private Supabase.Client supabase;
        public SupabaseConnector()
        {
            supabase = new Supabase.Client(_supabaseUrl, _supabaseKey);
        }

        public Supabase.Client GetSupabaseClient()
        {
            return supabase;
        }
    }
}
