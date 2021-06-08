using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace Shop_Clothes_Demo.Models
{
    public partial class ShopModel : DbContext
    {
        public ShopModel()
            : base("name=ShopModel1")
        {
        }

        public virtual DbSet<ChiTietDonHang> ChiTietDonHangs { get; set; }
        public virtual DbSet<DonDatHang> DonDatHangs { get; set; }
        public virtual DbSet<KhachHang> KhachHangs { get; set; }
        public virtual DbSet<LoaiSanPham> LoaiSanPhams { get; set; }
        public virtual DbSet<LoaiThanhVien> LoaiThanhViens { get; set; }
        public virtual DbSet<NhaSanXuat> NhaSanXuats { get; set; }
        public virtual DbSet<NhomMua> NhomMuas { get; set; }
        public virtual DbSet<PhanHoi> PhanHois { get; set; }
        public virtual DbSet<SanPham> SanPhams { get; set; }
        public virtual DbSet<ThanhVien> ThanhViens { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ChiTietDonHang>()
                .Property(e => e.DonGia)
                .HasPrecision(18, 0);

            modelBuilder.Entity<DonDatHang>()
                .HasMany(e => e.ChiTietDonHangs)
                .WithOptional(e => e.DonDatHang)
                .WillCascadeOnDelete();

            modelBuilder.Entity<KhachHang>()
                .HasMany(e => e.DonDatHangs)
                .WithOptional(e => e.KhachHang)
                .WillCascadeOnDelete();

            modelBuilder.Entity<LoaiSanPham>()
                .HasMany(e => e.SanPhams)
                .WithOptional(e => e.LoaiSanPham)
                .WillCascadeOnDelete();

            modelBuilder.Entity<LoaiThanhVien>()
                .HasMany(e => e.ThanhViens)
                .WithOptional(e => e.LoaiThanhVien)
                .WillCascadeOnDelete();

            modelBuilder.Entity<NhaSanXuat>()
                .HasMany(e => e.SanPhams)
                .WithOptional(e => e.NhaSanXuat)
                .WillCascadeOnDelete();

            modelBuilder.Entity<NhomMua>()
                .HasMany(e => e.SanPhams)
                .WithOptional(e => e.NhomMua)
                .WillCascadeOnDelete();

            modelBuilder.Entity<SanPham>()
                .Property(e => e.DonGia)
                .HasPrecision(18, 0);

            modelBuilder.Entity<SanPham>()
                .HasMany(e => e.ChiTietDonHangs)
                .WithOptional(e => e.SanPham)
                .WillCascadeOnDelete();

            modelBuilder.Entity<SanPham>()
                .HasMany(e => e.PhanHois)
                .WithOptional(e => e.SanPham)
                .WillCascadeOnDelete();

            modelBuilder.Entity<ThanhVien>()
                .HasMany(e => e.KhachHangs)
                .WithOptional(e => e.ThanhVien)
                .WillCascadeOnDelete();

            modelBuilder.Entity<ThanhVien>()
                .HasMany(e => e.PhanHois)
                .WithOptional(e => e.ThanhVien)
                .WillCascadeOnDelete();
        }
    }
}
