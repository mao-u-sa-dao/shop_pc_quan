using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Website_Laptop.Models;
namespace Website_Laptop.Areas.Admin.Controllers.APIController
{
    [Route("api/[controller]")]
    [ApiController]
    public class MaterialApiController : ControllerBase
    {
        private readonly QliBanPcContext db = new QliBanPcContext();
        [HttpGet]
        public IEnumerable<PcChatLieuSp> GetAllMaterial()
        {
            var material = db.PcChatLieuSps.ToList();
            return material;
        }
        [HttpPost]
        public async Task<ActionResult<PcChatLieuSp>> AddMaterial(PcChatLieuSp pcChatLieuSp)
        {
            if (ModelState.IsValid)
            {
                db.PcChatLieuSps.Add(pcChatLieuSp);
                await db.SaveChangesAsync();
                return CreatedAtAction("GetMaterial", new { id = pcChatLieuSp.MaChatLieu }, pcChatLieuSp);
            }
            return BadRequest(ModelState);
        }
    }

}
