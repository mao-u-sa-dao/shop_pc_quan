using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Website_Laptop.Models;
using Website_Laptop.Models.ShopingCart;
using Website_Laptop.Models.ShopingCart.ViewModels;
using Website_Laptop.Repository;

namespace Website_Laptop.Controllers.Shoppingcart
{
    public class CheckoutController : Controller
    {
        private readonly QliBanPcContext db = new QliBanPcContext();
        public IActionResult Index()
        {
            List<CartItemModel> cartItems = HttpContext.Session.GetJson<List<CartItemModel>>("Cart") ?? new List<CartItemModel>();
            CartItemViewModel cartItemViewModel = new()
            {
                Items = cartItems,
                GrandTotal = cartItems.Sum(x => x.SoLuong * x.DonGia)
            };
            return View(cartItemViewModel);
        }
        public async Task<IActionResult> Checkout()
        {
            var user = db.PcUsers.FirstOrDefault();
            if(user != null)
            {
                var maUser = user.MaUser;
                var maDonhang = Guid.NewGuid().ToString();
                var donHang = new DonhangPc();
                donHang.MaDonHang = maDonhang;
                donHang.MaUser = maUser;
                donHang.FirstAndLastName = HttpContext.Request.Form["Hoten"];
                donHang.PhoneNumber = HttpContext.Request.Form["Sodienthoai"];
                donHang.AddressDonhang = HttpContext.Request.Form["Diachi"];
                donHang.CreateDate = DateTime.Now;
                donHang.StatusDonhang = 1;
                db.Add(donHang);
                db.SaveChanges();
                List<CartItemModel> cartItems = HttpContext.Session.GetJson<List<CartItemModel>>("Cart") ?? new List<CartItemModel>();
                foreach(var cart in  cartItems)
                {
                    var detailDonhang = new DetailDonhangPc();
                    detailDonhang.MaUser = maUser;
                    detailDonhang.MaDonHang= maDonhang;
                    detailDonhang.MaSp = cart.MaSp;
                    detailDonhang.SoLuong = cart.SoLuong;
                    detailDonhang.DonGia = cart.DonGia;
                    db.Add(detailDonhang);
                    db.SaveChanges();
                }
                HttpContext.Session.Remove("Cart");
                TempData["success"] = "Đơn hàng đã được tạo thành công, vui lòng chờ duyệt đơn hàng";
                return RedirectToAction("Index", "Cart");
            }
            return View();
        }
    }
}
