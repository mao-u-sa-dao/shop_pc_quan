using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Website_Laptop.Models;
using X.PagedList;
namespace Website_Laptop.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("admin")]
    public class HomeAdminController : Controller
    {
        
        private readonly QliBanPcContext db = new QliBanPcContext();
        private readonly IWebHostEnvironment _webHostEnvironment;
        public HomeAdminController(IWebHostEnvironment webHostEnvironment)
        {
            _webHostEnvironment = webHostEnvironment;
        }
        [Route("")]
        [Route("index")]
        public IActionResult Index()
        {
            return View();
        }

        [Route("danhmucsanpham")]
        public IActionResult DanhMucSanPham(int? page)
        {
            int pageSize = 10;
            int pageNumBer = page == null || page < 0 ? 1 : page.Value;
            var lstSanPham = db.PcDanhMucSps.AsNoTracking().OrderBy(x => x.TenSp);
            PagedList<PcDanhMucSp> lst = new PagedList<PcDanhMucSp>(lstSanPham, pageNumBer, pageSize);
            return View(lst);
        }

        [HttpGet]
        [Route("themsanphammoi")]
        public IActionResult ThemSanPhamMoi()
        {
            ViewBag.MaChatLieu = new SelectList(db.PcChatLieuSps.ToList(), "MaChatLieu", "TenChatLieu");
            ViewBag.MaQuocGiaSX = new SelectList(db.PcQuocGiaSxes.ToList(), "MaQuocGiaSx", "TenQuocGiaSx");
            ViewBag.MaLoai = new SelectList(db.PcLoaiSps.ToList(), "MaLoai", "TenLoai");
            return View();
        }

        [HttpPost]
        [Route("themsanphammoi")]
        public async Task<IActionResult> ThemSanPhamMoi([FromForm] IFormFile AnhDaiDien)
        {

            //lấy tên
            var fileName = Path.GetFileName(AnhDaiDien.FileName);
            //lưu trữ vào thư mục của project
            var filePath = Path.Combine(_webHostEnvironment.WebRootPath, "img", fileName);
            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await AnhDaiDien.CopyToAsync(stream);
            }
            return RedirectToAction("DanhMucSanPham", "Admin");

        }

        [HttpGet]
        [Route("suasanpham")]
        public IActionResult UpdateProduct(string maSanPham)
        {
            ViewBag.MaChatLieu = new SelectList(db.PcChatLieuSps.ToList(), "MaChatLieu", "TenChatLieu");
            ViewBag.MaQuocGiaSx = new SelectList(db.PcQuocGiaSxes.ToList(), "MaQuocGiaSx", "TenQuocGiaSx");
            ViewBag.MaLoai = new SelectList(db.PcLoaiSps.ToList(), "MaLoai", "TenLoai");
            var sanPham = db.PcDanhMucSps.Find(maSanPham);
            return View(sanPham);
        }

        [HttpPost]
        [Route("suasanpham")]
        public async Task<IActionResult> UpdateProduct([FromForm] IFormFile AnhDaiDien)
        {
            if (AnhDaiDien != null)
            {
                //lấy tên
                var fileName = Path.GetFileName(AnhDaiDien.FileName);
                //lưu trữ vào thư mục của project
                var filePath = Path.Combine(_webHostEnvironment.WebRootPath, "img", fileName);
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await AnhDaiDien.CopyToAsync(stream);
                }
            }
            return RedirectToAction("DanhMucSanPham", "Admin");
        }
        [HttpGet]
        [Route("xoasanpham")]
        public async Task<IActionResult> DeleteProduct()
        {
            return RedirectToAction("DanhMucSanPham", "Admin");
        }
        // Loại sản phẩm
        [Route("loaisanpham")]
        public IActionResult LoaiSanPham()
        {
            var lstLoai = db.PcLoaiSps.AsNoTracking().OrderBy(x => x.TenLoai);
            return View(lstLoai);
        }
        [HttpGet]
        [Route("themloaisanpham")]
        public IActionResult AddLoai()
        {
            return View();
        }
        [HttpPost]
        [Route("themloaisanpham")]
        public IActionResult AddLoai(PcLoaiSp loaiSp)
        {
            return RedirectToAction("LoaiSanPham", "Admin");
        }
        [HttpGet]
        [Route("sualoaisanpham")]
        public IActionResult EditLoai(string maLoai)
        {
            var loaiSp = db.PcLoaiSps.Find(maLoai);
            return View(loaiSp);
        }
        [HttpPost]
        [Route("sualoaisanpham")]
        public async Task<IActionResult> EditLoai()
        {
            return RedirectToAction("LoaiSanPham", "Admin");
        }
        [HttpGet]
        [Route("xoaloai")]
        public IActionResult DeleteLoai()
        {
            return RedirectToAction("LoaiSanPham", "Admin");
        }
        // chất liệ sản phẩm
        [Route("chatlieu")]
        public IActionResult ChatLieuSanPham(int? page)
        {

            var lstChatLieu = db.PcChatLieuSps.AsNoTracking().OrderBy(x => x.TenChatLieu);
            return View(lstChatLieu);
        }
        // quốc gia
        [Route("quocgia")]
        public IActionResult QuocGia()
        {
            var lstQuocGia = db.PcQuocGiaSxes.AsNoTracking().OrderBy(x => x.TenQuocGiaSx);

            return View(lstQuocGia);
        }
        // quản lý user
        [Route("quanlyuser")]
        public IActionResult QuanLyUser()
        {

            var lstUser = db.PcUsers.AsNoTracking().OrderBy(x => x.AccountNameUser);
            return View(lstUser);
        }
        // ảnh sản phẩm
        [Route("anhsanpham")]
        public IActionResult AnhSanPham()
        {

            var lstAnh = db.PcAnhSps.AsNoTracking().OrderBy(x => x.TenFileAnh);
            return View(lstAnh);
        }





    }
}
