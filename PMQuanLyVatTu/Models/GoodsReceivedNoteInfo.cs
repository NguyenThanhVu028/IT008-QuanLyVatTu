using System;
using System.Collections.Generic;

namespace PMQuanLyVatTu.Models;

public partial class GoodsReceivedNoteInfo
{
    public string MaPn { get; set; } = null!;

    public string MaVt { get; set; } = null!;

    public int? SoLuong { get; set; }

    public double? ChietKhau { get; set; }

    public double? Vat { get; set; }

    public virtual GoodsReceivedNote MaPnNavigation { get; set; } = null!;

    public virtual Supply MaVtNavigation { get; set; } = null!;
}
