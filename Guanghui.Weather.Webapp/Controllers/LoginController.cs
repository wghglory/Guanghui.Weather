using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Guanghui.Weather.Webapp.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult SignIn()
        {
            Session["Admin"] = "admin";
            return RedirectToAction("Index", "Admin");
        }
    }
}