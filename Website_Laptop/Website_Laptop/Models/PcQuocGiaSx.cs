using System;
using System.Collections.Generic;

namespace Website_Laptop.Models;

public partial class PcQuocGiaSx
{
    public string MaQuocGiaSx { get; set; } = null!;

    public string? TenQuocGiaSx { get; set; }

    public virtual ICollection<PcDanhMucSp> PcDanhMucSps { get; set; } = new List<PcDanhMucSp>();
}
