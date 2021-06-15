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
        [DisplayName("Mã Sản Phẩm")]
        public int MaSanPham { get; set; }

        [StringLength(255)]
        [DisplayName("Tên Sản Phẩm")]
        [Required(ErrorMessage = "Không được để trống thông tin!!!")]
        public string TenSanPham { get; set; }

        [DisplayName("Đơn Giá")]
        [Required(ErrorMessage = "Không được để trống thông tin!!!")]
        public decimal? DonGia { get; set; }

        [DisplayName("Mô Tả")]
        public string MoTa { get; set; }

        [DisplayName("Ngày Cập Nhật")]
        [Required(ErrorMessage = "Không được để trống thông tin!!!")]
        public DateTime? NgayCapNhat { get; set; }

        [DisplayName("Hình Ảnh 1")]
        public string HinhAnh { get; set; }

        [DisplayName("Hình Ảnh 2")]
        public string HinhAnh2 { get; set; }

        [DisplayName("Hình Ảnh 3")]
        public string HinhAnh3 { get; set; }

        [DisplayName("Số Lượng Mua")]
        public int? SoLuongMua { get; set; }

        [DisplayName("Số Lượng Tồn")]
        [Required(ErrorMessage = "Không được để trống thông tin!!!")]
        public int? SoLuongTon { get; set; }
        [DisplayName("Nhà Sản Xuất")]
        public int? MaNhaSanXuat { get; set; }
        [DisplayName("Loại Sản Phẩm")]
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
