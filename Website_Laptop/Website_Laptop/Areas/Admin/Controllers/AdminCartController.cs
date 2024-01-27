using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Website_Laptop.Models;
using Website_Laptop.Models.Authentication;

namespace Website_Laptop.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("admincart")]
    [Route("admin/cart/admin")]
    public class AdminCartController : Controller
    {
        private readonly QliBanPcContext db = new QliBanPcContext();
        [Authentication]
        [Route("")]
        [Route("index")]
        public IActionResult Index()
        {
            var donHang = db.DonhangPcs.OrderBy(x => x.Id).ToList();
            return View(donHang);
        }
        [Authentication]
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
        [Authentication]
        [Route("")]
        [Route("xoadonhang")]
        public IActionResult DeleteDonHang(string maDonHang)
        {
            var donHang = db.DonhangPcs.Where(x => x.MaDonHang == maDonHang);
            var detailDonHang = db.DetailDonhangPcs.Where(x=>x.MaDonHang == maDonHang).ToList();
            if(donHang == null)
            {
                return NotFound();
            }
            if(detailDonHang != null && detailDonHang.Any())
            {
                db.RemoveRange(detailDonHang);
            }
            db.RemoveRange(donHang);
            db.SaveChanges();
            return RedirectToAction("Index","AdminCart");
        }
    }
}
