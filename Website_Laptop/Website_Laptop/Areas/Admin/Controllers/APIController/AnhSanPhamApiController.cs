using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Website_Laptop.Models.Authentication;
using Website_Laptop.Models;

namespace Website_Laptop.Areas.Admin.Controllers.APIController
{
    [Route("api/[controller]")]
    [ApiController]
    public class AnhSanPhamApiController : ControllerBase
    {
        private readonly QliBanPcContext db = new QliBanPcContext();
        [HttpGet]
        public IEnumerable<PcAnhSp> GetAllMaterial()
        {
            var material = db.PcAnhSps.ToList();
            return material;
        }
        [Authentication]
        [HttpPost]
        public async Task<ActionResult<PcAnhSp>> AddAnhSp(PcAnhSp pcAnhSp)
        {
            if (ModelState.IsValid)
            {
                db.PcAnhSps.Add(pcAnhSp);
                await db.SaveChangesAsync();
                return CreatedAtAction("GetProduct", new { id = pcAnhSp.MaAnhSp }, pcAnhSp);
            }
            return BadRequest(ModelState);
        }
        [HttpPut("{id}")]
        public async Task<ActionResult<PcAnhSp>> EditAnhSp(PcAnhSp pcAnhSp, string id)
        {
            var anhSp = db.PcAnhSps.SingleOrDefault(x => x.MaAnhSp == id);
            anhSp.MaAnhSp = pcAnhSp.MaAnhSp;
            anhSp.MaSp = pcAnhSp.MaSp;
            anhSp.TenFileAnh = pcAnhSp.TenFileAnh;
            anhSp.ViTri = pcAnhSp.ViTri;
            db.Update(anhSp);
            await db.SaveChangesAsync();
            return Ok();

        }
        [Authentication]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAnhSp(string id)
        {
            var anhSp = db.PcAnhSps.SingleOrDefault(x => x.MaAnhSp == id);
            db.PcAnhSps.Remove(anhSp);
            await db.SaveChangesAsync();
            return Ok();
        }
    }
}
