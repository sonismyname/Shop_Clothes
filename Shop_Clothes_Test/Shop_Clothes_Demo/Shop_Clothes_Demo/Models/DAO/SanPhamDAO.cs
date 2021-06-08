using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Shop_Clothes_Demo.Models.DAO
{
    public class SanPhamDAO
    {
        ShopModel db;
        public SanPhamDAO()
        {
            db = new ShopModel();
        }
        public IEnumerable<SanPham> LoadSanPham(int Pagenum, int Pagesiz)
        {
            var lst = db.Database.SqlQuery<SanPham>("select * from SanPham")
                .ToPagedList<SanPham>(Pagenum, Pagesiz);
            return lst;
        }
        public IEnumerable<SanPham> LoadSanPhamBanChay()
        {
            var lst = db.Database.SqlQuery<SanPham>("select top 3 * from SanPham order by soluongmua desc");
            return lst;
        }
        public IEnumerable<SanPham> SanPhamTuongTu(string MaNhaSanXuat)
        {
            if(MaNhaSanXuat==null)
            {
                return null;
            }    
            var lst = db.Database.SqlQuery<SanPham>("select top 4 * from SanPham sp where sp.MaNhaSanXuat="+MaNhaSanXuat+" order by soluongmua desc");
            return lst;
        }
        public IEnumerable<SanPham> LoadSanPhamTheoTieuChi(int Pagenum, int Pagesiz, int? MaNhom, int? MaLoai)
        {
            var lst = db.Database.SqlQuery<SanPham>("select * from SanPham where MaNhom = " + MaNhom.ToString() + " and MaLoaiSanPham =" + MaLoai.ToString() + "")
                .ToPagedList<SanPham>(Pagenum, Pagesiz);
            return lst;
        }
        public SanPham findSPByID(int? MaSanPham)
        {
            SanPham sp = db.SanPhams.Find(MaSanPham);
            return sp;
        }
    }
}