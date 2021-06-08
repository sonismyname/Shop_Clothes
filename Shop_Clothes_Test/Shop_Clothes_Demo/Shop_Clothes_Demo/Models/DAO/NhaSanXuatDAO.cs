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
    }
}