using System;
using System.Collections.Generic;

namespace PMQuanLyVatTu.Models;

public partial class Employee
{
    public string MaNv { get; set; } = null!;

    public string? HoTen { get; set; }

    public string? ChucVu { get; set; }

    public DateOnly? NgaySinh { get; set; }

    public string? GioiTinh { get; set; }

    public string? Sdt { get; set; }

    public string? Email { get; set; }

    public string? DiaChi { get; set; }

    public int? Luong { get; set; }

    public string? ImageLocation { get; set; }

    public bool? DaXoa { get; set; }

    public DateTime? ThoiGianXoa { get; set; }

    public virtual ICollection<Account> Accounts { get; set; } = new List<Account>();

    public virtual ICollection<ExportRequest> ExportRequests { get; set; } = new List<ExportRequest>();

    public virtual ICollection<GoodsDeliveryNote> GoodsDeliveryNotes { get; set; } = new List<GoodsDeliveryNote>();

    public virtual ICollection<GoodsReceivedNote> GoodsReceivedNotes { get; set; } = new List<GoodsReceivedNote>();

    public virtual ICollection<ImportRequest> ImportRequests { get; set; } = new List<ImportRequest>();
}
