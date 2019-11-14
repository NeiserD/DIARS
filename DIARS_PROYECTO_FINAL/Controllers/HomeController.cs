using DIARS_PROYECTO_FINAL.BD;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DIARS_PROYECTO_FINAL.Controllers
{
    public class HomeController : Controller
    {
        public StoreContext context = new StoreContext();

        public ActionResult Index()
        {
            var products = context.Productos.ToList();
            return View(products);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult Map() {
            return View();
        }

    }
}