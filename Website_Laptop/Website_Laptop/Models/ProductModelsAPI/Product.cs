namespace Website_Laptop.Models.ProductModelsAPI
{
    public class Product
    {
        public string MaSp { get; set; } = null!;

        public string? TenSp { get; set; }
        public string? MaLoai { get; set; }

        public string? AnhDaiDien { get; set; }
        public decimal? GiaSp { get; set; }

    }
}
