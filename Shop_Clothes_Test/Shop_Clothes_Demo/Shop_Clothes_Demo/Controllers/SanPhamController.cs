using Shop_Clothes_Demo.Models;
using Shop_Clothes_Demo.Models.DAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;

namespace Shop_Clothes_Demo.Controllers
{
    public class SanPhamController : Controller
    {
        // GET: SanPham
        ShopModel db = new ShopModel();
        SanPhamDAO dao = new SanPhamDAO();
        public ActionResult SanPham(int? MaNhom, int? MaLoaiSanPham, int? page)
        {
            // Load SP theo 2 tiêu chí mã nhóm, mã loại sp trong bảng sản phẩm
            if (MaNhom == null || MaLoaiSanPham == null)
            {
                return RedirectToAction("Index", "Intro");
            }

            int pageN = (page ?? 1);
            var lstSP = dao.LoadSanPhamTheoTieuChi(pageN, 6, MaNhom, MaLoaiSanPham);

            ViewBag.TenLoai = (from n in db.LoaiSanPhams where n.MaLoaiSanPham == MaLoaiSanPham select n).First().TenLoaiSanPham;
            ViewBag.TenNhom = (from n in db.NhomMuas where n.MaNhom == MaNhom select n).First().TenNhom;

            if (lstSP.Count() == 0)
            {
                return RedirectToAction("Index", "Intro");
            }
            return View(lstSP);
       
        }
        public ActionResult Details(int? MaSanPham)
        {
            var sp = dao.findSPByID(MaSanPham);
            var spTT = dao.SanPhamTuongTu(sp.MaNhaSanXuat.ToString());
            ViewBag.listsptt = spTT;
            NhaSanXuatDAO daoNSX = new NhaSanXuatDAO();
            ViewBag.ThongTinThem = daoNSX.thongTin(MaSanPham.ToString());
            PhanHoiDAO phdao = new PhanHoiDAO();
            ViewBag.BinhLuan = phdao.listPhanHoi(MaSanPham.ToString());
            return View(sp);
        }
        public ActionResult SanPhamTuongTuPartialView()
        {
            return PartialView();
        }
    }
}