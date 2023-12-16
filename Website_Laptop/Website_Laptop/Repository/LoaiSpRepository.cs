using Website_Laptop.Models;

namespace Website_Laptop.Repository
{
        public class LoaiSpRepository : ILoaiSpRepository
        {
            private readonly QliBanPcContext _context;
            public LoaiSpRepository(QliBanPcContext context)
            {
                _context = context;
            }
            public PcLoaiSp Add(PcLoaiSp loaisp)
            {
                _context.PcLoaiSps.Add(loaisp);
                _context.SaveChanges();
                return loaisp;
            }

            public PcLoaiSp Delete(string maloaiSp)
            {
                throw new NotImplementedException();
            }

            public IEnumerable<PcLoaiSp> GetALLloaiSp()
            {
                return _context.PcLoaiSps;
            }

            public PcLoaiSp GetLoaiSp(string maloaiSp)
            {
                return _context.PcLoaiSps.Find(maloaiSp);
            }

            public PcLoaiSp Update(PcLoaiSp loaiSp)
            {
                _context.Update(loaiSp);
                _context.SaveChanges();
                return loaiSp;
            }
        }
}
