namespace Shop_Clothes_Demo.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    using System.ComponentModel;

    [Table("SanPham")]
    public partial class SanPham
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public SanPham()
        {
            ChiTietDonHangs = new HashSet<ChiTietDonHang>();
            PhanHois = new HashSet<PhanHoi>();
        }

        [Key]
        [DisplayName("M? S?n Ph?m")]
        public int MaSanPham { get; set; }

        [StringLength(255)]
        [DisplayName("Tên S?n Ph?m")]
        [Required(ErrorMessage = "Không ðý?c ð? tr?ng thông tin!!!")]
        public string TenSanPham { get; set; }

        [DisplayName("Ðõn Giá")]
        [Required(ErrorMessage = "Không ðý?c ð? tr?ng thông tin!!!")]
        public decimal? DonGia { get; set; }

        [DisplayName("Mô T?")]
        public string MoTa { get; set; }

        [DisplayName("Ngày C?p Nh?t")]
        [Required(ErrorMessage = "Không ðý?c ð? tr?ng thông tin!!!")]
        public DateTime? NgayCapNhat { get; set; }

        [DisplayName("H?nh ?nh 1")]
        public string HinhAnh { get; set; }

        [DisplayName("H?nh ?nh 2")]
        public string HinhAnh2 { get; set; }

        [DisplayName("H?nh ?nh 3")]
        public string HinhAnh3 { get; set; }

        [DisplayName("S? Lý?ng Mua")]
        public int? SoLuongMua { get; set; }

        [DisplayName("S? Lý?ng T?n")]
        [Required(ErrorMessage = "Không ðý?c ð? tr?ng thông tin!!!")]
        public int? SoLuongTon { get; set; }
        [DisplayName("Nhà S?n Xu?t")]
        public int? MaNhaSanXuat { get; set; }
        [DisplayName("Lo?i S?n Ph?m")]
        public int? MaLoaiSanPham { get; set; }
        [DisplayName("Nhóm Mua")]
        public int? MaNhom { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ChiTietDonHang> ChiTietDonHangs { get; set; }

        public virtual LoaiSanPham LoaiSanPham { get; set; }

        public virtual NhaSanXuat NhaSanXuat { get; set; }

        public virtual NhomMua NhomMua { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PhanHoi> PhanHois { get; set; }
    }
}
