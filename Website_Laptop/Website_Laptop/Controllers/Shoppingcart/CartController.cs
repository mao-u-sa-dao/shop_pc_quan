using Microsoft.AspNetCore.Mvc;
using Website_Laptop.Models;
using Website_Laptop.Models.ShopingCart;
using Website_Laptop.Models.ShopingCart.ViewModels;
using Website_Laptop.Repository;

namespace Website_Laptop.Controllers.Shoppingcart
{
    public class CartController : Controller
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
        public async Task<IActionResult> Add(string maSp)
        {

            PcDanhMucSp pcDanhMuc = await db.PcDanhMucSps.FindAsync(maSp);

            List<CartItemModel> cart = HttpContext.Session.GetJson<List<CartItemModel>>("Cart") ?? new List<CartItemModel>();
            CartItemModel cartItem = cart.FirstOrDefault(c => c.MaSp == maSp);

            if (cartItem == null)
            {
                cart.Add(new CartItemModel(pcDanhMuc));
            }
            else
            {
                cartItem.SoLuong += 1;
            }
            TempData["success"] = "Thêm sản phẩm vào giỏ hàng thành công";

            HttpContext.Session.SetJson("Cart", cart);
            return Redirect(Request.Headers["Referer"].ToString());
        }
        public async Task<IActionResult> Decrease(string maSp)
        {
            List<CartItemModel> cart = HttpContext.Session.GetJson<List<CartItemModel>>("Cart");
            CartItemModel cartItem = cart.Where(x => x.MaSp == maSp).FirstOrDefault();
            if(cartItem.SoLuong > 1)
            {
                --cartItem.SoLuong;
            }
            else
            {
                cart.RemoveAll(x => x.MaSp == maSp);
            }
            if(cart.Count == 0)
            {
                HttpContext.Session.Remove("Cart");
            }
            else
            {
                HttpContext.Session.SetJson("Cart", cart);
            }
            return RedirectToAction("Index");
        }
        public async Task<IActionResult> Increase(string maSp)
        {
            List<CartItemModel> cart = HttpContext.Session.GetJson<List<CartItemModel>>("Cart");
            CartItemModel cartItem = cart.Where(x => x.MaSp == maSp).FirstOrDefault();

            ++cartItem.SoLuong;
            if (cart.Count == 0)
            {
                HttpContext.Session.Remove("Cart");
            }
            else
            {
                HttpContext.Session.SetJson("Cart", cart);
            }
            return RedirectToAction("Index");
        }
        public async Task<IActionResult> Remove(string maSp)
        {
            List<CartItemModel> cart = HttpContext.Session.GetJson<List<CartItemModel>>("Cart");
            cart.RemoveAll(p=>p.MaSp == maSp);
            if (cart.Count == 0)
            {
                HttpContext.Session.Remove("Cart");
            }
            else
            {
                HttpContext.Session.SetJson("Cart", cart);
            }
            TempData["success"] = "Xóa sản phẩm khỏi giỏ hàng thành công";
            return RedirectToAction("Index");
        }
        public async Task<IActionResult> Clear()
        {
            HttpContext.Session.Remove("Cart");
            TempData["success"] = "Xóa tất cả sản phẩm trong giỏ hàng thành công";
            return RedirectToAction("Index");
        }
        
    }
}
