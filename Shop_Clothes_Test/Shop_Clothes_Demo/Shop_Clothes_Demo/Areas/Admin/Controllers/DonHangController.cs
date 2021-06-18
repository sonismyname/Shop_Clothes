using Shop_Clothes_Demo.Models;
using Shop_Clothes_Demo.Models.DAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Shop_Clothes_Demo.Areas.Admin.Controllers
{
    public class DonHangController : Controller
    {
        // GET: Admin/DonHang
        ShopModel db = new ShopModel();
        DonHangDAO dao = new DonHangDAO();
        public ActionResult Index(int? page)
        {
            ThanhVien x = Session["admin"] as ThanhVien;
            if (x == null)
            {
                /*Session["deny_access"] = "Hãy nhập thông tin tài khoản";*/
                return RedirectToAction("Index", "../Intro/Index");
            }
            else
            {
                int pageS = 5;
                int pageN = (page ?? 1);
                return View(dao.LoadListDonHangAdmin(pageN, pageS));
            }
        }
        public ActionResult Details(int id)
        {
            DonDatHang dh = db.DonDatHangs.Find(id);
            var lst = from n in db.ChiTietDonHangs.ToList() where n.MaDonDatHang == dh.MaDonDatHang select n;
            return View(lst);
        }
        public ActionResult GetKey(string tukhoa, DateTime? ngaytrc, DateTime? ngaysau, string tinhtrang)
        {
            if(ngaytrc == null)
            {
                ngaytrc = dao.NgayDatSomNhat();
            }
            if (ngaysau == null)
            {
                ngaysau = dao.NgayGiaoMuonNhat();
            }
            return RedirectToAction("TimKiem", new { @tukhoa = tukhoa, @ngaytrc = ngaytrc, @ngaysau = ngaysau, @tinhtrang = tinhtrang });
        }
        [HttpGet]
        public ActionResult TimKiem(int? page, string tukhoa, DateTime ngaytrc, DateTime ngaysau, string tinhtrang)
        {
            ThanhVien x = Session["admin"] as ThanhVien;
            if (x == null)
            {
                return RedirectToAction("Index", "../Intro/Index");
            }
            else
            {
                int pageS = 5;
                int pageN = (page ?? 1);
                ViewBag.ngaytrc = ngaytrc.ToString("yyyy-MM-dd");
                ViewBag.ngaysau = ngaysau.ToString("yyyy-MM-dd");
                ViewBag.tinhtrang = tinhtrang;

                return View(dao.TimKiem(pageN,pageS, tukhoa, ngaytrc, ngaysau, tinhtrang));
            }
        }
    }
}