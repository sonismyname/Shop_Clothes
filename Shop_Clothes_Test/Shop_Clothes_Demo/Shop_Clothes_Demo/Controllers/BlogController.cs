using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Shop_Clothes_Demo.Controllers
{
    public class BlogController : Controller
    {
        // GET: Blog
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Bloger()
        {
            return View();
        }
        public ActionResult Contact()
        {
            return View();
        }
    }
}