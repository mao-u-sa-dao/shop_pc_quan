using System;
using System.Collections.Generic;

namespace Website_Laptop.Models;

public partial class DonhangPc
{
    public int Id { get; set; }

    public string MaDonHang { get; set; } = null!;

    public string MaUser { get; set; } = null!;

    public string FirstAndLastName { get; set; } = null!;

    public string PhoneNumber { get; set; } = null!;

    public string AddressDonhang { get; set; } = null!;

    public DateTime CreateDate { get; set; }

    public int StatusDonhang { get; set; }
}
