using System;
using System.Collections.Generic;

namespace PMQuanLyVatTu.Models;

public partial class ImportRequest
{
    public string MaYcn { get; set; } = null!;

    public string? MaNv { get; set; }

    public string? MaVt { get; set; }

    public int? SoLuong { get; set; }

    public DateTime? NgayLap { get; set; }

    public string? GhiChu { get; set; }

    public string? TrangThai { get; set; }

    public bool? DaXoa { get; set; }

    public DateTime? ThoiGianXoa { get; set; }

    public virtual Employee? MaNvNavigation { get; set; }

    public virtual Supply? MaVtNavigation { get; set; }
}
