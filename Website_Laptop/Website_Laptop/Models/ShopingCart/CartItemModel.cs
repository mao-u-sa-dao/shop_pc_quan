namespace Website_Laptop.Models.ShopingCart
{
    public class CartItemModel
    {
        public string MaSp { get; set; }

        public string TenSp { get; set; }
        public int SoLuong { get; set; }
        public decimal DonGia { get; set; }
        public string AnhDaiDien { get; set; }
        public decimal TongTien {

            get { return SoLuong * DonGia; }

        }
        public CartItemModel()
        {

        }
        public CartItemModel(PcDanhMucSp danhMucSp)
        {
            MaSp = danhMucSp.MaSp;
            TenSp = danhMucSp.TenSp;
            SoLuong = 1;
            DonGia = danhMucSp.GiaSp;
            AnhDaiDien = danhMucSp.AnhDaiDien;

        }
    }
}
