namespace Shop_Clothes_Demo.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("ThanhVien")]
    public partial class ThanhVien
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public ThanhVien()
        {
            KhachHangs = new HashSet<KhachHang>();
            PhanHois = new HashSet<PhanHoi>();
        }

        [Key]
        public int MaThanhVien { get; set; }

        [StringLength(255)]
        public string TaiKhoan { get; set; }

        [StringLength(255)]
        public string MatKhau { get; set; }

        public string HoTen { get; set; }

        [StringLength(50)]
        public string SoDienThoai { get; set; }

        public string DiaChi { get; set; }

        public string Email { get; set; }

        public int? MaLoaiThanhVien { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<KhachHang> KhachHangs { get; set; }

        public virtual LoaiThanhVien LoaiThanhVien { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PhanHoi> PhanHois { get; set; }
    }
}
