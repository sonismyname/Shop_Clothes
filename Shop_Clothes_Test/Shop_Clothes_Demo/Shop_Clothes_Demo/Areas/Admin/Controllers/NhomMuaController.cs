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
            int pageS = 5;
            int pageN = (page ?? 1);
            return View(dao.LoadNhomMua(pageN, pageS));
        }
        [HttpGet]
        public ActionResult Add(NhomMua nsx)
        {
            return View(nsx);
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
            Session["nhommua"] = null;
            dao.Delete(mn);
            return RedirectToAction("Index");
        }
        public ActionResult Edit(int MaNhom)
        {
            NhomMua nsx = db.NhomMuas.Find(MaNhom);
            Session["edit"] = nsx;
            return View(nsx);
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