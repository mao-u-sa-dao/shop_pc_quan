using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using Website_Laptop.Models;
using System.Collections.Generic;
using Website_Laptop.ViewModel;


namespace Website_Laptop.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
       
        QliBanPcContext db = new QliBanPcContext();
        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel1 { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        public IActionResult ProductDetail(string maSp)
        {
            var sanPham = db.PcDanhMucSps.SingleOrDefault(x => x.MaSp == maSp);
            var anhSanPham = db.PcAnhSps.Where(x=>x.MaSp == maSp).ToList();
            var homeProductDetail = new HomeProductDetalModel
            {
                danhMucSp = sanPham,
                anhSp = anhSanPham,
            };
            return View(homeProductDetail);
        }
        
    }
}