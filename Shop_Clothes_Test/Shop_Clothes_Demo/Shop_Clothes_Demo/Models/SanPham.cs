namespace Shop_Clothes_Demo.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

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
        public int MaSanPham { get; set; }

        [StringLength(255)]
        public string TenSanPham { get; set; }

        public decimal? DonGia { get; set; }

        public string MoTa { get; set; }

        public DateTime? NgayCapNhat { get; set; }

        public string HinhAnh { get; set; }

        public string HinhAnh2 { get; set; }

        public string HinhAnh3 { get; set; }

        public int? SoLuongMua { get; set; }

        public int? SoLuongTon { get; set; }

        public int? MaNhaSanXuat { get; set; }

        public int? MaLoaiSanPham { get; set; }

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
