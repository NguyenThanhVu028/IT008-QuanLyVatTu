using System;
using System.Collections.Generic;

namespace PMQuanLyVatTu.Models;

public partial class Warehouse
{
    public string MaKho { get; set; } = null!;

    public string? LoaiVatTu { get; set; }

    public string? DiaChi { get; set; }

    public bool? DaXoa { get; set; }

    public DateTime? ThoiGianXoa { get; set; }

    public virtual ICollection<ExportRequest> ExportRequests { get; set; } = new List<ExportRequest>();

    public virtual ICollection<GoodsDeliveryNote> GoodsDeliveryNotes { get; set; } = new List<GoodsDeliveryNote>();

    public virtual ICollection<GoodsReceivedNote> GoodsReceivedNotes { get; set; } = new List<GoodsReceivedNote>();

    public virtual ICollection<Supply> Supplies { get; set; } = new List<Supply>();
}
