using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Guanghui.Weather.BLL;
using Guanghui.Weather.Model;

namespace Guanghui.Weather.Webapp.Controllers
{
    public class AdminController : Controller
    {
        // GET: Admin
        public ActionResult Index()
        {
            if (Session["Admin"] == null)
            {
                return RedirectToAction("Index", "Login");
            }
            return View();
        }

        //CityBll _cityBll = new CityBll();

        //public IEnumerable<City> Get()
        //{
        //    return _cityBll.GetAll();
        //}

    }
}