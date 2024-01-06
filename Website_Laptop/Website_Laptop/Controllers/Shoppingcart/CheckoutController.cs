using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Website_Laptop.Models;
using Website_Laptop.Models.ShopingCart;
using Website_Laptop.Models.ShopingCart.ViewModels;
using Website_Laptop.Repository;
using Website_Laptop.Services;
using Website_Laptop.ViewModel;

namespace Website_Laptop.Controllers.Shoppingcart
{
    public class CheckoutController : Controller
    {
        private readonly QliBanPcContext db = new QliBanPcContext();
        private IVnPayService _vnPayService;

        public CheckoutController(IVnPayService vnPayService)
        {
            _vnPayService = vnPayService;
        }
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
        public async Task<IActionResult> Checkout(IFormCollection form)
        {
            try {
                List<CartItemModel> cartItems = HttpContext.Session.GetJson<List<CartItemModel>>("Cart") ?? new List<CartItemModel>();
                var tongTien = (int)cartItems.Sum(x => x.SoLuong * x.DonGia);
                if (HttpContext.Session.GetString("AccountNameUser") != null)
                {
                    
                    var maUser = HttpContext.Session.GetString("MaUser");
                    var maDonhang = Guid.NewGuid().ToString();
                    var donHang = new DonhangPc();
                    donHang.MaDonHang = maDonhang;
                    donHang.MaUser = maUser;
                    donHang.CreateDate = DateTime.Now;
                    donHang.StatusDonhang = 1;
                    db.Add(donHang);
                    db.SaveChanges();
                    foreach (var cart in cartItems)
                    {
                        var detailDonhang = new DetailDonhangPc();
                        detailDonhang.MaUser = maUser;
                        detailDonhang.MaDonHang = maDonhang;
                        detailDonhang.MaSp = cart.MaSp;
                        detailDonhang.SoLuong = cart.SoLuong;
                        detailDonhang.DonGia = cart.DonGia;
                        db.Add(detailDonhang);
                        db.SaveChanges();
                    }
                    var hoaDon = new PcHoaDon();
                    hoaDon.MaDonHang = maDonhang;
                    hoaDon.TenKhachHang = HttpContext.Request.Form["Hoten"];
                    hoaDon.DiaChi = HttpContext.Request.Form["Diachi"];
                    hoaDon.SoDienThoai = HttpContext.Request.Form["Sodienthoai"];
                    hoaDon.SoTienThanh = tongTien;
                    hoaDon.GhiChu = HttpContext.Request.Form["GhiChu"];
                    if (form["payment"].Contains("cash"))
                    {
                        hoaDon.CachThanhToan = "Thanh toán khi nhận hàng";
                        db.Add(hoaDon);
                        db.SaveChanges();
                        HttpContext.Session.Remove("Cart");
                        TempData["success"] = "Đơn hàng đã được tạo thành công, vui lòng chờ duyệt đơn hàng";
                    }
                    else if (form["payment"].Contains("bank"))
                    {
                        hoaDon.CachThanhToan = "Chuyển khoản";
                        
                        var vnPayModel = new VnPaymentRequestModel
                        {
                            Amount = tongTien,
                            CreatedDate = DateTime.Now,
                            Description = $"{HttpContext.Request.Form["Hoten"]}, {HttpContext.Request.Form["Sodienthoai"]}",
                            FullName = HttpContext.Request.Form["Hoten"],
                            OrderId = new Random().Next(1000, 100000)

                        };
                        db.Add(hoaDon);
                        db.SaveChanges();
                        HttpContext.Session.Remove("Cart");
                        return Redirect(_vnPayService.CreatePaymentUrl(HttpContext, vnPayModel));
                    }
                    
                    
                    
                }
                return View();
            }
            catch(Exception e)
            {
                TempData["error"] = "Có lỗi xảy ra trong quá trình xử lý đơn hàng. Vui lòng thử lại.";
                return RedirectToAction("Index", "Cart");
            }
        }
        public IActionResult PaymentFail()
        {
            return View();
        }
        public IActionResult PaymentSuccess()
        {
            return View();
        }
        public IActionResult PaymentCallBack(IFormCollection form)
        {
            var response = _vnPayService.PaymentExecute(Request.Query);
            if (response == null || response.VnPayResponseCode != "00")
            {
                TempData["success"] = $"Lỗi thanh toán VnPay: {response.VnPayResponseCode}";
                return RedirectToAction("PaymentFail");
            }
            
            TempData["success"] = $"Thanh toán VnPay thành công!";
            return RedirectToAction("PaymentSuccess");
        }
    }

}

