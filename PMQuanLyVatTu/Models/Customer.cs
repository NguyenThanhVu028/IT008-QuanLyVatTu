using PMQuanLyVatTu.ViewModel;
using System;
using System.Collections.Generic;

namespace PMQuanLyVatTu.Models;

public partial class Customer : BaseViewModel
{
    public string MaKh { get; set; } = null!;

    public string? HoTen { get; set; }

    public string? GioiTinh { get; set; }

    public string? Sdt { get; set; }

    public string? Email { get; set; }

    public DateOnly? NgaySinh { get; set; }

    public string? DiaChi { get; set; }

    public bool? DaXoa { get; set; }

    public DateTime? ThoiGianXoa { get; set; }

    public virtual ICollection<ExportRequest> ExportRequests { get; set; } = new List<ExportRequest>();

    public virtual ICollection<GoodsDeliveryNote> GoodsDeliveryNotes { get; set; } = new List<GoodsDeliveryNote>();
}
