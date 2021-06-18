using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Shop_Clothes_Demo.Models.DTO
{

    public class ThanhVienDTO
    {
        public int MaThanhVien { get; set; }

        public string TaiKhoan { get; set; }

        public string MatKhau { get; set; }

        public string HoTen { get; set; }

        public string SoDienThoai { get; set; }

        public string DiaChi { get; set; }

        public string Email { get; set; }

        public string TenLoaiThanhVien { get; set; }
        public ThanhVienDTO()
        {

        }
    }
}