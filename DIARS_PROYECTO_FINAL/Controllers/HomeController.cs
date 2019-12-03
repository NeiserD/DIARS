using DIARS_PROYECTO_FINAL.BD;
using DIARS_PROYECTO_FINAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DIARS_PROYECTO_FINAL.Controllers
{
    public class HomeController : Controller
    {
        public StoreContext context = StoreContext.getInstance();

        public ActionResult Index()
        {
            var products = context.Productos.ToList();
            return View(products);
        }
        
        public ActionResult UserDashBoard()
        {
            return View();
        }

        public ActionResult Ubication() {
            return View();
        }

        [HttpGet]
        public ActionResult enviarCorreo() {

            return View();
        }

        //[HttpPost]
        //public ActionResult enviarCorreo()
        //{

        //    return View();
        //}

    }
}