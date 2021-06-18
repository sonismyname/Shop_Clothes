using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Shop_Clothes_Demo.Models.DAO
{
    public class LoaiSanPhamDAO
    {
        ShopModel db;
        public LoaiSanPhamDAO()
        {
            db = new ShopModel();
        }
        public IEnumerable<LoaiSanPham> LoadLoaiSanPham(int Pagenum, int Pagesiz)
        {
            var lst = db.Database.SqlQuery<LoaiSanPham>("select * from LoaiSanPham")
                .ToPagedList<LoaiSanPham>(Pagenum, Pagesiz);
            return lst;
        }
        public LoaiSanPham findSPByID(int? MaLoaiSanPham)
        {
            LoaiSanPham lsp = db.LoaiSanPhams.Find(MaLoaiSanPham);
            return lsp;
        }
        public int Add(LoaiSanPham lsp)
        {
            db.LoaiSanPhams.Add(lsp);
            db.SaveChanges();
            return lsp.MaLoaiSanPham;
        }

        public void Delete(int id)
        {
            LoaiSanPham sp = db.LoaiSanPhams.Find(id);
            if (sp != null)
            {
                db.LoaiSanPhams.Remove(sp);
                db.SaveChanges();
            }
        }
        public IEnumerable<LoaiSanPham> searchbyName(string name)
        {
            var lst = db.Database.SqlQuery<LoaiSanPham>("Select * from LoaiSanPham where tenloaisanpham like N'%" + name + "%'");
            return lst;
        }
        public IEnumerable<LoaiSanPham> TimKiemLoaiSanPham(int Pagenum, int Pagesiz, string tukhoa)
        {
            var lst = db.Database.SqlQuery<LoaiSanPham>("select * from LoaiSanPham where TenLoaiSanPham like N'%"+tukhoa+"%'")
                .ToPagedList<LoaiSanPham>(Pagenum, Pagesiz);
            return lst;
        }

    }
}