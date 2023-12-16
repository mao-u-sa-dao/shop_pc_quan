using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Website_Laptop.Models;

namespace Website_Laptop.Controllers.Access
{
    public class AccessController : Controller
    {
        private readonly QliBanPcContext db = new QliBanPcContext();
        [HttpGet]
        public IActionResult Login()
        {
            if (HttpContext.Session.GetString("AccountNameUser") == null)
            {
                return View();
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }
        [HttpPost]
        public IActionResult Login(PcUser user)
        {
            //var userGiohang = db.PcUsers.AsNoTracking().Where(x => x.MaUser == maUser);
            if (HttpContext.Session.GetString("MaUser") == null)
            {
                var u = db.PcUsers.Where(x => x.AccountNameUser.Equals(user.AccountNameUser) &&
                x.PassWordUser.Equals(user.PassWordUser)).FirstOrDefault();
                if (u != null)
                {
                    HttpContext.Session.SetString("AccountNameUser", u.AccountNameUser.ToString());
                    HttpContext.Session.SetString("MaUser", u.MaUser.ToString());
                    HttpContext.Session.SetInt32("LoaiUser", Convert.ToInt32(u.LoaiUser));
                    if (HttpContext.Session.GetInt32("LoaiUser") == 0)
                    {
                        return RedirectToAction("Index", "Home");
                    }
                    return RedirectToAction("Index", "Admin");
                }
            }

            return View();
        }
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            HttpContext.Session.Remove("AccountNameUser");
            return RedirectToAction("Index", "Home");
        }

    }
}
