
using Website_Laptop.Models;

namespace Website_Laptop.Repository
{
    public interface ILoaiSpRepository
    {
        PcLoaiSp Add(PcLoaiSp loai);
        PcLoaiSp Update(PcLoaiSp loaiSp);
        PcLoaiSp Delete(string maloaiSp);
        PcLoaiSp GetLoaiSp(string maloaiSp);
        IEnumerable<PcLoaiSp> GetALLloaiSp();
    }
}




