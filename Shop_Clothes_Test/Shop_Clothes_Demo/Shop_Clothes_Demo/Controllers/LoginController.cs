using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Shop_Clothes_Demo.Controllers
{
    public class LoginController : Controller
    {
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
            return RedirectToAction("Index", "Intro");
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
            string user = f["us"];
            string pass = f["pw"];
            string rpass = f["rpw"];
            //thanhf coong ve form dang nhap
            //that bai thong bao va du nguyen view
            return View();
        }
        [HttpPost]
        public ActionResult ForgotAction(FormCollection f)
        {
            string SDT = f["sdt"];
            //thanh cong quay ve index
            //
            return View();
        }
        public ActionResult SignOut()
        {
            Session["username"] = null;
            return RedirectToAction("Index", "Intro");
        }
    }
}