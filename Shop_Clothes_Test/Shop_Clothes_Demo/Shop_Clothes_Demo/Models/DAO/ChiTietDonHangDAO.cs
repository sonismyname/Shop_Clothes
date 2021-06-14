using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Shop_Clothes_Demo.Models.DAO
{
    public class ChiTietDonHangDAO
    {
        ShopModel db;
        public ChiTietDonHangDAO()
        {
            db = new ShopModel();
        }
        public int Add(ChiTietDonHang d)
        {
            db.ChiTietDonHangs.Add(d);
            db.SaveChanges();
            return d.MaChiTietDDH;
        }
        public IEnumerable<ChiTietDonHang> Detail(int MaDonDH)
        {
            string que = "select ct.MaChiTietDDH, ct.MaDonDatHang, ct.MaSanPham, ct.TenSanPham, ct.SoLuong, ct.DonGia " +
               "from ChiTietDonHang ct, DonDatHang dh " +
               "where ct.MaDonDatHang = dh.MaDonDatHang and dh.MaDonDatHang = "+MaDonDH+"";
            var lst = db.Database.SqlQuery<ChiTietDonHang>(que);
            return lst;
        }
    }
}