namespace Website_Laptop.Models.ShopingCart.ViewModels
{
    public class CartItemViewModel
    {
        public List<CartItemModel> Items { get; set; }
        public decimal GrandTotal {  get; set; }
    }
}
