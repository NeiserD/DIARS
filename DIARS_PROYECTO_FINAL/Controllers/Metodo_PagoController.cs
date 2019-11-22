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
    public class Metodo_PagoController : Controller
    {
        StoreContext context = new StoreContext();
      
        public ActionResult Index()
        {
            var pagos = context.metodoPagos.ToList();
            return View(pagos);
        }



        // GET: Metodo_Pago/Create
  
        public ActionResult Crear()
        {
            return View(new MetodoPago());
        }

        // POST: Metodo_Pago/Create
 
        [HttpPost]
        public ActionResult Crear(MetodoPago metodoPago)
        {
            ValidarMetPag(metodoPago);

            if (ModelState.IsValid)
            {
                context.metodoPagos.Add(metodoPago);
                context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(metodoPago);
        }
     
        [HttpGet]
        public ActionResult Editar(int ID)
        {
            var metodoPag = context.metodoPagos.Where(x=>x.id==ID).First();
            
            return View(metodoPag);
        }

        
      
        [HttpPost]
        public ActionResult Editar(MetodoPago metodoPago)
        {

            ValidarMetPag(metodoPago);
            if (ModelState.IsValid)
            {
                context.Entry(metodoPago).State = EntityState.Modified;
                context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(metodoPago);
            
        }
     

        [HttpGet]
        public ActionResult Eliminar(int ID)
        {
            MetodoPago metodoPago = context.metodoPagos.Where(x=>x.id==ID).First();
            context.metodoPagos.Remove(metodoPago);
            context.SaveChanges();
            return RedirectToAction("Index");
        }

        public void ValidarMetPag(MetodoPago metodoPago)
        {
            if (metodoPago.nombre == null || metodoPago.nombre == "")
                ModelState.AddModelError("Nombre", "El campo  es obligatorio");
        }
    }
}
