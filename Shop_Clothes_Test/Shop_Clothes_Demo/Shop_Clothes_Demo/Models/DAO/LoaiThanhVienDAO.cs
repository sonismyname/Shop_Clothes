using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Shop_Clothes_Demo.Models.DAO
{
    public class LoaiThanhVienDAO
    {
        ShopModel db;
        public LoaiThanhVienDAO()
        {
            db = new ShopModel();
        }
        public IEnumerable<LoaiThanhVien> LoadNhomMua(int Pagenum, int Pagesiz)
        {
            var lst = db.Database.SqlQuery<LoaiThanhVien>("select * from LoaiThanhVien")
                .ToPagedList<LoaiThanhVien>(Pagenum, Pagesiz);
            return lst;
        }
        public LoaiThanhVien findSPByID(int? MaLoaiThanhVien)
        {
            LoaiThanhVien lsp = db.LoaiThanhViens.Find(MaLoaiThanhVien);
            return lsp;
        }
        public int Add(LoaiThanhVien lsp)
        {
            db.LoaiThanhViens.Add(lsp);
            db.SaveChanges();
            return lsp.MaLoaiThanhVien;
        }

        public void Delete(int id)
        {
            LoaiThanhVien sp = db.LoaiThanhViens.Find(id);
            if (sp != null)
            {
                db.LoaiThanhViens.Remove(sp);
                db.SaveChanges();
            }
        }

        public IEnumerable<LoaiThanhVien> TimKiemNM(int Pagenum, int Pagesiz, string tukhoa)
        {
            var lst = db.Database.SqlQuery<LoaiThanhVien>("select * from LoaiThanhVien where TenLoaiThanhVien like N'%" + tukhoa + "%'")
                .ToPagedList<LoaiThanhVien>(Pagenum, Pagesiz);
            return lst;
        }
    }
}