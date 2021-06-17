using Shop_Clothes_Demo.Models;
using Shop_Clothes_Demo.Models.DAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Shop_Clothes_Demo.Controllers
{
    public class IntroController : Controller
    {
        // GET: Intro
        SanPhamDAO dao = new SanPhamDAO();
        ShopModel db = new ShopModel();
        public ActionResult Index(int? page)
        {
            int pageS = 6;
            int pageN = (page ?? 1);
            ViewBag.list = dao.LoadSanPhamBanChay();
            
            return View(dao.LoadSanPham(pageN, pageS));
        }
        [HttpPost]
        public ActionResult Logout()
        {
            Session["username"] = null;
            Session["giaohang"] = null;
            return RedirectToAction("Index");
        }
        [ChildActionOnly]
        public ActionResult BanChayPartial()
        {
            return PartialView();
        }
        public ActionResult MenuPartial()
        {
            var lstSP = db.SanPhams;
            return PartialView(lstSP);
        }
        [HttpPost]
        public ActionResult DangNhap(FormCollection f)
        {
            string sTaiKhoan = f["txtTenDangNhap"];
            string sMatKhau = f["txtMatKhau"];

            ThanhVien tv = db.ThanhViens.SingleOrDefault(n => n.TaiKhoan == sTaiKhoan && n.MatKhau == sMatKhau);
              
            if (tv != null)
            {
                switch (tv.MaLoaiThanhVien)
                {
                    case 1:
                        {
                            Session["admin"] = tv;
                            return RedirectToAction("Index", "Admin/Dashboard");
                            
                        }
                    case 2:
                        {
                            Session["username"] = tv;
                            return RedirectToAction("Index");
                            
                        }
                    case 3:
                        {
                            Session["username"] = tv;
                            return RedirectToAction("Index");
                            
                        }
                    case 4:
                        {
                            Session["giaohang"] = tv;
                            return RedirectToAction("Index", "GiaoHang");
                        }
                    default:
                        {
                            return RedirectToAction("Index");
                        }
                }
            }
            return RedirectToAction("Index");
        }
        [HttpGet]
        public ActionResult Search(string search)
        {
            return View(dao.searchbyName(search));
        }
    }
}
