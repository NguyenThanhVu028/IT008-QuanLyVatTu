using System;
using System.Collections.Generic;

namespace PMQuanLyVatTu.Models;

public partial class Account
{
    public string TenDn { get; set; } = null!;

    public string? MaNv { get; set; }

    public string? MatKhau { get; set; }

    public string Email { get; set; } = null!;

    public bool? DaXoa { get; set; }

    public DateTime? ThoiGianXoa { get; set; }

    public virtual Employee? MaNvNavigation { get; set; }
}
