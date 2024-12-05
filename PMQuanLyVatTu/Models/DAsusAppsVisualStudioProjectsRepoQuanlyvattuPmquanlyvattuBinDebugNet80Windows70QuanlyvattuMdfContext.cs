using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Configuration;

namespace PMQuanLyVatTu.Models;

public partial class DAsusAppsVisualStudioProjectsRepoQuanlyvattuPmquanlyvattuBinDebugNet80Windows70QuanlyvattuMdfContext : DbContext
{
    public DAsusAppsVisualStudioProjectsRepoQuanlyvattuPmquanlyvattuBinDebugNet80Windows70QuanlyvattuMdfContext()
    {
    }

    public DAsusAppsVisualStudioProjectsRepoQuanlyvattuPmquanlyvattuBinDebugNet80Windows70QuanlyvattuMdfContext(DbContextOptions<DAsusAppsVisualStudioProjectsRepoQuanlyvattuPmquanlyvattuBinDebugNet80Windows70QuanlyvattuMdfContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Account> Accounts { get; set; }

    public virtual DbSet<Customer> Customers { get; set; }

    public virtual DbSet<Employee> Employees { get; set; }

    public virtual DbSet<ExportRequest> ExportRequests { get; set; }

    public virtual DbSet<ExportRequestInfo> ExportRequestInfos { get; set; }

    public virtual DbSet<GoodsDeliveryNote> GoodsDeliveryNotes { get; set; }

    public virtual DbSet<GoodsDeliveryNoteInfo> GoodsDeliveryNoteInfos { get; set; }

    public virtual DbSet<GoodsReceivedNote> GoodsReceivedNotes { get; set; }

    public virtual DbSet<GoodsReceivedNoteInfo> GoodsReceivedNoteInfos { get; set; }

    public virtual DbSet<ImportRequest> ImportRequests { get; set; }

    public virtual DbSet<Supplier> Suppliers { get; set; }

    public virtual DbSet<Supply> Supplies { get; set; }

    public virtual DbSet<Warehouse> Warehouses { get; set; }

    //    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    //#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
    //        => optionsBuilder.UseSqlServer("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=\"D:\\ASUS\\Apps\\Visual Studio\\Projects\\Repo-QuanLyVatTu\\PMQuanLyVatTu\\bin\\Debug\\net8.0-windows7.0\\QuanLyVatTu.mdf\";Integrated Security=True");

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
//#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
    => optionsBuilder.UseSqlServer("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=\"|DataDirectory|\\QuanLyVatTu.mdf\";Integrated Security=True");


    //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    //{
    //    if (!optionsBuilder.IsConfigured)
    //    {
    //        optionsBuilder.UseSqlServer(ConfigurationManager.ConnectionStrings["QuanLyVatTu"].ConnectionString);
    //    }
    //}

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Account>(entity =>
        {
            entity.HasKey(e => e.TenDn).HasName("PK__Account__4CF965590BF5CBEB");

            entity.ToTable("Account");

            entity.Property(e => e.TenDn)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("TenDN");
            entity.Property(e => e.Email).IsUnicode(false);
            entity.Property(e => e.MaNv)
                .HasMaxLength(6)
                .IsUnicode(false)
                .HasColumnName("MaNV");
            entity.Property(e => e.MatKhau).IsUnicode(false);
            entity.Property(e => e.ThoiGianXoa).HasColumnType("datetime");

            entity.HasOne(d => d.MaNvNavigation).WithMany(p => p.Accounts)
                .HasForeignKey(d => d.MaNv)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK__Account__MaNV__3F466844");
        });

        modelBuilder.Entity<Customer>(entity =>
        {
            entity.HasKey(e => e.MaKh).HasName("PK__Customer__2725CF1EC974F987");

            entity.ToTable("Customer");

            entity.Property(e => e.MaKh)
                .HasMaxLength(6)
                .IsUnicode(false)
                .HasColumnName("MaKH");
            entity.Property(e => e.Email).IsUnicode(false);
            entity.Property(e => e.GioiTinh).HasMaxLength(5);
            entity.Property(e => e.Sdt)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("SDT");
            entity.Property(e => e.ThoiGianXoa).HasColumnType("datetime");
        });

        modelBuilder.Entity<Employee>(entity =>
        {
            entity.HasKey(e => e.MaNv).HasName("PK__Employee__2725D70A135D6E42");

            entity.ToTable("Employee");

            entity.Property(e => e.MaNv)
                .HasMaxLength(6)
                .IsUnicode(false)
                .HasColumnName("MaNV");
            entity.Property(e => e.Email).IsUnicode(false);
            entity.Property(e => e.GioiTinh).HasMaxLength(5);
            entity.Property(e => e.ImageLocation).IsUnicode(false);
            entity.Property(e => e.Sdt)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("SDT");
            entity.Property(e => e.ThoiGianXoa).HasColumnType("datetime");
        });

        modelBuilder.Entity<ExportRequest>(entity =>
        {
            entity.HasKey(e => e.MaYcx).HasName("PK__ExportRe__209B200402ABBBB9");

            entity.ToTable("ExportRequest");

            entity.Property(e => e.MaYcx)
                .HasMaxLength(7)
                .IsUnicode(false)
                .HasColumnName("MaYCX");
            entity.Property(e => e.KhoXuat)
                .HasMaxLength(7)
                .IsUnicode(false);
            entity.Property(e => e.MaKh)
                .HasMaxLength(6)
                .IsUnicode(false)
                .HasColumnName("MaKH");
            entity.Property(e => e.MaNv)
                .HasMaxLength(6)
                .IsUnicode(false)
                .HasColumnName("MaNV");
            entity.Property(e => e.NgayLap).HasColumnType("datetime");
            entity.Property(e => e.ThoiGianXoa).HasColumnType("datetime");
            entity.Property(e => e.TrangThai).HasMaxLength(50);

            entity.HasOne(d => d.KhoXuatNavigation).WithMany(p => p.ExportRequests)
                .HasForeignKey(d => d.KhoXuat)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("FK__ExportReq__KhoXu__00200768");

            entity.HasOne(d => d.MaKhNavigation).WithMany(p => p.ExportRequests)
                .HasForeignKey(d => d.MaKh)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("FK__ExportRequ__MaKH__7F2BE32F");

            entity.HasOne(d => d.MaNvNavigation).WithMany(p => p.ExportRequests)
                .HasForeignKey(d => d.MaNv)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("FK__ExportRequ__MaNV__7E37BEF6");
        });

        modelBuilder.Entity<ExportRequestInfo>(entity =>
        {
            entity.HasKey(e => new { e.MaYcx, e.MaVt }).HasName("PK__ExportRe__D2E971070C44AB8F");

            entity.ToTable("ExportRequestInfo");

            entity.Property(e => e.MaYcx)
                .HasMaxLength(7)
                .IsUnicode(false)
                .HasColumnName("MaYCX");
            entity.Property(e => e.MaVt)
                .HasMaxLength(6)
                .IsUnicode(false)
                .HasColumnName("MaVT");
            entity.Property(e => e.Vat).HasColumnName("VAT");

            entity.HasOne(d => d.MaVtNavigation).WithMany(p => p.ExportRequestInfos)
                .HasForeignKey(d => d.MaVt)
                .HasConstraintName("FK__ExportRequ__MaVT__03F0984C");

            entity.HasOne(d => d.MaYcxNavigation).WithMany(p => p.ExportRequestInfos)
                .HasForeignKey(d => d.MaYcx)
                .HasConstraintName("FK__ExportReq__MaYCX__02FC7413");
        });

        modelBuilder.Entity<GoodsDeliveryNote>(entity =>
        {
            entity.HasKey(e => e.MaPx).HasName("PK__GoodsDel__2725E7CACA0A7C2B");

            entity.ToTable("GoodsDeliveryNote");

            entity.Property(e => e.MaPx)
                .HasMaxLength(6)
                .IsUnicode(false)
                .HasColumnName("MaPX");
            entity.Property(e => e.KhoXuat)
                .HasMaxLength(7)
                .IsUnicode(false);
            entity.Property(e => e.MaKh)
                .HasMaxLength(6)
                .IsUnicode(false)
                .HasColumnName("MaKH");
            entity.Property(e => e.MaNv)
                .HasMaxLength(6)
                .IsUnicode(false)
                .HasColumnName("MaNV");
            entity.Property(e => e.NgayLap).HasColumnType("datetime");
            entity.Property(e => e.ThoiGianXoa).HasColumnType("datetime");
            entity.Property(e => e.TrangThai).HasMaxLength(50);
            entity.Property(e => e.Vat).HasColumnName("VAT");

            entity.HasOne(d => d.KhoXuatNavigation).WithMany(p => p.GoodsDeliveryNotes)
                .HasForeignKey(d => d.KhoXuat)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("FK__GoodsDeli__KhoXu__6B24EA82");

            entity.HasOne(d => d.MaKhNavigation).WithMany(p => p.GoodsDeliveryNotes)
                .HasForeignKey(d => d.MaKh)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("FK__GoodsDeliv__MaKH__6A30C649");

            entity.HasOne(d => d.MaNvNavigation).WithMany(p => p.GoodsDeliveryNotes)
                .HasForeignKey(d => d.MaNv)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("FK__GoodsDeliv__MaNV__693CA210");
        });

        modelBuilder.Entity<GoodsDeliveryNoteInfo>(entity =>
        {
            entity.HasKey(e => new { e.MaPx, e.MaVt }).HasName("PK__GoodsDel__D557B6C977F2CD29");

            entity.ToTable("GoodsDeliveryNoteInfo");

            entity.Property(e => e.MaPx)
                .HasMaxLength(6)
                .IsUnicode(false)
                .HasColumnName("MaPX");
            entity.Property(e => e.MaVt)
                .HasMaxLength(6)
                .IsUnicode(false)
                .HasColumnName("MaVT");
            entity.Property(e => e.Vat).HasColumnName("VAT");

            entity.HasOne(d => d.MaPxNavigation).WithMany(p => p.GoodsDeliveryNoteInfos)
                .HasForeignKey(d => d.MaPx)
                .HasConstraintName("FK__GoodsDeliv__MaPX__7A672E12");

            entity.HasOne(d => d.MaVtNavigation).WithMany(p => p.GoodsDeliveryNoteInfos)
                .HasForeignKey(d => d.MaVt)
                .HasConstraintName("FK__GoodsDeliv__MaVT__7B5B524B");
        });

        modelBuilder.Entity<GoodsReceivedNote>(entity =>
        {
            entity.HasKey(e => e.MaPn).HasName("PK__GoodsRec__2725E7F03850ACAE");

            entity.ToTable("GoodsReceivedNote");

            entity.Property(e => e.MaPn)
                .HasMaxLength(6)
                .IsUnicode(false)
                .HasColumnName("MaPN");
            entity.Property(e => e.KhoNhap)
                .HasMaxLength(7)
                .IsUnicode(false);
            entity.Property(e => e.MaNcc)
                .HasMaxLength(7)
                .IsUnicode(false)
                .HasColumnName("MaNCC");
            entity.Property(e => e.MaNv)
                .HasMaxLength(6)
                .IsUnicode(false)
                .HasColumnName("MaNV");
            entity.Property(e => e.NgayLap).HasColumnType("datetime");
            entity.Property(e => e.ThoiGianXoa).HasColumnType("datetime");
            entity.Property(e => e.TrangThai).HasMaxLength(50);
            entity.Property(e => e.Vat).HasColumnName("VAT");

            entity.HasOne(d => d.KhoNhapNavigation).WithMany(p => p.GoodsReceivedNotes)
                .HasForeignKey(d => d.KhoNhap)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("FK__GoodsRece__KhoNh__6383C8BA");

            entity.HasOne(d => d.MaNccNavigation).WithMany(p => p.GoodsReceivedNotes)
                .HasForeignKey(d => d.MaNcc)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("FK__GoodsRece__MaNCC__628FA481");

            entity.HasOne(d => d.MaNvNavigation).WithMany(p => p.GoodsReceivedNotes)
                .HasForeignKey(d => d.MaNv)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("FK__GoodsRecei__MaNV__619B8048");
        });

        modelBuilder.Entity<GoodsReceivedNoteInfo>(entity =>
        {
            entity.HasKey(e => new { e.MaPn, e.MaVt }).HasName("PK__GoodsRec__D557B6F32F8D0CE0");

            entity.ToTable("GoodsReceivedNoteInfo");

            entity.Property(e => e.MaPn)
                .HasMaxLength(6)
                .IsUnicode(false)
                .HasColumnName("MaPN");
            entity.Property(e => e.MaVt)
                .HasMaxLength(6)
                .IsUnicode(false)
                .HasColumnName("MaVT");
            entity.Property(e => e.Vat).HasColumnName("VAT");

            entity.HasOne(d => d.MaPnNavigation).WithMany(p => p.GoodsReceivedNoteInfos)
                .HasForeignKey(d => d.MaPn)
                .HasConstraintName("FK__GoodsRecei__MaPN__76969D2E");

            entity.HasOne(d => d.MaVtNavigation).WithMany(p => p.GoodsReceivedNoteInfos)
                .HasForeignKey(d => d.MaVt)
                .HasConstraintName("FK__GoodsRecei__MaVT__778AC167");
        });

        modelBuilder.Entity<ImportRequest>(entity =>
        {
            entity.HasKey(e => e.MaYcn).HasName("PK__ImportRe__209B203298EF9395");

            entity.ToTable("ImportRequest");

            entity.Property(e => e.MaYcn)
                .HasMaxLength(7)
                .IsUnicode(false)
                .HasColumnName("MaYCN");
            entity.Property(e => e.MaNv)
                .HasMaxLength(6)
                .IsUnicode(false)
                .HasColumnName("MaNV");
            entity.Property(e => e.MaVt)
                .HasMaxLength(6)
                .IsUnicode(false)
                .HasColumnName("MaVT");
            entity.Property(e => e.NgayLap).HasColumnType("datetime");
            entity.Property(e => e.ThoiGianXoa).HasColumnType("datetime");
            entity.Property(e => e.TrangThai).HasMaxLength(50);

            entity.HasOne(d => d.MaNvNavigation).WithMany(p => p.ImportRequests)
                .HasForeignKey(d => d.MaNv)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("FK__ImportRequ__MaNV__47DBAE45");

            entity.HasOne(d => d.MaVtNavigation).WithMany(p => p.ImportRequests)
                .HasForeignKey(d => d.MaVt)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("FK__ImportRequ__MaVT__48CFD27E");
        });

        modelBuilder.Entity<Supplier>(entity =>
        {
            entity.HasKey(e => e.MaNcc).HasName("PK__Supplier__3A185DEB80208529");

            entity.ToTable("Supplier");

            entity.Property(e => e.MaNcc)
                .HasMaxLength(7)
                .IsUnicode(false)
                .HasColumnName("MaNCC");
            entity.Property(e => e.Email).IsUnicode(false);
            entity.Property(e => e.Sdt)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("SDT");
            entity.Property(e => e.TenNcc).HasColumnName("TenNCC");
            entity.Property(e => e.ThoiGianXoa).HasColumnType("datetime");
        });

        modelBuilder.Entity<Supply>(entity =>
        {
            entity.HasKey(e => e.MaVt).HasName("PK__Supply__2725103EA35DD4B5");

            entity.ToTable("Supply");

            entity.Property(e => e.MaVt)
                .HasMaxLength(6)
                .IsUnicode(false)
                .HasColumnName("MaVT");
            entity.Property(e => e.DonViTinh).HasMaxLength(50);
            entity.Property(e => e.ImageLocation).IsUnicode(false);
            entity.Property(e => e.LoaiVatTu).HasMaxLength(50);
            entity.Property(e => e.MaKho)
                .HasMaxLength(7)
                .IsUnicode(false);
            entity.Property(e => e.MaNcc)
                .HasMaxLength(7)
                .IsUnicode(false)
                .HasColumnName("MaNCC");
            entity.Property(e => e.ThoiGianXoa).HasColumnType("datetime");

            entity.HasOne(d => d.MaKhoNavigation).WithMany(p => p.Supplies)
                .HasForeignKey(d => d.MaKho)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("FK__Supply__MaKho__44FF419A");

            entity.HasOne(d => d.MaNccNavigation).WithMany(p => p.Supplies)
                .HasForeignKey(d => d.MaNcc)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("FK__Supply__MaNCC__440B1D61");
        });

        modelBuilder.Entity<Warehouse>(entity =>
        {
            entity.HasKey(e => e.MaKho).HasName("PK__Warehous__3BDA9350A93F6D6B");

            entity.ToTable("Warehouse");

            entity.Property(e => e.MaKho)
                .HasMaxLength(7)
                .IsUnicode(false);
            entity.Property(e => e.LoaiVatTu).HasMaxLength(50);
            entity.Property(e => e.ThoiGianXoa).HasColumnType("datetime");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
