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
                Session["username"] = tv;
                return RedirectToAction("Index");
            }
            return RedirectToAction("Index");
        }
        [HttpGet]
        public ActionResult Search(string search)
        {
            string kq = search;
            return RedirectToAction("Index", "Intro");
        }
    }
}
