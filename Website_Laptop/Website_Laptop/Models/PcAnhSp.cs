using System;
using System.Collections.Generic;

namespace Website_Laptop.Models;

public partial class PcAnhSp
{
    public string MaAnhSp { get; set; } = null!;

    public string MaSp { get; set; } = null!;

    public string? TenFileAnh { get; set; }

    public short? ViTri { get; set; }

    public virtual PcDanhMucSp MaSpNavigation { get; set; } = null!;
}
