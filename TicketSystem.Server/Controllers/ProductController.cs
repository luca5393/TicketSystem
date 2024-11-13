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
    public class ProductController : ControllerBase
    {
        private readonly string _supabaseJwtSecret = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJpc3MiOiJzdXBhYmFzZSIsInJlZiI6InVkcnp5bWhndm9pbmJucm9tYnVqIiwicm9sZSI6InNlcnZpY2Vfcm9sZSIsImlhdCI6MTczMDQ0NTgxMCwiZXhwIjoyMDQ2MDIxODEwfQ.X0xb6y_oVhXoHQmDHBbQfOnJVFSGt2m3sNmFzrwr0F8";

        private Supabase.Client _supabaseClient = new SupabaseConnector().GetSupabaseClient();

        private Validator _validator = new Validator();

        // GET
        [HttpGet("productList")]
        public async Task<IActionResult> ProductList()
        {
            var result = await _supabaseClient.From<Product>().Get();
            var productList = result.Models;

            return Ok(new { message = "Succes", products = productList });
        }

        // GET
        [HttpGet("productSLA")]
        public async Task<IActionResult> ProductSLAList([FromBody] Product Product)
        {
            var result = await _supabaseClient.From<SLA>().Where(x => x.Product_id == Product.Id).Get();
            var SLA = result.Models;

            return Ok(new { message = "Succes", productSLA = SLA });
        }

        // GET
        [HttpGet("productQNAList")]
        public async Task<IActionResult> ProductQNAList([FromBody] Product Product)
        {
            var result = await _supabaseClient.From<QNA>().Where(x => x.Product_id == Product.Id).Get();
            var QNAList = result.Models;

            return Ok(new { message = "Succes", productQNAList = QNAList });
        }

    }

    [Table("products")]
    public class Product : BaseModel
    {
        [PrimaryKey("id", false)]
        public int Id { get; set; }

        [Column("name")]
        public string Name { get; set; }

        [Column("desc")]
        public string Desc { get; set; }

        [Column("price")]
        public int price { get; set; }
    }

    [Table("sla")]
    public class SLA : BaseModel
    {
        [PrimaryKey("product_id", false)]
        public int Product_id { get; set; }

        [Column("uptime")]
        public string Uptime { get; set; }

        [Column("resolution_time")]
        public string Resolution_time { get; set; }

        [Column("response_time")]
        public int Response_time { get; set; }
    }

    [Table("qna")]
    public class QNA : BaseModel
    {
        [PrimaryKey("id", false)]
        public int Id { get; set; }

        [Column("product_id")]
        public int Product_id { get; set; }

        [Column("title")]
        public string Title { get; set; }

        [Column("question")]
        public string Question { get; set; }

        [Column("answer")]
        public string Answer { get; set; }
    }

}
