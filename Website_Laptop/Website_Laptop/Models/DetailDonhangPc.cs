using System;
using System.Collections.Generic;

namespace Website_Laptop.Models;

public partial class DetailDonhangPc
{
    public int Id { get; set; }

    public string MaDonHang { get; set; } = null!;

    public string MaUser { get; set; } = null!;

    public string MaSp { get; set; } = null!;

    public decimal DonGia { get; set; }

    public int SoLuong { get; set; }

    public virtual PcDanhMucSp MaSpNavigation { get; set; } = null!;
}
