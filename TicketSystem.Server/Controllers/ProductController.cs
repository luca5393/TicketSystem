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
using Newtonsoft.Json;
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

        [HttpGet("productList")]
        public async Task<IActionResult> ProductList()
        {
            var result = await _supabaseClient.From<Product>().Order("id", Supabase.Postgrest.Constants.Ordering.Ascending).Get();
            var productList = result.Models.Select(product => new ProductViewModel
            {
                Id = product.Id,
                Name = product.Name,
                Desc = product.Desc,
                Price = product.Price
            }).ToList();

            return Ok(new { message = "Success", products = productList });
        }

        [HttpPost("productSLA")]
        public async Task<IActionResult> ProductSLA([FromBody] Product Product)
        {
            var result = await _supabaseClient.From<SLA>().Where(x => x.Product_id == Product.Id).Get();
            var sla = result.Models.Select(sla => new SLAViewModel
            {
                Product_Id = sla.Product_id,
                Uptime = sla.Uptime,
                Resolution_Time = sla.Resolution_time,
                Respone_Time = sla.Response_time
            }).ToList();

            return Ok(new { message = "Success", sla = sla });
        }

        [HttpPost("productQNAList")]
        public async Task<IActionResult> ProductQNAList([FromBody] Product Product)
        {
            var result = await _supabaseClient.From<QNA>().Where(x => x.Product_id == Product.Id).Get();
            var qna = result.Models.Select(qna => new QNAViewModel
            {
                Id = qna.Id,
                Product_id = qna.Product_id,
                Title = qna.Title,
                Question = qna.Question,
                Answer = qna.Answer
            }).ToList();

            return Ok(new { message = "Success", qna = qna });
         }

    }
    public class SLAViewModel
    {
        public int Product_Id { get; set; }

        public string Uptime { get; set; }

        public string Resolution_Time { get; set; }

        public string Respone_Time { get; set; }
    }

    public class ProductViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Desc { get; set; }

        public int Price { get; set; }
    }

    public class QNAViewModel
    {
        public int Id { get; set; }

        public int Product_id { get; set; }

        public string Title { get; set; }

        public string Question { get; set; }

        public string Answer { get; set; }
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
        public int Price { get; set; }
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
        public string Response_time { get; set; }
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
