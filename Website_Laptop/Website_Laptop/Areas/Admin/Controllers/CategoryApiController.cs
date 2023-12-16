using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Website_Laptop.Models;

namespace Website_Laptop.Areas.Admin.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryApiController : ControllerBase
    {
        private readonly QliBanPcContext db = new QliBanPcContext();
        [HttpGet]
        public IEnumerable<PcLoaiSp> GetAllLoaiSp()
        {
            var category = db.PcLoaiSps.ToList();
            return category;
        }
        [HttpPost]
        public async Task<ActionResult<PcLoaiSp>> AddLoaiSp(PcLoaiSp category)
        {
            if(ModelState.IsValid)
            {
                db.PcLoaiSps.Add(category);
                await db.SaveChangesAsync();
                return CreatedAtAction("GetCategory", new {id = category.MaLoai}, category);
            }
            return BadRequest(ModelState);
        }
    }
}
