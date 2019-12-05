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
        public StoreContext context = StoreContext.getInstance();

        public ActionResult Index(string query)
        {
            //var products = context.Productos.ToList();
            //return View(products); 
            return View();
        }

        public ActionResult Buscar (string query)
        {
            var datos = new List<Producto>();
   
            if (query == null || query == "")
            {
                datos = context.Productos.ToList();
            }
            else
            {
                datos = context.Productos.Where(o => o.nombre.Contains(query)).ToList();
            }
            ViewBag.datos = query;
            return View(datos);
            
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