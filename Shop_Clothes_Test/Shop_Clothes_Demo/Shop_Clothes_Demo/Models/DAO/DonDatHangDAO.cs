using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Shop_Clothes_Demo.Models.DAO
{
    public class DonDatHangDAO
    {
        ShopModel db;
        public DonDatHangDAO()
        {
            db = new ShopModel();
        }
        public int Add(DonDatHang d)
        {
            db.DonDatHangs.Add(d);
            db.SaveChanges();
            return d.MaDonDatHang;
        }
        public IEnumerable<DonDatHang> listDH(int MaThanhVien)
        {
            string que = "select dh.MaDonDatHang, dh.NgayDat, dh.NgayGiaoDuKien, dh.TinhTrangGiaoHang, dh.DaThanhToan, dh.MaKhachHang, dh.UuDai " +
            "from DonDatHang dh, KhachHang kh, ThanhVien tv " +
            "where dh.makhachhang = kh.makhachhang and kh.mathanhvien = tv.mathanhvien and tv.mathanhvien = "+MaThanhVien+"";
            var lst = db.Database.SqlQuery<DonDatHang>(que);
            return lst;
        }
    }
}