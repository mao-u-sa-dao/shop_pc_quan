using System;
using System.Collections.Generic;

namespace Website_Laptop.Models;

public partial class PcLoaiSp
{
    public string MaLoai { get; set; } = null!;

    public string? TenLoai { get; set; }

    public virtual ICollection<PcDanhMucSp> PcDanhMucSps { get; set; } = new List<PcDanhMucSp>();
}
