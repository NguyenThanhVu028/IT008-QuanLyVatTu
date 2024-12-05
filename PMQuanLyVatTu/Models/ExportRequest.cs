using System;
using System.Collections.Generic;

namespace PMQuanLyVatTu.Models;

public partial class ExportRequest
{
    public string MaYcx { get; set; } = null!;

    public string? MaNv { get; set; }

    public string? MaKh { get; set; }

    public string? KhoXuat { get; set; }

    public DateTime? NgayLap { get; set; }

    public string? GhiChu { get; set; }

    public string? TrangThai { get; set; }

    public bool? DaXoa { get; set; }

    public DateTime? ThoiGianXoa { get; set; }

    public virtual ICollection<ExportRequestInfo> ExportRequestInfos { get; set; } = new List<ExportRequestInfo>();

    public virtual Warehouse? KhoXuatNavigation { get; set; }

    public virtual Customer? MaKhNavigation { get; set; }

    public virtual Employee? MaNvNavigation { get; set; }
}
