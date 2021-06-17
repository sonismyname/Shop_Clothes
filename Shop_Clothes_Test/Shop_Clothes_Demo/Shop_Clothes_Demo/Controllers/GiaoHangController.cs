using Shop_Clothes_Demo.Models;
using Shop_Clothes_Demo.Models.DAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Shop_Clothes_Demo.Controllers
{
    public class GiaoHangController : Controller
    {
        ShopModel db = new ShopModel();
        DonHangDAO dao = new DonHangDAO(); 
        // GET: GiaoHang
        public ActionResult Index(int? page)
        {
            if(Session["giaohang"]!=null)
            {
                int pageS = 5;
                int pageN = (page ?? 1);
                return View(dao.LoadListDonHang(pageN, pageS));
            }    
            return RedirectToAction("Index", "Intro");
        }
        public ActionResult KhongNhan(int id)
        {
            //lấy id xóa đơn hàng, cập nhật lại số lượng tồn, 
            
            DonDatHang dh = db.DonDatHangs.Find(id);
            if(dh!= null)
            {
                //Xóa đơn hàng
                db.DonDatHangs.Remove(dh);
                db.SaveChanges();
                //Cập nhật số lượng tồn và số lượng bán
                ChiTietDonHangDAO daoCT = new ChiTietDonHangDAO();
                List<ChiTietDonHang> listCT = daoCT.ListDH(id);
                foreach (ChiTietDonHang item in listCT)
                {
                    SanPham sp = db.SanPhams.Find(item.MaSanPham);
                    if (sp != null)
                    {
                        sp.SoLuongMua -= item.SoLuong;
                        sp.SoLuongTon += item.SoLuong;
                    }
                    db.SaveChanges();
                }
            }



            return RedirectToAction("Index", "GiaoHang");
        }
        public ActionResult DaNhan(int id)
        {
            //thay tình trạng giao hàng đã nhận, đã thanh toán

            DonDatHang dh = db.DonDatHangs.Find(id);
            if(dh!=null)
            {
                dh.TinhTrangGiaoHang = "Đã nhận";
                dh.DaThanhToan = true;
                db.SaveChanges();
            }
            return RedirectToAction("Index", "GiaoHang");
        }
        public ActionResult GiaoSau(int id)
        {
            //Delay ngay giao bây giờ + 1 day
            DonDatHang dh = db.DonDatHangs.Find(id);
            if(dh!=null)
            {
                DateTime date = dh.NgayGiaoDuKien.Value;
                
                dh.NgayGiaoDuKien = date.AddDays(1);
                db.SaveChanges();
            }    

            return RedirectToAction("Index", "GiaoHang");
        }
        public ActionResult ThongTinTaiKhoan()
        {
            if (Session["giaohang"] != null)
            {
                return View();
            }
            else
                return RedirectToAction("Index", "GiaoHang");
        }
        [HttpGet]
        public ActionResult Search(int search_inf = 0)
        {
            if (Session["giaohang"] != null)
            {
                return View(dao.Search(search_inf));
            }
            else
            {
                return RedirectToAction("Index", "Intro");
            }
        }
    }
}