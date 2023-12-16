using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Website_Laptop.Models;

namespace Website_Laptop.Areas.Admin.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminProductApiController : ControllerBase
    {
        private readonly QliBanPcContext db = new QliBanPcContext();
        [HttpPost]
        public async Task<ActionResult<PcDanhMucSp>> AddProduct(PcDanhMucSp product)
        {
            if (ModelState.IsValid)
            {
                db.PcDanhMucSps.Add(product);
                await db.SaveChangesAsync();
                return CreatedAtAction("GetProduct", new { id = product.MaSp }, product);
            }
            return BadRequest(ModelState);
        }
    }
}
