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
            DateTime day = DateTime.Now.AddDays(2);
            string ngay = day.ToString("MM/dd/yyyy");
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
        public IEnumerable<DonHangDTO> LoadListDonHangAdmin(int Pagenum, int Pagesiz)
        {
            var lst = db.Database.SqlQuery<DonHangDTO>("Select kh.MakhachHang, TenKhachHang, DiaChi, Email, SoDienThoai, " +
                      "MaDonDatHang, NgayGiaoDuKien, TinhTrangGiaoHang, DaThanhToan, NgayDat " +
                      "from KhachHang kh, DonDatHang dh " +
                      "where kh.MaKhachHang = dh.MaKhachHang ")
                .ToPagedList<DonHangDTO>(Pagenum, Pagesiz);
            return lst;
        }
        public IEnumerable<DonHangDTO> TimKiem(int Pagenum, int Pagesiz,string tukhoa, DateTime ngaytrc, DateTime ngaysau, string tinhtrang)
        {
            if (tinhtrang == "Tất cả")
            {
                tinhtrang = "";
            }
            string que = "Select kh.MakhachHang, TenKhachHang, DiaChi, Email, SoDienThoai, " +
                      "MaDonDatHang, NgayGiaoDuKien, TinhTrangGiaoHang, DaThanhToan, NgayDat " +
                      "from KhachHang kh, DonDatHang dh " +
                      "where kh.MaKhachHang = dh.MaKhachHang and dh.NgayDat >='" + ngaytrc +
                      "' and dh.NgayGiaoDuKien <='" + ngaysau + "' and dh.TinhTrangGiaoHang like N'%" + tinhtrang + "%' ";
            if( tukhoa != null)
            {
                int a;
                try { a = Convert.ToInt32(tukhoa); }
                catch
                {
                    a = -1;
                }
                que += "and dh.MaDonDatHang = " + a + " ";
            }
            var lst = db.Database.SqlQuery<DonHangDTO>(que)
                .ToPagedList<DonHangDTO>(Pagenum, Pagesiz);
            return lst;
        }
        public DateTime NgayDatSomNhat()
        {
            DonDatHang dh = db.DonDatHangs.SqlQuery("select * from DonDatHang order by NgayDat asc").FirstOrDefault();
            return (DateTime)dh.NgayDat;
        }
        public DateTime NgayGiaoMuonNhat()
        {
            DonDatHang dh = db.DonDatHangs.SqlQuery("select * from DonDatHang order by NgayGiaoDuKien desc").FirstOrDefault();
            return (DateTime)dh.NgayGiaoDuKien;
        }

    }
}