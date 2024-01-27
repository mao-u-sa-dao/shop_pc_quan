using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Website_Laptop.Models;
using Website_Laptop.Models.Authentication;
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
        [Authentication]
        [Route("")]
        [Route("index")]
        public IActionResult Index()
        {
            return View();
        }
        // DANH MỤC SẢN PHẨM
        [Authentication]
        [HttpGet]
        [Route("danhmucsanpham")]
        public IActionResult DanhMucSanPham(int? page)
        {
            int pageSize = 10;
            int pageNumBer = page == null || page < 0 ? 1 : page.Value;
            var lstSanPham = db.PcDanhMucSps.AsNoTracking().OrderBy(x => x.TenSp);
            PagedList<PcDanhMucSp> lst = new PagedList<PcDanhMucSp>(lstSanPham, pageNumBer, pageSize);
            return View(lst);
        }
        [Authentication]
        [HttpGet]
        [Route("themsanphammoi")]
        public IActionResult ThemSanPhamMoi()
        {
            ViewBag.MaChatLieu = new SelectList(db.PcChatLieuSps.ToList(), "MaChatLieu", "TenChatLieu");
            ViewBag.MaQuocGiaSX = new SelectList(db.PcQuocGiaSxes.ToList(), "MaQuocGiaSx", "TenQuocGiaSx");
            ViewBag.MaLoai = new SelectList(db.PcLoaiSps.ToList(), "MaLoai", "TenLoai");
            return View();
        }
        [Authentication]
        [HttpPost]
        [Route("themsanphammoi")]
        public async Task<IActionResult> ThemSanPhamMoi([FromForm] IFormFile AnhDaiDien)
        {

            //lấy tên
            var fileName = Path.GetFileName(AnhDaiDien.FileName);
            //lưu trữ vào thư mục của project
            var subFolderName = "product";
            var mainFolderPath = Path.Combine(_webHostEnvironment.WebRootPath, "img");
            var subFolderPath1 = Path.Combine(mainFolderPath, subFolderName);
            var filePath = Path.Combine(subFolderPath1, fileName);
            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await AnhDaiDien.CopyToAsync(stream);
            }
            return RedirectToAction("DanhMucSanPham", "Admin");

        }
        [Authentication]
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
        [Authentication]
        [HttpPost]
        [Route("suasanpham")]
        public async Task<IActionResult> UpdateProduct([FromForm] IFormFile AnhDaiDien)
        {
            if (AnhDaiDien != null)
            {
                //lấy tên
                var fileName = Path.GetFileName(AnhDaiDien.FileName);
                //lưu trữ vào thư mục của project

                var subFolderName = "product";
                var mainFolderPath = Path.Combine(_webHostEnvironment.WebRootPath, "img");
                var subFolderPath1 = Path.Combine(mainFolderPath, subFolderName);
                var filePath = Path.Combine(subFolderPath1, fileName);
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await AnhDaiDien.CopyToAsync(stream);
                }
            }
            return RedirectToAction("DanhMucSanPham", "Admin");
        }


        // LOẠI SẢN PHẨM
        [Authentication]
        [Route("loaisanpham")]
        public IActionResult LoaiSanPham()
        {
            var lstLoai = db.PcLoaiSps.AsNoTracking().OrderBy(x => x.TenLoai);
            return View(lstLoai);
        }
        [Authentication]
        [HttpGet]
        [Route("themloaisanpham")]
        public IActionResult AddLoai()
        {
            return View();
        }
        [Authentication]
        [HttpGet]
        [Route("sualoaisanpham")]
        public IActionResult EditLoai(string maLoai)
        {
            var loaiSp = db.PcLoaiSps.Find(maLoai);
            return View(loaiSp);
        }

        // CHẤT LIỆU SẢN PHẨM
        [Authentication]
        [Route("chatlieusanpham")]
        public IActionResult ChatLieuSanPham(int? page)
        {

            var lstChatLieu = db.PcChatLieuSps.AsNoTracking().OrderBy(x => x.TenChatLieu);
            return View(lstChatLieu);
        }
        [Authentication]
        [HttpGet]
        [Route("themchatlieu")]
        public IActionResult AddChatLieu()
        {
            return View();
        }
        [Authentication]
        [HttpGet]
        [Route("suachatlieu")]
        public IActionResult EditChatLieu(string maChatLieu)
        {
            var chatLieu = db.PcChatLieuSps.Find(maChatLieu);
            return View(chatLieu);
        }


        // QUỐC GIA
        [Authentication]
        [Route("quocgia")]
        public IActionResult QuocGia()
        {
            var lstQuocGia = db.PcQuocGiaSxes.AsNoTracking().OrderBy(x => x.TenQuocGiaSx);

            return View(lstQuocGia);
        }
        [Authentication]
        [HttpGet]
        [Route("themquocgia")]
        public IActionResult AddQuocGia()
        {
            return View();
        }
        [Authentication]
        [HttpGet]
        [Route("suaquocgia")]
        public IActionResult EditQuocGia(string id)
        {
            var quocGia = db.PcQuocGiaSxes.Find(id);
            return View(quocGia);
        }

        // QUẢN LÝ USER
        [Authentication]
        [Route("quanlyuser")]
        public IActionResult QuanLyUser()
        {

            var lstUser = db.PcUsers.AsNoTracking().OrderBy(x => x.AccountNameUser);
            return View(lstUser);
        }
        [Authentication]
        [HttpGet]
        [Route("themuser")]
        public IActionResult AddUser()
        {
            return View();
        }

        [Authentication]
        [HttpGet]
        [Route("suauser")]
        public IActionResult EditUser(string maUser)
        {
            var user = db.PcUsers.FirstOrDefault(x => x.MaUser == maUser);
            return View(user);
        }
        [Authentication]
        [HttpGet]
        [Route("thongtinuser")]
        public IActionResult DetailUser(string maUser)
        {
            var user = db.PcUsers.FirstOrDefault(x=>x.MaUser == maUser);
            return View(user);
        }
        // ẢNH SẢN PHẨM
        [Authentication]
        [Route("anhsanpham")]
        public IActionResult AnhSanPham()
        {

            var sanPham = db.PcDanhMucSps.AsNoTracking().OrderBy(x => x.MaSp).ToList();
            return View(sanPham);
        }
        [Authentication]
        [HttpGet]
        [Route("themanhsanpham")]
        public IActionResult AddAnhSp(string maSp)
        {
            ViewBag.MaspValue = maSp;
            return View();
        }
        [Authentication]
        [HttpPost]
        [Route("themanhsanpham")]
        public async Task<IActionResult> AddAnhSp([FromForm] IFormFile TenFileAnh)
        {
            //lấy tên
            var fileName = Path.GetFileName(TenFileAnh.FileName);
            //lưu trữ vào thư mục của project
            var subFolderName = "product";
            var subFolderName1 = "details";
            var mainFolderPath = Path.Combine(_webHostEnvironment.WebRootPath, "img");
            var subFolderPath1 = Path.Combine(mainFolderPath, subFolderName);
            var subFolderPath2 = Path.Combine(subFolderPath1, subFolderName1);

            var filePath = Path.Combine(subFolderPath2, fileName);
            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await TenFileAnh.CopyToAsync(stream);
            }
            return RedirectToAction("AnhSanPham", "Admin");
        }
        [Authentication]
        [HttpGet]
        [Route("suaanhsanpham")]
        public IActionResult EditAnhSp(string maAnhSp)
        {
            var anhSp = db.PcAnhSps.SingleOrDefault(x=>x.MaAnhSp == maAnhSp);
            return View(anhSp);
        }
        [Authentication]
        [HttpPost]
        [Route("suaanhsanpham")]
        public async Task<IActionResult> EditAnhSp([FromForm] IFormFile TenFileAnh)
        {
            if (TenFileAnh != null)
            {
                //lấy tên
                var fileName = Path.GetFileName(TenFileAnh.FileName);
                //lưu trữ vào thư mục của project

                var subFolderName = "product";
                var subFolderName1 = "details";
                var mainFolderPath = Path.Combine(_webHostEnvironment.WebRootPath, "img");
                var subFolderPath1 = Path.Combine(mainFolderPath, subFolderName);
                var subFolderPath2 = Path.Combine(subFolderPath1, subFolderName1);

                var filePath = Path.Combine(subFolderPath2, fileName);
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await TenFileAnh.CopyToAsync(stream);
                }
            }
            return RedirectToAction("AnhSanPham", "Admin");
        }
        [Authentication]
        [HttpGet]
        [Route("detailsanhsp")]
        public IActionResult DetailsAnhSp(string maSp)
        {
            var lstAnhSp = db.PcAnhSps.Where(x=>x.MaSp == maSp).Include(x=>x.MaSpNavigation).ToList();
            return View(lstAnhSp);
        }




    }
}
