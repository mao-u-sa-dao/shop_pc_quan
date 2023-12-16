using System;
using System.Collections.Generic;

namespace Website_Laptop.Models;

public partial class PcUser
{
    public string MaUser { get; set; } = null!;

    public string AccountNameUser { get; set; } = null!;

    public string PassWordUser { get; set; } = null!;

    public string GmailUser { get; set; } = null!;

    public byte? LoaiUser { get; set; }

    public virtual AccountInfor? AccountInfor { get; set; }
}
