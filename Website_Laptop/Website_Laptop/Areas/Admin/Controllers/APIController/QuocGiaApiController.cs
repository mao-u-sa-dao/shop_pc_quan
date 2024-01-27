using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Website_Laptop.Models;
using Website_Laptop.Models.Authentication;

namespace Website_Laptop.Areas.Admin.Controllers.APIController
{
    [Route("api/[controller]")]
    [ApiController]
    public class QuocGiaApiController : ControllerBase
    {
        private readonly QliBanPcContext db = new QliBanPcContext();
        [Authentication]
        [HttpGet]
        public IEnumerable<PcQuocGiaSx> GetAllQuocGia()
        {
            var quocGiasx = db.PcQuocGiaSxes.ToList();
            return quocGiasx;
        }
        [Authentication]
        [HttpPost]
        public async Task<ActionResult<PcQuocGiaSx>> AddQuocGia(PcQuocGiaSx quocGiaSx)
        {
            if (ModelState.IsValid)
            {
                db.PcQuocGiaSxes.Add(quocGiaSx);
                await db.SaveChangesAsync();
                return CreatedAtAction("GetQuocGia", new { id = quocGiaSx.MaQuocGiaSx }, quocGiaSx);
            }
            return BadRequest(ModelState);
        }
        [Authentication]
        [HttpPut("{id}")]

        public async Task<ActionResult<PcQuocGiaSx>> EditQuocGia(PcQuocGiaSx pcQuocGiaSx, string id)
        {
            var quocGia = db.PcQuocGiaSxes.SingleOrDefault(x => x.MaQuocGiaSx == id);
            if (quocGia == null)
            {
                return NotFound();
            }
            quocGia.MaQuocGiaSx = pcQuocGiaSx.MaQuocGiaSx;
            quocGia.TenQuocGiaSx = pcQuocGiaSx.TenQuocGiaSx;
            db.Update(quocGia);
            await db.SaveChangesAsync();
            return Ok();
        }
        [Authentication]
        [HttpDelete("{id}")]
        public async Task<ActionResult<PcQuocGiaSx>> DeleteQuocGia(string id)
        {
            var quocGia = db.PcQuocGiaSxes.FirstOrDefault(x=>x.MaQuocGiaSx==id);
            if (quocGia == null)
            {
                return NotFound();
            }
            db.Remove(quocGia);
            await db.SaveChangesAsync();
            return Ok();
        }
    }
}
