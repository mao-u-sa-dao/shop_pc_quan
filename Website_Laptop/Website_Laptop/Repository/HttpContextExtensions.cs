using Website_Laptop.Models.ShopingCart;
using Website_Laptop.Models.ShopingCart.ViewModels;

namespace Website_Laptop.Repository
{
    public static class HttpContextExtensions
    {
        public static int GetCartItemCount(this HttpContext httpContext)
        {
            List<CartItemViewModel> cart = httpContext.Session.GetJson<List<CartItemViewModel>>("Cart") ?? new List<CartItemViewModel>();
            return cart.Count();
        }
    }
}
