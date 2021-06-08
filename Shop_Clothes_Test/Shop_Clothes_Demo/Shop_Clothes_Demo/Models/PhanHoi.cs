namespace Shop_Clothes_Demo.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("PhanHoi")]
    public partial class PhanHoi
    {
        [Key]
        public int MaPhanHoi { get; set; }

        public string NoiDung { get; set; }

        public int? MaSanPham { get; set; }

        public int? MaThanhVien { get; set; }

        public virtual SanPham SanPham { get; set; }

        public virtual ThanhVien ThanhVien { get; set; }
    }
}
