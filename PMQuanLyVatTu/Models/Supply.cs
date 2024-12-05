using System;
using System.Collections.Generic;

namespace PMQuanLyVatTu.Models;

public partial class Supply
{
    public string MaVt { get; set; } = null!;

    public string? MaNcc { get; set; }

    public string? MaKho { get; set; }

    public string? TenVatTu { get; set; }

    public string? LoaiVatTu { get; set; }

    public string? DonViTinh { get; set; }

    public int? GiaNhap { get; set; }

    public int? GiaXuat { get; set; }

    public int? SoLuongTonKho { get; set; }

    public string? ImageLocation { get; set; }

    public bool? DaXoa { get; set; }

    public DateTime? ThoiGianXoa { get; set; }

    public virtual ICollection<ExportRequestInfo> ExportRequestInfos { get; set; } = new List<ExportRequestInfo>();

    public virtual ICollection<GoodsDeliveryNoteInfo> GoodsDeliveryNoteInfos { get; set; } = new List<GoodsDeliveryNoteInfo>();

    public virtual ICollection<GoodsReceivedNoteInfo> GoodsReceivedNoteInfos { get; set; } = new List<GoodsReceivedNoteInfo>();

    public virtual ICollection<ImportRequest> ImportRequests { get; set; } = new List<ImportRequest>();

    public virtual Warehouse? MaKhoNavigation { get; set; }

    public virtual Supplier? MaNccNavigation { get; set; }
}
