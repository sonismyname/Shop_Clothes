using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Shop_Clothes_Demo.Models.DAO
{
    public class NhomMuaDAO
    {
        ShopModel db;
        public NhomMuaDAO()
        {
            db = new ShopModel();
        }
        public IEnumerable<NhomMua> LoadNhomMua(int Pagenum, int Pagesiz)
        {
            var lst = db.Database.SqlQuery<NhomMua>("select * from NhomMua")
                .ToPagedList<NhomMua>(Pagenum, Pagesiz);
            return lst;
        }
        public NhomMua findSPByID(int? MaNhom)
        {
            NhomMua lsp = db.NhomMuas.Find(MaNhom);
            return lsp;
        }
        public int Add(NhomMua lsp)
        {
            db.NhomMuas.Add(lsp);
            db.SaveChanges();
            return lsp.MaNhom;
        }

        public void Delete(int id)
        {
            NhomMua sp = db.NhomMuas.Find(id);
            if (sp != null)
            {
                db.NhomMuas.Remove(sp);
                db.SaveChanges();
            }
        }
        public IEnumerable<NhomMua> searchbyName(string name)
        {
            var lst = db.Database.SqlQuery<NhomMua>("Select * from NhomMua where tennhom like N'%" + name + "%'");
            return lst;
        }
        public IEnumerable<NhomMua> TimKiemNM(int Pagenum, int Pagesiz, string tukhoa)
        {
            var lst = db.Database.SqlQuery<NhomMua>("select * from NhomMua where TenNhom like N'%" + tukhoa + "%'")
                .ToPagedList<NhomMua>(Pagenum, Pagesiz);
            return lst;
        }
    }
}