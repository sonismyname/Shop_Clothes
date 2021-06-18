using PagedList;
using Shop_Clothes_Demo.Models.DTO;
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
        public IEnumerable<ThanhVienDTO> LoadThanhVien(int Pagenum, int Pagesiz)
        {
            var ltv = db.Database.SqlQuery<ThanhVienDTO>("select MaThanhVien, HoTen, SoDienThoai, Email, DiaChi, TaiKhoan, MatKhau, TenLoaiThanhVien "
                + "  from ThanhVien as tv, LoaiThanhVien as ltv where tv.MaLoaiThanhVien =ltv.MaLoaiThanhVien")
                .ToPagedList<ThanhVienDTO>(Pagenum, Pagesiz);
            return ltv;
        }
        public ThanhVien findSPByID(int? MaThanhVien)
        {
            ThanhVien tv = db.ThanhViens.Find(MaThanhVien);
            return tv;
        }

        public void Delete(int id)
        {
            ThanhVien sp = db.ThanhViens.Find(id);
            if (sp != null)
            {
                db.ThanhViens.Remove(sp);
                db.SaveChanges();
            }
        }
        public IEnumerable<ThanhVienDTO> TimKiemTV(int Pagenum, int Pagesiz, string tukhoa, int LoaiTV)
        {
            string que = "select MaThanhVien, HoTen, SoDienThoai, Email, DiaChi, TaiKhoan, MatKhau, TenLoaiThanhVien "
                + "  from ThanhVien as tv, LoaiThanhVien as ltv where tv.MaLoaiThanhVien =ltv.MaLoaiThanhVien and tv.HoTen like N'%" + tukhoa + "%' and tv.MaLoaiThanhVien =" + LoaiTV + "";
            var lst = db.Database.SqlQuery<ThanhVienDTO>(que)
                .ToPagedList<ThanhVienDTO>(Pagenum, Pagesiz);
            return lst;
        }
    }
}