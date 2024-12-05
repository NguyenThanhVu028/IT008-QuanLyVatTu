using System;
using System.Collections.Generic;

namespace PMQuanLyVatTu.Models;

public partial class Supplier
{
    public string MaNcc { get; set; } = null!;

    public string? TenNcc { get; set; }

    public string? Sdt { get; set; }

    public string? Email { get; set; }

    public string? DiaChi { get; set; }

    public bool? DaXoa { get; set; }

    public DateTime? ThoiGianXoa { get; set; }

    public virtual ICollection<GoodsReceivedNote> GoodsReceivedNotes { get; set; } = new List<GoodsReceivedNote>();

    public virtual ICollection<Supply> Supplies { get; set; } = new List<Supply>();
}
