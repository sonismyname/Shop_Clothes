using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.ComponentModel;

namespace Shop_Clothes_Demo.Models.DTO
{
    public class SanPhamDTO
    {
        [DisplayName("Mã Sản Phẩm")]
        public int MaSanPham { get; set; }
        [DisplayName("Tên Sản Phẩm")]
        public string TenSanPham { get; set; }
        [DisplayName("Đơn Giá")]
        public decimal? DonGia { get; set; }
        [DisplayName("Ngày Cập Nhật")]
        public DateTime? NgayCapNhat { get; set; }
        [DisplayName("Hình Ảnh 1")]
        public string HinhAnh { get; set; }
        [DisplayName("Số Lượng Tồn")]
        public int? SoLuongTon { get; set; }

        public string TenNhaSanXuat { get; set; }

        public string TenLoaiSanPham { get; set; }

        public string TenNhom { get; set; }

        public SanPhamDTO()
        {

        }
    }


}