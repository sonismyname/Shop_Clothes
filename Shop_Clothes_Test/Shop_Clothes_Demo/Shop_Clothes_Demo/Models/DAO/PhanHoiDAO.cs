using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Shop_Clothes_Demo.Models.DAO
{
    public class PhanHoiDAO
    {
        ShopModel db;
        public PhanHoiDAO()
        {
            db = new ShopModel();
        }   
        public IEnumerable<PhanHoi> listPhanHoi(string MaSanPham)
        {
            var lst = db.Database.SqlQuery<PhanHoi>("Select * from PhanHoi where MaSanPham="+MaSanPham+"");
            
            return lst;
        }
    }
}