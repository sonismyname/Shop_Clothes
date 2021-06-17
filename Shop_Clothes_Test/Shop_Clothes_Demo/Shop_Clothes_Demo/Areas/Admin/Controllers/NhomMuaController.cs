using Shop_Clothes_Demo.Models;
using Shop_Clothes_Demo.Models.DAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Shop_Clothes_Demo.Areas.Admin.Controllers
{
    public class NhomMuaController : Controller
    {
        // GET: Admin/NhomMua
        ShopModel db = new ShopModel();
        NhomMuaDAO dao = new NhomMuaDAO();
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
        public ActionResult Add(NhomMua nsx)
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
        public ActionResult Add(NhomMua nsx, FormCollection f)
        {
            if(f["TenNhom"] == ""||f["TenNhom"] == null)
            {
                Session["nhommua"] = "Không Thể Thiếu Tên Nhóm!!!";
                return RedirectToAction("Index", "NhomMua");
            }
            Session["nhommua"] = null;
            int ma = dao.Add(nsx);
            return RedirectToAction("Index", "NhomMua");
        }
        public ActionResult Delete(int mn)
        {
            ThanhVien x = Session["admin"] as ThanhVien;
            if (x == null)
            {
                /*Session["deny_access"] = "Hãy nhập thông tin tài khoản";*/
                return RedirectToAction("Index", "../Intro/Index");
            }
            else
            {
                Session["nhommua"] = null;
                dao.Delete(mn);
                return RedirectToAction("Index");
            }
        }
        public ActionResult Edit(int MaNhom)
        {
            ThanhVien x = Session["admin"] as ThanhVien;
            if (x == null)
            {
                /*Session["deny_access"] = "Hãy nhập thông tin tài khoản";*/
                return RedirectToAction("Index", "../Intro/Index");
            }
            else
            {
                NhomMua nsx = db.NhomMuas.Find(MaNhom);
                Session["edit"] = nsx;
                return View(nsx);
            }
        }
        [HttpPost]
        public ActionResult Edit(NhomMua lsp)
        {
            lsp.MaNhom = (Session["edit"] as NhomMua).MaNhom;
            Session["edit"] = null;

            db.Entry(lsp).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}