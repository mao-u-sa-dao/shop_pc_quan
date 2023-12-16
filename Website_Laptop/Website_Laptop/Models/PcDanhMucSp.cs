using System;
using System.Collections.Generic;

namespace Website_Laptop.Models;

public partial class PcDanhMucSp
{
    public string MaSp { get; set; } = null!;

    public string? TenSp { get; set; }

    public double? CanNang { get; set; }

    public string? MaChatLieu { get; set; }

    public string? MaQuocGiaSx { get; set; }

    public string? ThoiGianBaoHanh { get; set; }

    public string? GioiThieuSp { get; set; }

    public string? MaLoai { get; set; }

    public string? AnhDaiDien { get; set; }

    public decimal GiaSp { get; set; }

    public virtual ICollection<DetailDonhangPc> DetailDonhangPcs { get; set; } = new List<DetailDonhangPc>();

    public virtual PcChatLieuSp? MaChatLieuNavigation { get; set; }

    public virtual PcLoaiSp? MaLoaiNavigation { get; set; }

    public virtual PcQuocGiaSx? MaQuocGiaSxNavigation { get; set; }

    public virtual ICollection<PcAnhSp> PcAnhSps { get; set; } = new List<PcAnhSp>();
}
