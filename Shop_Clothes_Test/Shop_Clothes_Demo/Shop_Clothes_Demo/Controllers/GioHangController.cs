using Shop_Clothes_Demo.Models;
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
            int So = Convert.ToInt32(f["SoLuong"]);
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
            
            int SL = Convert.ToInt32(f["SoLuongSP"]);
            //update so luong
            ItemGioHang it = item.FirstOrDefault();
            it.SoLuong = SL;
            it.ThanhTien = SL * it.DonGia;

            return Redirect(strURL);
        }
        public ActionResult ThanhToan()
        {
            return View();
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