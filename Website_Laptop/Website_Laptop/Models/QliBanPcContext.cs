using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Website_Laptop.Models;

public partial class QliBanPcContext : DbContext
{
    public QliBanPcContext()
    {
    }

    public QliBanPcContext(DbContextOptions<QliBanPcContext> options)
        : base(options)
    {
    }

    public virtual DbSet<AccountInfor> AccountInfors { get; set; }

    public virtual DbSet<DetailDonhangPc> DetailDonhangPcs { get; set; }

    public virtual DbSet<DonhangPc> DonhangPcs { get; set; }

    public virtual DbSet<PcAnhSp> PcAnhSps { get; set; }

    public virtual DbSet<PcChatLieuSp> PcChatLieuSps { get; set; }

    public virtual DbSet<PcDanhMucSp> PcDanhMucSps { get; set; }

    public virtual DbSet<PcHoaDon> PcHoaDons { get; set; }

    public virtual DbSet<PcLoaiSp> PcLoaiSps { get; set; }

    public virtual DbSet<PcQuocGiaSx> PcQuocGiaSxes { get; set; }

    public virtual DbSet<PcUser> PcUsers { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=DESKTOP-BI83N0Q;Initial Catalog=QliBanPc;Persist Security Info=True;User ID=sa;Password=quan154;Trust Server Certificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<AccountInfor>(entity =>
        {
            entity.HasKey(e => e.MaUser).HasName("PK__AccountI__55DAC4B7F963225E");

            entity.ToTable("AccountInfor");

            entity.Property(e => e.MaUser)
                .HasMaxLength(20)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.AddressUser)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("Address_User");
            entity.Property(e => e.DateOfBirthUser)
                .HasMaxLength(20)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("DateOfBirth_User");
            entity.Property(e => e.NameUser)
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.HasOne(d => d.MaUserNavigation).WithOne(p => p.AccountInfor)
                .HasForeignKey<AccountInfor>(d => d.MaUser)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_USER_INFO");
        });

        modelBuilder.Entity<DetailDonhangPc>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__DetailDo__3214EC0796F4C654");

            entity.ToTable("DetailDonhangPc");

