using Shop_Clothes_Demo.Models;
using Shop_Clothes_Demo.Models.DAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Shop_Clothes_Demo.Controllers
{
    public class GioHangController : Controller
    {
        ShopModel db = new ShopModel();
        SanPhamDAO daosp = new SanPhamDAO();
        KhachHangDAO daokh = new KhachHangDAO();
        DonDatHangDAO daodh = new DonDatHangDAO();
        ChiTietDonHangDAO daoct = new ChiTietDonHangDAO();
        //lay gio hang
        public List<ItemGioHang> getGioHang()
        {
            //giỏ hàng đã tồn tại
            List<ItemGioHang> lstGH = Session["GioHang"] as List<ItemGioHang>;
            if(lstGH==null)
            {
                // chưa có Session
                lstGH = new List<ItemGioHang>();
                Session["GioHang"] = lstGH;
            }    
            return lstGH;
        }
        //add gio hang
        public ActionResult ThemGioHang(int MaSanPham, string strURL)
        {
            
            SanPham sp = db.SanPhams.SingleOrDefault(n => n.MaSanPham == MaSanPham);
            if(sp==null)
            {
                Response.StatusCode = 404;
                return null;
            }    
            else
            {
                //lay gio hang
                List<ItemGioHang> listGH = getGioHang();
                //TH1: sp đã E
                ItemGioHang spCheck = listGH.SingleOrDefault(n => n.MaSP== MaSanPham);
                if(spCheck!=null)
                {
                    if(sp.SoLuongTon<spCheck.SoLuong)
                    {
                        return View("ThongBao");
                    }    
                    spCheck.SoLuong++;
                    spCheck.ThanhTien = spCheck.SoLuong * spCheck.DonGia;
                    return Redirect(strURL);
                }
                
                ItemGioHang itemGH = new ItemGioHang(MaSanPham);
                if (sp.SoLuongTon < itemGH.SoLuong)
                {
                    return View("ThongBao");
                }
                listGH.Add(itemGH);
                return Redirect(strURL);
            }    
        }

        [HttpPost]
        public ActionResult ThemGioHang(FormCollection f, int MaSanPham, string strURL)
        {
            int So;
            try
            {
                So = Convert.ToInt32(f["SoLuong"]);
            }
            catch
            {
                So = 1;
            }
            
            string size =f["_size"];
            string sizeF = KichThuoc(Convert.ToInt32(size));
            SanPham sp = db.SanPhams.SingleOrDefault(n => n.MaSanPham == MaSanPham);
            if (sp == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            else
            {
                //lay gio hang
                List<ItemGioHang> listGH = getGioHang();
                //TH1: sp đã E
                var item = from n in listGH where n.Size == sizeF && n.MaSP == MaSanPham select n;
                ItemGioHang spCheck = item.FirstOrDefault();
                
                if (spCheck != null)
                {
                    //size giông nhau
                    if (sp.SoLuongTon < spCheck.SoLuong)
                    {
                        return View("ThongBao");
                    }
                    spCheck.SoLuong += So;
                    spCheck.ThanhTien = spCheck.SoLuong * spCheck.DonGia;
                    return Redirect(strURL);
                }
                ItemGioHang itemGH = new ItemGioHang(MaSanPham,So, sizeF);
                if (sp.SoLuongTon < itemGH.SoLuong)
                {
                    return View("ThongBao");
                }
                listGH.Add(itemGH);
                return Redirect(strURL);
            }
        }

        public ActionResult XoaItemGioHang(int MaSanPham, string size)
        {
            List<ItemGioHang> lst = Session["GioHang"] as List<ItemGioHang>;
            var item = from n in lst where n.MaSP == MaSanPham && n.Size == size select n;
            ItemGioHang it = item.FirstOrDefault();
            if(it!=null)
            {
                lst.Remove(it);
            }    
            Session["GioHang"] = lst;
            return RedirectToAction("XemGioHang", "GioHang");
        }
        public double TinhTongSoLuong()
        {
            //lay gio hang
            List<ItemGioHang> lstGH = Session["GioHang"] as List<ItemGioHang>;
            int sl = 0;
            if(lstGH==null)
            {
                return 0;
            }
            foreach (ItemGioHang item in lstGH)
            {
                sl = sl + item.SoLuong;
            }
            return sl;
        }
        public decimal TinhTien()
        {
            //lay gio hang
            List<ItemGioHang> lstGH = Session["GioHang"] as List<ItemGioHang>;
            if (lstGH == null)
            {
                return 0;
            }
            decimal price = 0;
            foreach (ItemGioHang item in lstGH)
            {
                price += item.ThanhTien;
            }
            return price;
        }
        
        public ActionResult XemGioHang()
        {
            ViewBag.TongTien = TinhTien();
            List<ItemGioHang> lst = getGioHang();
            /*if(lst.Count<=0)
            {
                return RedirectToAction("Index", "Intro");
            }*/
            return View(lst);
        }
        public ActionResult GioHangPartial()
        {
            if(TinhTongSoLuong() ==0)
            {
                ViewBag.TongSoLuong = 0;
                ViewBag.TongTien = 0;
                return PartialView();
            }
            ViewBag.TongSoLuong = TinhTongSoLuong();
            ViewBag.TongTien = TinhTien();
            return PartialView();
        }
        public ActionResult CapNhatSoLuong(int MaSanPham, string size, string strURL, FormCollection f)
        {
            List<ItemGioHang> lst = Session["GioHang"] as List<ItemGioHang>;

            var item = from n in lst where n.MaSP==MaSanPham && n.Size==size select n;
            int SL;
            try 
            {
                SL = Convert.ToInt32(f["SoLuongSP"]);
            } catch
            {
                SL = 1;
            }
            
            //update so luong
            ItemGioHang it = item.FirstOrDefault();
            it.SoLuong = SL;
            it.ThanhTien = SL * it.DonGia;

            return Redirect(strURL);
        }
        public ActionResult ThanhToan()
        {
            List<ItemGioHang> lst = getGioHang();
            if (lst.Count <= 0)
            {
                return RedirectToAction("XemGioHang", "GioHang");
            }
            return View();
        }
        [HttpPost]
        public ActionResult ThanhToan(FormCollection f)
        {
            ThanhVien tv = Session["username"] as ThanhVien;
            //KhachHang ks = daokh;
            if (f["tenkhachhang"].Equals("Hãy nhập họ tên...") || f["diachi"].Equals("Hãy nhập địa chỉ...") || f["sodienthoai"].Equals("Hãy nhập số điện thoại..."))
            {
                Session["thongbao"] = "Hãy nhập đầy đủ thông tin !!";
                return RedirectToAction("ThanhToan");
            }
            Session["thongbao"] = null;
            int maKhachHang;
            if(Session["username"] != null)
            {
                //lưu đơn hàng theo thành viên
                KhachHang ks = db.KhachHangs.SingleOrDefault(n=>n.MaThanhVien==tv.MaThanhVien);
                maKhachHang = ks.MaKhachHang;
            }    
            else
            {
                //tạo đơn hàng và khách hàng mới
                KhachHang ks = new KhachHang();
                ks.TenKhachHang = f["tenkhachhang"];
                ks.DiaChi = f["diachi"];
                ks.SoDienThoai = f["sodienthoai"];
                maKhachHang = daokh.Add(ks);
            }    
            //Lưu dơn hàng từ session
            DonDatHang ddh = new DonDatHang();
            ddh.MaKhachHang = maKhachHang;
            ddh.NgayDat = DateTime.Now;
            DateTime ngayGiao = DateTime.Now;
            ngayGiao = ngayGiao.AddDays(2);
            ddh.DaThanhToan = false;
            ddh.TinhTrangGiaoHang = "Đang giao";
            ddh.NgayGiaoDuKien = ngayGiao;
            int maDonHang = daodh.Add(ddh);
            //lưu chi tiết đơn hàng
            List<ItemGioHang> lst = Session["GioHang"] as List<ItemGioHang>;
            foreach (ItemGioHang item in lst)
            {
                ChiTietDonHang ct = new ChiTietDonHang();
                ct.MaSanPham = item.MaSP;
                ct.DonGia = item.DonGia;
                ct.MaDonDatHang = maDonHang;
                ct.SoLuong = item.SoLuong;
                ct.TenSanPham = item.TenSP;
                if (daoct.Add(ct) != -1)
                {
                    daosp.MuaHang(item.MaSP, item.SoLuong);
                    //thay đổi số liệu Sản Phẩm
                }
            }
            Session["GioHang"] = null;
            return RedirectToAction("Index", "Intro");
        }

        public string KichThuoc(int value)
        {
            string val = "";
            switch(value)
            {
                case 1:
                    {
                        val = "M";
                        break;
                    }
                case 2:
                    {
                        val = "L";
                        break;
                    }
                case 3:
                    {
                        val = "XL";
                        break;
                    }
                case 4:
                    {
                        val = "FS";
                        break;
                    }
                case 5:
                    {
                        val = "S";
                        break;
                    }
                default:
                    {
                        val = "-1";
                        break;
                    }

            }
            return val;
        }
    }
}