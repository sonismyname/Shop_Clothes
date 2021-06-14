using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Shop_Clothes_Demo.Models.DAO
{
    public class KhachHangDAO
    {
        ShopModel db;
        public KhachHangDAO()
        {
            db = new ShopModel();
        }
        public int Add(KhachHang kh)
        {
            db.KhachHangs.Add(kh);
            db.SaveChanges();
            return kh.MaKhachHang;
        }
    }
}