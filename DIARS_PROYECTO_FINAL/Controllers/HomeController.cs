using DIARS_PROYECTO_FINAL.BD;
using DIARS_PROYECTO_FINAL.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
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
        
        public ActionResult UserDashBoard()
        {
            return View();
        }

        public ActionResult Map() {
            return View();
        }

        [HttpGet]
        public ActionResult enviarCorreo() {

            return View();
        }

        
        public ActionResult Conocenos()
        {

            return View();
        }

    }
}