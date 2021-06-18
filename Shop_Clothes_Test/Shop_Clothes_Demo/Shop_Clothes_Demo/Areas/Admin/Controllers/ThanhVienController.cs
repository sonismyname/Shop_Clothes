using Shop_Clothes_Demo.Models;
using Shop_Clothes_Demo.Models.DAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Shop_Clothes_Demo.Areas.Admin.Controllers
{
    public class ThanhVienController : Controller
    {
        ShopModel db = new ShopModel();
        ThanhVienDAO dao = new ThanhVienDAO();
        public ActionResult Index(int? page)
        {
            Session["edit"] = null;
            ThanhVien x = Session["admin"] as ThanhVien;
            if (x == null)
            {
                /*Session["deny_access"] = "Hãy nhập thông tin tài khoản";*/
                return RedirectToAction("Index", "../Intro/Index");
            }
            else
            {
                ViewBag.LTV = new SelectList(db.LoaiThanhViens.OrderBy(n => n.TenLoaiThanhVien), "MaLoaiThanhVien", "TenLoaiThanhVien");

                int pageS = 5;
                int pageN = (page ?? 1);
                return View(dao.LoadThanhVien(pageN, pageS));
            }
        }
        public ActionResult Delete(int mtv)
        {
            ThanhVien x = Session["admin"] as ThanhVien;
            if (x == null)
            {
                /*Session["deny_access"] = "Hãy nhập thông tin tài khoản";*/
                return RedirectToAction("Index", "../Intro/Index");
            }
            else
            {
                dao.Delete(mtv);
                return RedirectToAction("Index");
            }
        }
        public ActionResult GetKey(string tukhoa, int LoaiTV)
        {
            return RedirectToAction("TimKiem", new { @tukhoa = tukhoa, @LoaiTV = LoaiTV });
        }
        [HttpGet]
        public ActionResult TimKiem(int? page, string tukhoa, int LoaiTV)
        {
            ThanhVien x = Session["admin"] as ThanhVien;
            if (x == null)
            {
                return RedirectToAction("Index", "../Intro/Index");
            }
            else
            {
                ViewBag.LTV = new SelectList(db.LoaiThanhViens.OrderBy(n => n.TenLoaiThanhVien), "MaLoaiThanhVien", "TenLoaiThanhVien");
                int pageS = 5;
                int pageN = (page ?? 1);
                return View(dao.TimKiemTV(pageN, pageS, tukhoa, LoaiTV));
            }
        }
        public ActionResult Details(int id)
        {
            ThanhVien x = Session["admin"] as ThanhVien;
            if (x == null)
            {
                return RedirectToAction("Index", "../Intro/Index");
            }
            else
            {
                ThanhVien tv = db.ThanhViens.Find(id);
                Session["edit"] = tv;
                ViewBag.LTV = new SelectList(db.LoaiThanhViens.OrderBy(n => n.TenLoaiThanhVien), "MaLoaiThanhVien", "TenLoaiThanhVien", tv.MaLoaiThanhVien);
                return View(tv);
            }
        }
        [HttpPost]
        public ActionResult Details(ThanhVien tv)
        {
            tv.DiaChi = (Session["edit"] as ThanhVien).DiaChi;
            tv.Email = (Session["edit"] as ThanhVien).Email;
            tv.HoTen = (Session["edit"] as ThanhVien).HoTen;
            tv.MaThanhVien = (Session["edit"] as ThanhVien).MaThanhVien;
            tv.SoDienThoai = (Session["edit"] as ThanhVien).SoDienThoai;
            tv.TaiKhoan = (Session["edit"] as ThanhVien).TaiKhoan;
            tv.MatKhau = (Session["edit"] as ThanhVien).MatKhau;
            Session["edit"] = null;
            ViewBag.LTV = new SelectList(db.LoaiThanhViens.OrderBy(n => n.TenLoaiThanhVien), "MaLoaiThanhVien", "TenLoaiThanhVien", tv.MaLoaiThanhVien);
            db.Entry(tv).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();
            return View(tv);

        }

    }
}