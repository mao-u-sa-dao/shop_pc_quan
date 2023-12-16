using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Website_Laptop.Models;

namespace Website_Laptop.Areas.Admin.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QuocGiaApiController : ControllerBase
    {
        private readonly QliBanPcContext db = new QliBanPcContext();
        [HttpGet]
        public IEnumerable<PcQuocGiaSx> GetAllQuocGia()
        {
            var quocGiasx = db.PcQuocGiaSxes.ToList();
            return quocGiasx;
        }
        [HttpPost]
        public async Task<ActionResult<PcQuocGiaSx>> AddQuocGia(PcQuocGiaSx quocGiaSx)
        {
            if(ModelState.IsValid)
            {
                db.PcQuocGiaSxes.Add(quocGiaSx);
                await db.SaveChangesAsync();
                return CreatedAtAction("GetQuocGia", new { id = quocGiaSx.MaQuocGiaSx }, quocGiaSx);
            }
            return BadRequest(ModelState);
        }
    }
}
