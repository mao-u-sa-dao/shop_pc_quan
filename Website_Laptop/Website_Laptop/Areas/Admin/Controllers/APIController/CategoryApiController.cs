using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Website_Laptop.Models;

namespace Website_Laptop.Areas.Admin.Controllers.APIController
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
            if (ModelState.IsValid)
            {
                db.PcLoaiSps.Add(category);
                await db.SaveChangesAsync();
                return CreatedAtAction("GetCategory", new { id = category.MaLoai }, category);
            }
            return BadRequest(ModelState);
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> EditLoaiSp(string id, PcLoaiSp pcloaiSp)
        {
            var loaiSp = db.PcLoaiSps.SingleOrDefault(x => x.MaLoai == id);
            loaiSp.MaLoai = pcloaiSp.MaLoai;
            loaiSp.TenLoai = pcloaiSp.TenLoai;
            db.Update(loaiSp);
            await db.SaveChangesAsync();
            return Ok();
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteLoaiSp(string id)
        {
            var loaiSp = db.PcLoaiSps.FirstOrDefault(x => x.MaLoai == id);
            var sanPham = db.PcDanhMucSps.Where(x => x.MaLoai == id);
            if (sanPham != null && sanPham.Any())
            {
                return BadRequest("Không thể xóa loại sản phẩm có sản phẩm liên quan.");
            }
            db.Remove(loaiSp);
            await db.SaveChangesAsync();
            return Ok();
        }
    }
}