            entity.Property(e => e.DonGia).HasColumnType("decimal(18, 0)");
            entity.Property(e => e.MaDonHang)
                .HasMaxLength(50)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.MaSp)
                .HasMaxLength(20)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("MaSP");
            entity.Property(e => e.MaUser)
                .HasMaxLength(20)
                .IsUnicode(false)
                .IsFixedLength();

            entity.HasOne(d => d.MaSpNavigation).WithMany(p => p.DetailDonhangPcs)
                .HasForeignKey(d => d.MaSp)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_DETAILDONHANG_DMSP");
        });

        modelBuilder.Entity<DonhangPc>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__DonhangP__3214EC072A13F631");

            entity.ToTable("DonhangPc");

            entity.Property(e => e.CreateDate).HasColumnType("datetime");
            entity.Property(e => e.MaDonHang)
                .HasMaxLength(50)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.MaUser)
                .HasMaxLength(20)
                .IsUnicode(false)
                .IsFixedLength();
        });

        modelBuilder.Entity<PcAnhSp>(entity =>
        {
            entity.HasKey(e => e.MaAnhSp).HasName("PK__pcAnhSP__B994756047D2E4A3");

            entity.ToTable("pcAnhSP");

            entity.Property(e => e.MaAnhSp)
                .HasMaxLength(20)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("MaAnhSP");
            entity.Property(e => e.MaSp)
                .HasMaxLength(20)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("MaSP");
            entity.Property(e => e.TenFileAnh)
                .HasMaxLength(50)
                .IsUnicode(false)
                .IsFixedLength();

            entity.HasOne(d => d.MaSpNavigation).WithMany(p => p.PcAnhSps)
                .HasForeignKey(d => d.MaSp)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ASP_DMSP");
        });

        modelBuilder.Entity<PcChatLieuSp>(entity =>
        {
            entity.HasKey(e => e.MaChatLieu).HasName("PK__pcChatLi__453995BC8C1EF73C");

            entity.ToTable("pcChatLieuSP");

            entity.Property(e => e.MaChatLieu)
                .HasMaxLength(20)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.TenChatLieu).HasMaxLength(50);
        });

        modelBuilder.Entity<PcDanhMucSp>(entity =>
        {
            entity.HasKey(e => e.MaSp).HasName("PK__pcDanhMu__2725087C5D0AE8C6");

            entity.ToTable("pcDanhMucSP");

            entity.Property(e => e.MaSp)
                .HasMaxLength(20)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.AnhDaiDien)
                .HasMaxLength(100)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.GiaSp).HasColumnType("decimal(18, 0)");
            entity.Property(e => e.GioiThieuSp)
                .HasMaxLength(300)
                .HasColumnName("GioiThieuSP");
            entity.Property(e => e.MaChatLieu)
                .HasMaxLength(20)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.MaLoai)
                .HasMaxLength(20)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.MaQuocGiaSx)
                .HasMaxLength(20)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("MaQuocGiaSX");
            entity.Property(e => e.TenSp).HasMaxLength(150);
            entity.Property(e => e.ThoiGianBaoHanh)
                .HasMaxLength(25)
                .IsUnicode(false)
                .IsFixedLength();

            entity.HasOne(d => d.MaChatLieuNavigation).WithMany(p => p.PcDanhMucSps)
                .HasForeignKey(d => d.MaChatLieu)
                .HasConstraintName("FK_DMSP_CLSP");

            entity.HasOne(d => d.MaLoaiNavigation).WithMany(p => p.PcDanhMucSps)
                .HasForeignKey(d => d.MaLoai)
                .HasConstraintName("FK_DMSP_LSP");

            entity.HasOne(d => d.MaQuocGiaSxNavigation).WithMany(p => p.PcDanhMucSps)
                .HasForeignKey(d => d.MaQuocGiaSx)
                .HasConstraintName("FK_DMSP_QGSX");
        });

        modelBuilder.Entity<PcHoaDon>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__pcHoaDon__3214EC07089DD007");

            entity.ToTable("pcHoaDon");

            entity.Property(e => e.CachThanhToan).HasMaxLength(30);
            entity.Property(e => e.DiaChi).HasMaxLength(100);
            entity.Property(e => e.GhiChu).HasMaxLength(100);
            entity.Property(e => e.MaDonHang)
                .HasMaxLength(50)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.SoDienThoai)
                .HasMaxLength(20)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.SoTienThanh).HasColumnType("decimal(18, 0)");
            entity.Property(e => e.TenKhachHang).HasMaxLength(50);
        });

        modelBuilder.Entity<PcLoaiSp>(entity =>
        {
            entity.HasKey(e => e.MaLoai).HasName("PK__pcLoaiSP__730A575908372283");

            entity.ToTable("pcLoaiSP");

            entity.Property(e => e.MaLoai)
                .HasMaxLength(20)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.TenLoai)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<PcQuocGiaSx>(entity =>
        {
            entity.HasKey(e => e.MaQuocGiaSx).HasName("PK__pcQuocGi__0159F6553C21421E");

            entity.ToTable("pcQuocGiaSX");

            entity.Property(e => e.MaQuocGiaSx)
                .HasMaxLength(20)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("MaQuocGiaSX");
            entity.Property(e => e.TenQuocGiaSx)
                .HasMaxLength(50)
                .HasColumnName("TenQuocGiaSX");
        });

        modelBuilder.Entity<PcUser>(entity =>
        {
            entity.HasKey(e => e.MaUser).HasName("PK__pcUser__55DAC4B7B893398B");

            entity.ToTable("pcUser");

            entity.Property(e => e.MaUser)
                .HasMaxLength(20)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.AccountNameUser)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("AccountName_User");
            entity.Property(e => e.GmailUser)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("Gmail_User");
            entity.Property(e => e.LoaiUser).HasColumnName("Loai_User");
            entity.Property(e => e.PassWordUser)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("PassWord_User");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
