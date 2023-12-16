using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Website_Laptop.Models;

namespace Website_Laptop.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("admincart")]
    [Route("admin/cart/admin")]
    public class AdminCartController : Controller
    {
        private readonly QliBanPcContext db = new QliBanPcContext();
        [Route("")]
        [Route("index")]
        public IActionResult Index()
        {
            var donHang = db.DonhangPcs.OrderBy(x => x.Id).ToList();
            return View(donHang);
        }
        [Route("")]
        [Route("viewcart")]
        public IActionResult ViewCart(string maDonhang)
        {
            var donHangDetails = db.DetailDonhangPcs
                .Where(d => d.MaDonHang == maDonhang)
                .Include(d => d.MaSpNavigation)
                .ToList();
            return View(donHangDetails);
        }
        public IActionResult Delete()
        {
            var donHang = db.DonhangPcs.OrderBy(x => x.Id).ToList();
            return View(donHang);
        }
    }
}
