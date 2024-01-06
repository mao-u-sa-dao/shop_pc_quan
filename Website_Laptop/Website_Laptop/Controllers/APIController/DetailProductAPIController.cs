using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Website_Laptop.Models;
using Website_Laptop.Models.ProductModelsAPI;
using Website_Laptop.ViewModel;

namespace Website_Laptop.Controllers.APIController
{
    [Route("api/[controller]")]
    [ApiController]
    public class DetailProductAPIController : ControllerBase
    {
        private readonly QliBanPcContext db = new QliBanPcContext();
        [HttpGet]
        public IActionResult ProductDetail(string maSp)
        {
            var sanPham = (from t1 in db.PcDanhMucSps
                           join t2 in db.PcAnhSps on t1.MaSp equals t2.MaSp
                           where t1.MaSp == maSp
                           select new 
                           {
                               TenSp = t1.TenSp,
                               AnhDaiDien = t1.AnhDaiDien,
                               GiaSp = t1.GiaSp,
                               CanNang = t1.CanNang,
                               GioiThieuSp = t1.GioiThieuSp,
                               TenFileAnh = t2.TenFileAnh
                           });
            return Ok(sanPham.ToList());
        }
    }
}
