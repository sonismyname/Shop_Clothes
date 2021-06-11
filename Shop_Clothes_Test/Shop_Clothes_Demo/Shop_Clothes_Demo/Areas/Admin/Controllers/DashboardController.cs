using Shop_Clothes_Demo.Models;
using Shop_Clothes_Demo.Models.DAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;
using System.IO;

namespace Shop_Clothes_Demo.Areas.Admin.Controllers
{
    public class DashboardController : Controller
    {
        // GET: Admin/Dashboard
        ShopModel db = new ShopModel();
        SanPhamDAO spdao = new SanPhamDAO();
        public ActionResult Index(int? page)
        {
            //var x = Session["admin"];
            //if (x == null)
            //{
            //    Session["deny_access"] = "Hãy nhập thông tin tài khoản";
            //    return RedirectToAction("Index", "Intro");
            //}
            //else
            //{
            int pageS = 5;
            int pageN = (page ?? 1);
            return View(spdao.LoadSanPhamAdmin(pageN, pageS));

            //}
        }
        [HttpGet]
        public ActionResult AddProduct(SanPham sp)
        {
            ViewBag.MaLoaiSanPham = new SelectList(db.LoaiSanPhams.OrderBy(n => n.TenLoaiSanPham), "MaLoaiSanPham", "TenLoaiSanPham");
            ViewBag.MaNhaSanXuat = new SelectList(db.NhaSanXuats.OrderBy(n => n.TenNhaSanXuat), "MaNhaSanXuat", "TenNhaSanXuat");
            ViewBag.MaNhom = new SelectList(db.NhomMuas.OrderBy(n => n.TenNhom), "MaNhom", "TenNhom");
            return View(sp);
        }
        //[ValidateInput(false)]// tắt ktra mota của microsoft
        [HttpPost]
        public ActionResult AddProduct(SanPham sp, HttpPostedFileBase[] photo)
        {
            ViewBag.MaLoaiSanPham = new SelectList(db.LoaiSanPhams.OrderBy(n => n.TenLoaiSanPham), "MaLoaiSanPham", "TenLoaiSanPham");
            ViewBag.MaNhaSanXuat = new SelectList(db.NhaSanXuats.OrderBy(n => n.TenNhaSanXuat), "MaNhaSanXuat", "TenNhaSanXuat");
            ViewBag.MaNhom = new SelectList(db.NhomMuas.OrderBy(n => n.TenNhom), "MaNhom", "TenNhom");
            List<string> filename = new List<string>();
            if (photo[0] == null || photo[1] == null || photo[2] == null)
            {
                ViewBag.upload2 = "Thêm đầy đủ 3 hình ảnh";
            }

            if (photo[0] != null && photo[1] != null && photo[2] != null)
            {
                // kiểm tra xem có phải hình ảnh hay không
                for (int i = 0; i < 3; i++)
                {
                    if (photo[i].ContentLength > 0)
                    {
                        // kiểm tra định dạng hình ảnh
                        if (photo[i].ContentType != "image/jpeg" && photo[i].ContentType != "image/png" && photo[i].ContentType != "image/gif")
                        {
                            ViewBag.upload = "Hình Ảnh không hợp lệ!!!";
                        }
                        else
                        {
                            var fileName = Path.GetFileName(photo[i].FileName);
                            string[] filenames = fileName.ToString().Split('.');
                            filename.Add(filenames[0]);
                            //lấy tên hình ảnh truyền vào
                            var path = Path.Combine(Server.MapPath("~/Content/SanPham"), fileName);
                            //so sánh tên với tên hình ảnh có sẵn
                            if (System.IO.File.Exists(path))
                            {
                                ////nếu đã tồn tại hình ảnh 
                                //ViewBag.upload = "Hình Ảnh đã tồn tại!!!";
                            }
                            else
                            {
                                //lưu hình ảnh vào ~/Content/SanPham
                                photo[i].SaveAs(path);
                            }

                        }
                    }
                }
                sp.HinhAnh = filename[0];
                sp.HinhAnh2 = filename[1];
                sp.HinhAnh3 = filename[2];
                int ma = spdao.Add(sp);
                return RedirectToAction("Index", "Dashboard");
            }
            return View(sp);
        }
        public ActionResult Delete(int masp)
        {
            spdao.Delete(masp);
            return RedirectToAction("Index");
        }
    }

}