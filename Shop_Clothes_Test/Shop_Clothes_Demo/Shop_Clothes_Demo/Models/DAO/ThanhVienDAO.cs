using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Shop_Clothes_Demo.Models.DAO
{
    public class ThanhVienDAO
    {
        ShopModel db;
        public ThanhVienDAO()
        {
            db = new ShopModel();
        }
        public int Add(ThanhVien tv)
        {
            var tvCheck = from n in db.ThanhViens.ToList() where n.TaiKhoan == tv.TaiKhoan select n;
            ThanhVien tvC = tvCheck.FirstOrDefault();
            if(tvC!=null)
            {
                //đã có tài khoản
                return -1;
            }    
            else
            {
                db.ThanhViens.Add(tv);
                db.SaveChanges();
                return tv.MaThanhVien;
            }    
            
        }
    }
}