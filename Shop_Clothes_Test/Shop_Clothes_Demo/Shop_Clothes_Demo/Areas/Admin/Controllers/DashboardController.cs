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
            ThanhVien x = Session["admin"] as ThanhVien;
            if (x == null)
            {
                
                return RedirectToAction("Index", "../Intro/Index");
            }
            else
            {
                int pageS = 5;
                int pageN = (page ?? 1);

                ViewBag.MLSP = new SelectList(db.LoaiSanPhams.OrderBy(n => n.TenLoaiSanPham), "MaLoaiSanPham", "TenLoaiSanPham");
                ViewBag.MNSX = new SelectList(db.NhaSanXuats.OrderBy(n => n.TenNhaSanXuat), "MaNhaSanXuat", "TenNhaSanXuat");
                ViewBag.MN = new SelectList(db.NhomMuas.OrderBy(n => n.TenNhom), "MaNhom", "TenNhom");

                return View(spdao.LoadSanPhamAdmin(pageN, pageS));

            }
        }
        [HttpGet]
        public ActionResult AddProduct(SanPham sp)
        {
            ThanhVien x = Session["admin"] as ThanhVien;
            if (x == null)
            {
                /*Session["deny_access"] = "Hãy nhập thông tin tài khoản";*/
                return RedirectToAction("Index", "../Intro/Index");
            }
            else
            {
                ViewBag.MaLoaiSanPham = new SelectList(db.LoaiSanPhams.OrderBy(n => n.TenLoaiSanPham), "MaLoaiSanPham", "TenLoaiSanPham");
                ViewBag.MaNhaSanXuat = new SelectList(db.NhaSanXuats.OrderBy(n => n.TenNhaSanXuat), "MaNhaSanXuat", "TenNhaSanXuat");
                ViewBag.MaNhom = new SelectList(db.NhomMuas.OrderBy(n => n.TenNhom), "MaNhom", "TenNhom");
                return View(sp);
            }          
        }
        [ValidateInput(false)]// tắt ktra mota của microsoft
        [HttpPost]
        public ActionResult AddProduct(SanPham sp, HttpPostedFileBase[] photo)
        {
            ViewBag.MaLoaiSanPham = new SelectList(db.LoaiSanPhams.OrderBy(n => n.TenLoaiSanPham), "MaLoaiSanPham", "TenLoaiSanPham");
            ViewBag.MaNhaSanXuat = new SelectList(db.NhaSanXuats.OrderBy(n => n.TenNhaSanXuat), "MaNhaSanXuat", "TenNhaSanXuat");
            ViewBag.MaNhom = new SelectList(db.NhomMuas.OrderBy(n => n.TenNhom), "MaNhom", "TenNhom");

            List<string> filename = new List<string>();

            if (photo[0] == null || photo[1] == null || photo[2] == null)
            {
                ViewBag.upload = "Thêm đầy đủ 3 hình ảnh";
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
                            return View(sp);
                        }
                        else
                        {
                            var fileName = Path.GetFileName(photo[i].FileName);
                            filename.Add(fileName);
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
                sp.SoLuongMua = 0;
                int ma = spdao.Add(sp);
                return RedirectToAction("Index", "Dashboard");
            }
            return View(sp);
        }
        public ActionResult Delete(int masp)
        {
            ThanhVien x = Session["admin"] as ThanhVien;
            if (x == null)
            {
                /*Session["deny_access"] = "Hãy nhập thông tin tài khoản";*/
                return RedirectToAction("Index", "../Intro/Index");
            }
            else
            {
                spdao.Delete(masp);
                return RedirectToAction("Index");
            }
           
        }
        public ActionResult Details(int MaSanPham)
        {
            ThanhVien x = Session["admin"] as ThanhVien;
            if (x == null)
            {
                /*Session["deny_access"] = "Hãy nhập thông tin tài khoản";*/
                return RedirectToAction("Index", "../Intro/Index");
            }
            else
            {
                SanPham sp = db.SanPhams.Find(MaSanPham);
                ViewBag.tnsx = sp.NhaSanXuat.TenNhaSanXuat.ToString();
                ViewBag.tlsp = sp.LoaiSanPham.TenLoaiSanPham.ToString();
                return View(sp);
            }      
        }
        public ActionResult Edit(int MaSanPham)
        {
            ThanhVien x = Session["admin"] as ThanhVien;
            if (x == null)
            {
                /*Session["deny_access"] = "Hãy nhập thông tin tài khoản";*/
                return RedirectToAction("Index", "../Intro/Index");
            }
            else
            {
                SanPham sp = db.SanPhams.Find(MaSanPham);
                if (sp == null)
                {
                    return HttpNotFound();
                }
                ViewBag.MLSP = new SelectList(db.LoaiSanPhams.OrderBy(n => n.TenLoaiSanPham), "MaLoaiSanPham", "TenLoaiSanPham", sp.MaLoaiSanPham);
                ViewBag.MNSX = new SelectList(db.NhaSanXuats.OrderBy(n => n.TenNhaSanXuat), "MaNhaSanXuat", "TenNhaSanXuat", sp.MaNhaSanXuat);
                ViewBag.MN = new SelectList(db.NhomMuas.OrderBy(n => n.TenNhom), "MaNhom", "TenNhom", sp.MaNhom);
                Session["edit"] = sp;
                return View(sp);
            }
        }
        [ValidateInput(false)]// tắt ktra mota của microsoft ở mô tả
        [HttpPost]
        public ActionResult Edit(SanPham sp, HttpPostedFileBase[] photo)
        {
            ViewBag.MLSP = new SelectList(db.LoaiSanPhams.OrderBy(n => n.TenLoaiSanPham), "MaLoaiSanPham", "TenLoaiSanPham", sp.MaLoaiSanPham);
            ViewBag.MNSX = new SelectList(db.NhaSanXuats.OrderBy(n => n.TenNhaSanXuat), "MaNhaSanXuat", "TenNhaSanXuat", sp.MaNhaSanXuat);
            ViewBag.MN = new SelectList(db.NhomMuas.OrderBy(n => n.TenNhom), "MaNhom", "TenNhom", sp.MaNhom);

            sp.MaSanPham = (Session["edit"] as SanPham).MaSanPham;
            sp.HinhAnh = (Session["edit"] as SanPham).HinhAnh;
            sp.HinhAnh2 = (Session["edit"] as SanPham).HinhAnh2;
            sp.HinhAnh3 = (Session["edit"] as SanPham).HinhAnh3;
            //sp.MoTa = (Session["edit"] as SanPham).MoTa;

            Session["edit"] = null;
            
            List<string> filename = new List<string>();
            for (int i = 0; i < 3; i++)
            {
                if (photo[i] != null)
                {
                    if (photo[i].ContentLength > 0)
                    {
                        // kiểm tra định dạng hình ảnh
                        if (photo[i].ContentType != "image/jpeg" && photo[i].ContentType != "image/png" && photo[i].ContentType != "image/gif")
                        {
                            ViewBag.upload = "Hình Ảnh không hợp lệ!!!";
                            return View(sp);
                        }
                        else
                        {
                            var fileName = Path.GetFileName(photo[i].FileName);
                            filename.Add(fileName);
                            //lấy tên hình ảnh truyền vào
                            var path = Path.Combine(Server.MapPath("~/Content/SanPham"), fileName);
                            //so sánh tên với tên hình ảnh có sẵn
                            if (System.IO.File.Exists(path))
                            {
                            }
                            else
                            {
                                photo[i].SaveAs(path);
                            }

                        }
                    }
                    else
                    {
                        return View("Edit");
                    }
                }
                filename.Add("");
            }
            if(photo[0]!= null)
            {
                //System.IO.File.Delete("~/Content/SanPham/" + filename);
                sp.HinhAnh = filename[0];
            }
            if (photo[1] != null)
            {
                sp.HinhAnh2 = filename[1];
            }
            if (photo[2] != null)
            {
                sp.HinhAnh3 = filename[2];
            }
            sp.NgayCapNhat = DateTime.Now;
            db.Entry(sp).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Index", "Dashboard");
        }
        public ActionResult ThongTinTaiKhoan()
        {
            ThanhVien x = Session["admin"] as ThanhVien;
            if (x == null)
            {
                return RedirectToAction("Index", "../Intro/Index");
            }
            else
            {
                return View(x);
            }
        }
        public ActionResult DoiMatKhau(int? id)
        {
            return RedirectToAction("ThongTinTaiKhoan", "Dashboard");
        }
        public ActionResult SignOut()
        {
            Session["admin"] = null;
            return RedirectToAction("Index", "../Intro/Index");
        }
        [HttpPost]
        public ActionResult GetKey(string tukhoa, int Malsp, int Mansx, int Man, int Min, int Max)
        {
            return RedirectToAction("TimKiem", new { @tukhoa = tukhoa, @Malsp = Malsp, @Mansx = Mansx, @Man = Man, @Min = Min, @Max = Max });
        }
        [HttpGet]
        public ActionResult TimKiem(int? page, string tukhoa, int Malsp, int Mansx, int Man, int Min, int Max)
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
                ViewBag.min = Min;
                ViewBag.max = Max;

                ViewBag.MLSP = new SelectList(db.LoaiSanPhams.OrderBy(n => n.TenLoaiSanPham), "MaLoaiSanPham", "TenLoaiSanPham");
                ViewBag.MNSX = new SelectList(db.NhaSanXuats.OrderBy(n => n.TenNhaSanXuat), "MaNhaSanXuat", "TenNhaSanXuat");
                ViewBag.MN = new SelectList(db.NhomMuas.OrderBy(n => n.TenNhom), "MaNhom", "TenNhom");

                return View(spdao.timKiem(pageN, pageS, tukhoa, Malsp, Mansx, Man, Min, Max));
            }
        }
    }

}