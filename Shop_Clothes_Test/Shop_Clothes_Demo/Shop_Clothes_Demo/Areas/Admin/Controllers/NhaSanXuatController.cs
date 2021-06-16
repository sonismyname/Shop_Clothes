using Shop_Clothes_Demo.Models;
using Shop_Clothes_Demo.Models.DAO;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Shop_Clothes_Demo.Areas.Admin.Controllers
{
    public class NhaSanXuatController : Controller
    {
        // GET: Admin/NhaSanXuat
        ShopModel db = new ShopModel();
        NhaSanXuatDAO dao = new NhaSanXuatDAO();
        public ActionResult Index(int? page)
        {
            int pageS = 5;
            int pageN = (page ?? 1);
            return View(dao.LoadNhaSanXuat(pageN, pageS));
        }
        [HttpGet]
        public ActionResult Add(NhaSanXuat nsx)
        {
            return View(nsx);
        }
        [HttpPost]
        public ActionResult Add(NhaSanXuat nsx, HttpPostedFileBase photo)
        {
            if (photo == null)
            {
                ViewBag.uploadnsx = "Cần Thêm Logo";
            }

            if (photo != null)
            {
                if (photo.ContentLength > 0)
                {
                    if (photo.ContentType != "image/jpeg" && photo.ContentType != "image/png" && photo.ContentType != "image/gif")
                    {
                        ViewBag.uploadnsx = "Hình Ảnh không hợp lệ!!!";
                        return View(nsx);
                    }
                    else
                    {
                        var fileName = Path.GetFileName(photo.FileName);
                        var path = Path.Combine(Server.MapPath("~/Content/NhaSanXuat"), fileName);
                        if (System.IO.File.Exists(path))
                        {
                            nsx.Logo = fileName;
                        }
                        else
                        {
                            nsx.Logo = fileName;
                            photo.SaveAs(path);
                        }

                    }
                }
                int ma = dao.Add(nsx);
                return RedirectToAction("Index", "NhaSanXuat");
            }
            return View(nsx);
        }
        public ActionResult Delete(int mansx)
        {
            dao.Delete(mansx);
            return RedirectToAction("Index");
        }
        public ActionResult Details(int MaNhaSanXuat)
        {
            NhaSanXuat nsx = db.NhaSanXuats.Find(MaNhaSanXuat);
            return View(nsx);
        }
        public ActionResult Edit(int MaNhaSanXuat)
        {
            NhaSanXuat nsx = db.NhaSanXuats.Find(MaNhaSanXuat);
            Session["edit"] = nsx;
            return View(nsx);
        }
        [HttpPost]
        public ActionResult Edit(NhaSanXuat lsp, HttpPostedFileBase photo)
        {
            lsp.MaNhaSanXuat = (Session["edit"] as NhaSanXuat).MaNhaSanXuat;
            lsp.Logo = (Session["edit"] as NhaSanXuat).Logo;

            Session["edit"] = null;

            if (photo != null)
            {
                if (photo.ContentLength > 0)
                {
                    // kiểm tra định dạng hình ảnh
                    if (photo.ContentType != "image/jpeg" && photo.ContentType != "image/png" && photo.ContentType != "image/gif")
                    {
                        ViewBag.uploadnsxe = "Hình Ảnh không hợp lệ!!!";
                        return View(lsp);
                    }
                    else
                    {
                        var fileName = Path.GetFileName(photo.FileName);
                        var path = Path.Combine(Server.MapPath("~/Content/NhaSanXuat"), fileName);
                        if (System.IO.File.Exists(path))
                        {
                            lsp.Logo = fileName;
                        }
                        else
                        {
                            lsp.Logo = fileName;
                            photo.SaveAs(path);
                        }
                    }
                }
                else
                {
                    return View("Edit");
                }
            }
            db.Entry(lsp).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Index", "NhaSanXuat");
        }
    }
}