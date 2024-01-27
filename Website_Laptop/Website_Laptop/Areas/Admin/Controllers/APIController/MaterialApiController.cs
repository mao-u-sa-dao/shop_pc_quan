using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Website_Laptop.Models;
using Website_Laptop.Models.Authentication;
namespace Website_Laptop.Areas.Admin.Controllers.APIController
{
    [Route("api/[controller]")]
    [ApiController]
    public class MaterialApiController : ControllerBase
    {
        private readonly QliBanPcContext db = new QliBanPcContext();
        [Authentication]
        [HttpGet]
        public IEnumerable<PcChatLieuSp> GetAllMaterial()
        {
            var material = db.PcChatLieuSps.ToList();
            return material;
        }
        [Authentication]
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
        [Authentication]
        [HttpPut("{id}")]
        public async Task<ActionResult<PcDanhMucSp>> UpdateMaterial( [FromBody] PcChatLieuSp pcChatLieuSp)
        {
            var chatLieu = db.PcChatLieuSps.SingleOrDefault(x => x.MaChatLieu == pcChatLieuSp.MaChatLieu);
            if (chatLieu == null)
            {
                return NotFound();
            }
            chatLieu.MaChatLieu = pcChatLieuSp.MaChatLieu;
            chatLieu.TenChatLieu = pcChatLieuSp.TenChatLieu;
            db.Update(chatLieu);
            await db.SaveChangesAsync();
            return Ok();

        }
        [Authentication]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMaterial(string id)
        {
            var chatLieu = db.PcChatLieuSps.FirstOrDefault(x => x.MaChatLieu == id);
            db.Remove(chatLieu);
            await db.SaveChangesAsync();
            return Ok();
        }
    }

}
