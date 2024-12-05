using System;
using System.Collections.Generic;

namespace PMQuanLyVatTu.Models;

public partial class GoodsDeliveryNote
{
    public string MaPx { get; set; } = null!;

    public string? MaNv { get; set; }

    public string? MaKh { get; set; }

    public string? KhoXuat { get; set; }

    public DateTime? NgayLap { get; set; }

    public string? LyDoXuat { get; set; }

    public int? ChietKhau { get; set; }

    public int? Vat { get; set; }

    public int? TongGia { get; set; }

    public string? TrangThai { get; set; }

    public bool? DaXoa { get; set; }

    public DateTime? ThoiGianXoa { get; set; }

    public virtual ICollection<GoodsDeliveryNoteInfo> GoodsDeliveryNoteInfos { get; set; } = new List<GoodsDeliveryNoteInfo>();

    public virtual Warehouse? KhoXuatNavigation { get; set; }

    public virtual Customer? MaKhNavigation { get; set; }

    public virtual Employee? MaNvNavigation { get; set; }
}
