using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Website_Laptop.Models;
using Website_Laptop.Models.Access;

namespace Website_Laptop.Controllers.APIController
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccessAPIController : ControllerBase
    {
        private readonly QliBanPcContext db = new QliBanPcContext();
        [HttpPost]
        public async Task<ActionResult<Userpc>> Register(Userpc userpc)
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
    }
}
