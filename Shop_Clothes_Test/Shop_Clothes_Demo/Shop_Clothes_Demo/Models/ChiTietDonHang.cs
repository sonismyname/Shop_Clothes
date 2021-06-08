namespace Shop_Clothes_Demo.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("ChiTietDonHang")]
    public partial class ChiTietDonHang
    {
        [Key]
        public int MaChiTietDDH { get; set; }

        public int? MaDonDatHang { get; set; }

        public int? MaSanPham { get; set; }

        [StringLength(255)]
        public string TenSanPham { get; set; }

        public int? SoLuong { get; set; }

        public decimal? DonGia { get; set; }

        public virtual DonDatHang DonDatHang { get; set; }

        public virtual SanPham SanPham { get; set; }
    }
}
