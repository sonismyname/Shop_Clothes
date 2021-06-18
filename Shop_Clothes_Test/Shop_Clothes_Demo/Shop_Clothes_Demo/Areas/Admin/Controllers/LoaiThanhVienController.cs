using Shop_Clothes_Demo.Models;
using Shop_Clothes_Demo.Models.DAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Shop_Clothes_Demo.Areas.Admin.Controllers
{
    public class LoaiThanhVienController : Controller
    {
        ShopModel db = new ShopModel();
        LoaiThanhVienDAO dao = new LoaiThanhVienDAO();
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
                return View(dao.LoadNhomMua(pageN, pageS));
            }
        }
        [HttpGet]
        public ActionResult Add(LoaiThanhVien nsx)
        {
            ThanhVien x = Session["admin"] as ThanhVien;
            if (x == null)
            {
                /*Session["deny_access"] = "Hãy nhập thông tin tài khoản";*/
                return RedirectToAction("Index", "../Intro/Index");
            }
            else
            {
                return View(nsx);
            }
        }
        [HttpPost]
        public ActionResult Add(LoaiThanhVien nsx, FormCollection f)
        {
            if (f["TenLoaiThanhVien"] == "" || f["TenLoaiThanhVien"] == null)
            {
                Session["Ltv"] = "Không Thể Thiếu Tên Loại!!!";
                return RedirectToAction("Index", "LoaiThanhVien");
            }
            if (f["UuDai"] == "" || f["UuDai"] == null)
            {
                Session["uudai"] = "Không Thể Thiếu Ưu Đãi!!!";
                return RedirectToAction("Index", "LoaiThanhVien");
            }
            Session["Ltv"] = null;
            Session["uudai"] = null;
            int ma = dao.Add(nsx);
            return RedirectToAction("Index", "LoaiThanhVien");
        }
        public ActionResult Delete(int ltv)
        {
            ThanhVien x = Session["admin"] as ThanhVien;
            if (x == null)
            {
                /*Session["deny_access"] = "Hãy nhập thông tin tài khoản";*/
                return RedirectToAction("Index", "../Intro/Index");
            }
            else
            {
                Session["Ltv"] = null;
                dao.Delete(ltv);
                return RedirectToAction("Index");
            }
        }
        public ActionResult Edit(int id)
        {
            ThanhVien x = Session["admin"] as ThanhVien;
            if (x == null)
            {
                /*Session["deny_access"] = "Hãy nhập thông tin tài khoản";*/
                return RedirectToAction("Index", "../Intro/Index");
            }
            else
            {
                LoaiThanhVien nsx = db.LoaiThanhViens.Find(id);
                Session["edit"] = nsx;
                return View(nsx);
            }
        }
        [HttpPost]
        public ActionResult Edit(LoaiThanhVien lsp)
        {
            lsp.MaLoaiThanhVien = (Session["edit"] as LoaiThanhVien).MaLoaiThanhVien;
            Session["edit"] = null;

            db.Entry(lsp).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult GetKey(string tukhoa)
        {
            return RedirectToAction("TimKiem", new { @tukhoa = tukhoa });
        }
        [HttpGet]
        public ActionResult TimKiem(int? page, string tukhoa)
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
                return View(dao.TimKiemNM(pageN, pageS, tukhoa));
            }
        }
    }
}