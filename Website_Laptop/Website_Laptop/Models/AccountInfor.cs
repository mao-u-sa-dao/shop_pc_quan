using System;
using System.Collections.Generic;

namespace Website_Laptop.Models;

public partial class AccountInfor
{
    public string MaUser { get; set; } = null!;

    public string NameUser { get; set; } = null!;

    public string? DateOfBirthUser { get; set; }

    public string? AddressUser { get; set; }

    public virtual PcUser MaUserNavigation { get; set; } = null!;
}
