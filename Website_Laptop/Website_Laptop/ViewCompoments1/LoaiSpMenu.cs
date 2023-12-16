using Website_Laptop.Models;
using Microsoft.AspNetCore.Mvc;
using Website_Laptop.Repository;
namespace Website_Laptop.ViewCompoments1
{
    public class LoaiSpMenu : ViewComponent
    {
        private readonly ILoaiSpRepository _loaiSp;
        public LoaiSpMenu(ILoaiSpRepository loaiSpRepository)
        {
            _loaiSp = loaiSpRepository;
        }
        public IViewComponentResult Invoke()
        {
            var loaisp = _loaiSp.GetALLloaiSp().OrderBy(x => x.TenLoai);
            return View(loaisp);
        }

    }
}
