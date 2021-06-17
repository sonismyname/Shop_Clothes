using Shop_Clothes_Demo.Models;
using Shop_Clothes_Demo.Models.DAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Shop_Clothes_Demo.Controllers
{
    public class LoginController : Controller
    {
        ShopModel db = new ShopModel();
        ThanhVienDAO daotv = new ThanhVienDAO();
        KhachHangDAO daokh = new KhachHangDAO();
        // GET: Login
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult LoginAction(FormCollection f)
        {
            string userName = f["us"];
            string pass = f["pw"];
            Session["username"] = userName;
            //kt login
            return RedirectToAction("Index", "Dashboard");
        }
        public ActionResult Register()
        {
            return View();
        }
        public ActionResult ForgetPassWord()
        {
            return View();
        }
        [HttpPost]
        public ActionResult RegisterAction(FormCollection f)
        {
            Session["username"] = null;
            Session["kqdk"] = null;
            List<string> listTTDangKi = new List<string>();
            ViewBag.thieuTTDK = null;
            if (f["hoten"].Equals("Hãy nhập họ tên...") || f["diachi"].Equals("Hãy nhập địa chỉ...") || f["email"].Equals("Định dạng: abc@gmail.com...") || f["sdt"].Equals("Hãy nhập số điện thoại...") || f["taikhoan"].Equals("Tài khoản...") || f["matkhau"].Equals("Mật khẩu...") || f["rematkhau"].Equals("Mật khẩu...") || !f["matkhau"].Equals(f["rematkhau"]))
            {
                listTTDangKi.Add(f["hoten"]);
                listTTDangKi.Add(f["diachi"]);
                listTTDangKi.Add(f["email"]);
                listTTDangKi.Add(f["sdt"]);
                listTTDangKi.Add(f["taikhoan"]);
                listTTDangKi.Add(f["matkhau"]);
                listTTDangKi.Add(f["rematkhau"]);
                if(!f["matkhau"].Equals(f["rematkhau"]))
                {
                    listTTDangKi.Add("Mật khẩu nhập lại phải giống nhau");
                }  
                else
                {
                    listTTDangKi.Add("Hãy nhập thông đầy đủ thông tin");
                }                 
                Session["lstDangKi"] = listTTDangKi;
                return RedirectToAction("Register", "Login");
            }
            ThanhVien tv = new ThanhVien();
            //tv.MaLoaiThanhVien=1; thêm khi nào có mã loại thành viên
            tv.TaiKhoan = f["taikhoan"];
            tv.HoTen = f["hoten"];
            tv.DiaChi = f["diachi"];
            tv.SoDienThoai = f["sdt"];
            tv.MatKhau = f["matkhau"];
            tv.Email = f["email"];
            int maThanhVien = 0;
            maThanhVien = daotv.Add(tv);
            if(maThanhVien != -1)
            {
                //thêm thành công
                Session["username"] = tv;
                KhachHang kh = new KhachHang();
                kh.MaThanhVien = maThanhVien;
                kh.TenKhachHang = tv.HoTen;
                kh.DiaChi = tv.DiaChi;
                kh.Email = tv.Email;
                kh.SoDienThoai = tv.SoDienThoai;
                daokh.Add(kh);
                return RedirectToAction("Index", "Intro");   
            }    
            else
            {
                //không thành công
                Session["kqdk"] = "Tài khoản đã tồn tại";
                return RedirectToAction("Register", "Login");
            }      
            //thanhf coong ve form dang nhap
            //that bai thong bao va du nguyen view
        }
        [HttpPost]
        public ActionResult ForgotAction(FormCollection f)
        {
            string SDT = f["sdt"];
            //thanh cong quay ve index
            //
            return View();
        }
        public ActionResult thongTinTaiKhoan()
        {
            if (Session["username"] != null)
            {

                ViewBag.LoaiTV = (db.LoaiThanhViens.Find((Session["username"] as ThanhVien).MaLoaiThanhVien)).TenLoaiThanhVien;
                return View();
            }
            else
                return RedirectToAction("Index", "Intro");
        }
        [HttpPost]
        public ActionResult CapNhat(FormCollection f)
        {
            return View();
        }

        public ActionResult ListDonHang(int MaThanhVien)
        {
            DonDatHangDAO dao = new DonDatHangDAO();

            return View(dao.listDH(MaThanhVien));
        }

        public ActionResult ChiTietDonHang(int MaDonDatHang)
        {
            ChiTietDonHangDAO dao = new ChiTietDonHangDAO();
            if(Session["username"]!=null)
            {
                return View(dao.Detail(MaDonDatHang));
            }
            return RedirectToAction("Index", "Intro");
        }

        public ActionResult SignOut()
        {
            Session["username"] = null;
            Session["giaohang"] = null;
            return RedirectToAction("Index", "Intro");
        }
    }
}