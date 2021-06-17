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
                return View(lspdao.LoadLoaiSanPham(pageN, pageS));
            }
        }
        [HttpGet]
        public ActionResult Add(LoaiSanPham lsp)
        {
            ThanhVien x = Session["admin"] as ThanhVien;
            if (x == null)
            {
                /*Session["deny_access"] = "Hãy nhập thông tin tài khoản";*/
                return RedirectToAction("Index", "../Intro/Index");
            }
            else
            {
                return View(lsp);
            }
        }
        [HttpPost]
        public ActionResult Add(LoaiSanPham lsp, HttpPostedFileBase photo)
        {
            if (photo == null)
            {
                ViewBag.uploadlsp = "Cần Thêm Icon";
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
            ThanhVien x = Session["admin"] as ThanhVien;
            if (x == null)
            {
                /*Session["deny_access"] = "Hãy nhập thông tin tài khoản";*/
                return RedirectToAction("Index", "../Intro/Index");
            }
            else
            {
                lspdao.Delete(malsp);
                return RedirectToAction("Index");
            }
        }
        public ActionResult Details(int MaLoaiSanPham)
        {
            ThanhVien x = Session["admin"] as ThanhVien;
            if (x == null)
            {
                /*Session["deny_access"] = "Hãy nhập thông tin tài khoản";*/
                return RedirectToAction("Index", "../Intro/Index");
            }
            else
            {
                LoaiSanPham lsp = db.LoaiSanPhams.Find(MaLoaiSanPham);
                return View(lsp);
            }
        }
        public ActionResult Edit(int MaLoaiSanPham)
        {
            ThanhVien x = Session["admin"] as ThanhVien;
            if (x == null)
            {
                /*Session["deny_access"] = "Hãy nhập thông tin tài khoản";*/
                return RedirectToAction("Index", "../Intro/Index");
            }
            else
            {
                LoaiSanPham lsp = db.LoaiSanPhams.Find(MaLoaiSanPham);
                Session["edit"] = lsp;
                return View(lsp);
            }
        }
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
                        ViewBag.uploadlspe = "Hình Ảnh không hợp lệ!!!";
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