using DIARS_PROYECTO_FINAL.BD;
using DIARS_PROYECTO_FINAL.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DIARS_PROYECTO_FINAL.Controllers
{
    public class CarouselController : Controller
    {
        public StoreContext context = StoreContext.getInstance();
        public ActionResult Index()
        {
            var carousel = context.Carousels.ToList();
            return View(carousel);
        }

        public ActionResult Comprar() {
            return View();
        }
        public ActionResult Crear() {
            return View();
        }
        [HttpPost]
        public ActionResult Crear(Carousel carousel, HttpPostedFileBase file)
        {
            if (file != null && file.ContentLength > 0)
            {
                string ruta = Path.Combine(Server.MapPath("~/imagenes"), Path.GetFileName(file.FileName));
                file.SaveAs(ruta);
                carousel.imagen = "/imagenes/" + Path.GetFileName(file.FileName);
            }
            if (ModelState.IsValid)
            {
                context.Carousels.Add(carousel);
                context.SaveChanges();

                return RedirectToAction("Index", "Home");
            }
            return View(carousel);
        }

        public ActionResult Eliminar( int ID) {
            Carousel carousel = context.Carousels.Where(a => a.Id == ID).First();
            context.Carousels.Remove(carousel);
            context.SaveChanges();
            return RedirectToAction("Index","Home");
        }

        public ActionResult IndexPrincipal()
        {
            var carousel = context.Carousels.ToList();
            return View(carousel);
        }
    }
}