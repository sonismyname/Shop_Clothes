using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Shop_Clothes_Demo.Models.DTO
{
    public class DonHangDTO
    {
        public int MaKhachHang { get; set; }

        public string TenKhachHang { get; set; }

        public string DiaChi { get; set; }

        public string Email { get; set; }

        public string SoDienThoai { get; set; }

        public int MaDonDatHang { get; set; }

        public DateTime NgayGiaoDuKien { get; set; }

        public string TinhTrangGiaoHang { get; set; }

        public bool DaThanhToan { get; set; }

        public DonHangDTO() { }
    }
}