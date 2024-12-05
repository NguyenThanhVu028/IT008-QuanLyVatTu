using System;
using System.Collections.Generic;

namespace PMQuanLyVatTu.Models;

public partial class GoodsReceivedNote
{
    public string MaPn { get; set; } = null!;

    public string? MaNv { get; set; }

    public string? MaNcc { get; set; }

    public string? KhoNhap { get; set; }

    public DateTime? NgayLap { get; set; }

    public string? LyDoNhap { get; set; }

    public int? ChietKhau { get; set; }

    public int? Vat { get; set; }

    public int? TongGia { get; set; }

    public string? TrangThai { get; set; }

    public bool? DaXoa { get; set; }

    public DateTime? ThoiGianXoa { get; set; }

    public virtual ICollection<GoodsReceivedNoteInfo> GoodsReceivedNoteInfos { get; set; } = new List<GoodsReceivedNoteInfo>();

    public virtual Warehouse? KhoNhapNavigation { get; set; }

    public virtual Supplier? MaNccNavigation { get; set; }

    public virtual Employee? MaNvNavigation { get; set; }
}
