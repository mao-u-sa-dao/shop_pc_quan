using System;
using System.Collections.Generic;

namespace Website_Laptop.Models;

public partial class PcHoaDon
{
    public int Id { get; set; }

    public string MaDonHang { get; set; } = null!;

    public string TenKhachHang { get; set; } = null!;

    public string DiaChi { get; set; } = null!;

    public string SoDienThoai { get; set; } = null!;

    public decimal SoTienThanh { get; set; }

    public string CachThanhToan { get; set; } = null!;

    public string GhiChu { get; set; } = null!;
}
