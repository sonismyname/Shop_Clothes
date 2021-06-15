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
    public class LoaiSanPhamController : Controller
    {
        // GET: Admin/LoaiSanPham
        ShopModel db = new ShopModel();
        LoaiSanPhamDAO lspdao = new LoaiSanPhamDAO();
        public ActionResult Index(int? page)
        {
            int pageS = 5;
            int pageN = (page ?? 1);
            return View(lspdao.LoadLoaiSanPham(pageN, pageS));
        }
        [HttpGet]
        public ActionResult Add(LoaiSanPham lsp)
        {

            return View(lsp);
        }
        [ValidateInput(false)]// tắt ktra mota của microsoft
        [HttpPost]
        public ActionResult Add(LoaiSanPham lsp, HttpPostedFileBase photo)
        {
            if (photo == null)
            {
                ViewBag.uploadlsp = "Cần Thêm Logo";
            }

            if (photo != null)
            {
                if (photo.ContentLength > 0)
                {
                    if (photo.ContentType != "image/jpeg" && photo.ContentType != "image/png" && photo.ContentType != "image/gif")
                    {
                        ViewBag.uploadlsp = "Hình Ảnh không hợp lệ!!!";
                        return View(lsp);
                    }
                    else
                    {
                        var fileName = Path.GetFileName(photo.FileName);
                        var path = Path.Combine(Server.MapPath("~/Content/LoaiSanPham"), fileName);
                        if (System.IO.File.Exists(path))
                        {
                            lsp.Icon = fileName;
                        }
                        else
                        {
                            lsp.Icon = fileName;
                            photo.SaveAs(path);
                        }

                    }
                }
                int ma = lspdao.Add(lsp);
                return RedirectToAction("Index", "LoaiSanPham");
            }
            return View(lsp);
        }
        public ActionResult Delete(int malsp)
        {
            lspdao.Delete(malsp);
            return RedirectToAction("Index");
        }
        public ActionResult Details(int MaLoaiSanPham)
        {
            LoaiSanPham lsp = db.LoaiSanPhams.Find(MaLoaiSanPham);
            return View(lsp);
        }
        public ActionResult Edit(int MaLoaiSanPham)
        {
            LoaiSanPham lsp = db.LoaiSanPhams.Find(MaLoaiSanPham);
            Session["edit"] = lsp;
            return View(lsp);
        }
        [ValidateInput(false)]
        [HttpPost]
        public ActionResult Edit(LoaiSanPham lsp, HttpPostedFileBase photo)
        {
            lsp.MaLoaiSanPham = (Session["edit"] as LoaiSanPham).MaLoaiSanPham;
            lsp.Icon = (Session["edit"] as LoaiSanPham).Icon;

            Session["edit"] = null;

            if (photo != null)
            {
                if (photo.ContentLength > 0)
                {
                    // kiểm tra định dạng hình ảnh
                    if (photo.ContentType != "image/jpeg" && photo.ContentType != "image/png" && photo.ContentType != "image/gif")
                    {
                        ViewBag.upload = "Hình Ảnh không hợp lệ!!!";
                        return View(lsp);
                    }
                    else
                    {
                        var fileName = Path.GetFileName(photo.FileName);
                        var path = Path.Combine(Server.MapPath("~/Content/LoaiSanPham"), fileName);
                        if (System.IO.File.Exists(path))
                        {
                            lsp.Icon = fileName;
                        }
                        else
                        {
                            lsp.Icon = fileName;
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
            return RedirectToAction("Index", "LoaiSanPham");
        }
    }
}