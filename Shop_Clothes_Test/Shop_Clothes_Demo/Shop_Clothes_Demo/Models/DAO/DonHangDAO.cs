using PagedList;
using Shop_Clothes_Demo.Models.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Shop_Clothes_Demo.Models.DAO
{
    public class DonHangDAO
    {
        ShopModel db;
        public DonHangDAO()
        {
            db = new ShopModel();
        }
        public IEnumerable<DonHangDTO> LoadListDonHang(int Pagenum, int Pagesiz)
        {
            string ngay = DateTime.Now.ToString("MM/dd/yyyy");
            var lst = db.Database.SqlQuery<DonHangDTO>("Select kh.MakhachHang, TenKhachHang, DiaChi, Email, SoDienThoai, " +
                      "MaDonDatHang, NgayGiaoDuKien, TinhTrangGiaoHang, DaThanhToan " +
                      "from KhachHang kh, DonDatHang dh " +
                      "where kh.MaKhachHang = dh.MaKhachHang and TinhTrangGiaoHang = N'Đang giao' and NgayGiaoDuKien<='"+ngay+"' ")
                .ToPagedList<DonHangDTO>(Pagenum, Pagesiz);
            return lst;
        }
        public IEnumerable<DonHangDTO> Search(int search)
        {
            var lst = db.Database.SqlQuery<DonHangDTO>("Select kh.MakhachHang, TenKhachHang, DiaChi, Email, SoDienThoai, " +
                      "MaDonDatHang, NgayGiaoDuKien, TinhTrangGiaoHang, DaThanhToan " +
                      "from KhachHang kh, DonDatHang dh " +
                      "where kh.MaKhachHang = dh.MaKhachHang and MaDonDatHang="+search+"");          
            return lst;
        }

    }
}