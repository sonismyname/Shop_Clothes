using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Shop_Clothes_Demo.Models.DAO
{
    public class NhaSanXuatDAO
    {
        ShopModel db;
        public NhaSanXuatDAO()
        {
            db = new ShopModel();
        }
        public IEnumerable<NhaSanXuat> thongTin(string MaSanPham)
        {
            int ma = 0;
            if(MaSanPham!="")
            {
                ma = Convert.ToInt32(MaSanPham);
            }    
            else
            {
                ma = 0;
            }  
            if(ma!=0)
            {
                SanPham sp = db.SanPhams.Find(ma);
                var nsx = db.Database.SqlQuery<NhaSanXuat>("Select * from NhaSanXuat where MaNhaSanXuat=" + sp.MaNhaSanXuat + "");
                return nsx;
            }    
            else
            {
                return null;
            }    
        }
        public IEnumerable<NhaSanXuat> LoadNhaSanXuat(int Pagenum, int Pagesiz)
        {
            var lnsx = db.Database.SqlQuery<NhaSanXuat>("select * from NhaSanXuat")
                .ToPagedList<NhaSanXuat>(Pagenum, Pagesiz);
            return lnsx;
        }
        public NhaSanXuat findSPByID(int? MaNhaSanXuat)
        {
            NhaSanXuat nsx = db.NhaSanXuats.Find(MaNhaSanXuat);
            return nsx;
        }
        public int Add(NhaSanXuat nsx)
        {
            db.NhaSanXuats.Add(nsx);
            db.SaveChanges();
            return nsx.MaNhaSanXuat;
        }

        public void Delete(int id)
        {
            NhaSanXuat sp = db.NhaSanXuats.Find(id);
            if (sp != null)
            {
                db.NhaSanXuats.Remove(sp);
                db.SaveChanges();
            }
        }
        public IEnumerable<NhaSanXuat> searchbyName(string name)
        {
            var lst = db.Database.SqlQuery<NhaSanXuat>("Select * from NhaSanXuat where tennhasanxuat like N'%" + name + "%'");
            return lst;
        }

    }
}