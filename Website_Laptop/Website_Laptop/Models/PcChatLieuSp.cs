using System;
using System.Collections.Generic;

namespace Website_Laptop.Models;

public partial class PcChatLieuSp
{
    public string MaChatLieu { get; set; } = null!;

    public string? TenChatLieu { get; set; }

    public virtual ICollection<PcDanhMucSp> PcDanhMucSps { get; set; } = new List<PcDanhMucSp>();
}
