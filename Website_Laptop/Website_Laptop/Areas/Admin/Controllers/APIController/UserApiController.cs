using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Website_Laptop.Models.Access;
using Website_Laptop.Models;
using Website_Laptop.Models.Authentication;
using Microsoft.EntityFrameworkCore;

namespace Website_Laptop.Areas.Admin.Controllers.APIController
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserApiController : ControllerBase
    {
        private readonly QliBanPcContext db = new QliBanPcContext();
        [HttpGet]
        public async Task<ActionResult<PcUser>> GetAllUser()
        {
            var user = db.PcUsers.AsNoTracking().OrderBy(x=>x.MaUser).ToList();
            return Ok(user);
        }
        [HttpPost]
        public async Task<ActionResult<Userpc>> AddUser(Userpc userpc)
        {

            if (ModelState.IsValid)
            {
                var user = new PcUser
                {
                    MaUser = userpc.MaUser,
                    AccountNameUser = userpc.AccountNameUser,
                    PassWordUser = userpc.PassWordUser,
                    GmailUser = userpc.GmailUser,
                    LoaiUser = userpc.LoaiUser,
                };
                db.PcUsers.Add(user);
                await db.SaveChangesAsync();
                return Ok();
            }
            return BadRequest(ModelState);
        }
        [HttpPut("{id}")]
        public async Task<ActionResult<Userpc>> EditUser(Userpc userpc, string id)
        {
            var user = db.PcUsers.SingleOrDefault(x => x.MaUser == id);
            if (user == null)
            {
                return NotFound();
            }    
            user.AccountNameUser = userpc.AccountNameUser;
            user.GmailUser = userpc.GmailUser;
            user.PassWordUser = userpc.PassWordUser;
            user.LoaiUser = userpc.LoaiUser;
            db.Update(user);
            await db.SaveChangesAsync();
            return Ok();
        }
        [Authentication]
        [HttpDelete("{id}")]
        public async Task<ActionResult<PcUser>> DeleteUser(string id)
        {
            var user = db.PcUsers.SingleOrDefault(x=> x.MaUser == id);
            if (user == null)
            {
                return NotFound();
            }
            db.Remove(user);
            await db.SaveChangesAsync();
            return Ok();
        }
    }
}
