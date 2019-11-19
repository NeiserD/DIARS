using DIARS_PROYECTO_FINAL.BD;
using DIARS_PROYECTO_FINAL.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DIARS_PROYECTO_FINAL.Controllers
{
    public class OfertaController : Controller
    {
        StoreContext context = new StoreContext();
       
        public ActionResult Index()
        {
            var oferta = context.ofertas.ToList();
            return View(oferta);
        }

        // GET: Oferta/Create
        
        public ActionResult Crear()
        {
            return View(new Oferta());
        }

        // POST: Oferta/Create
       
        [HttpPost]
        public ActionResult Crear(Oferta oferta, HttpPostedFileBase file)
        {
            ValidarOfer(oferta);

            if (file != null && file.ContentLength > 0)
            {
                string ruta = Path.Combine(Server.MapPath("~/imagenes"), Path.GetFileName(file.FileName));
                file.SaveAs(ruta);
                oferta.imagen = "/imagenes/" + Path.GetFileName(file.FileName);
            }
            if (ModelState.IsValid)
            {
                oferta.fechaInicio = DateTime.Now;
                context.ofertas.Add(oferta);
                context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(oferta);
        }

        // GET: Oferta/Edit/5
        
        public ActionResult Editar(int ID)
        {

            var oferta = context.ofertas.Where(o=>o.id== ID).First();

            return View(oferta);
        }

        // POST: Oferta/Edit/5
        
        [HttpPost]
        public ActionResult Editar(Oferta oferta, HttpPostedFileBase file)
        {
            
            ValidarOfer(oferta);
            if (ModelState.IsValid)
            {
                context.Entry(oferta).State = EntityState.Modified;
                context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(oferta);

            //try
            //{
            //    context.Entry(oferta).State = EntityState.Modified;
            //    context.SaveChanges();
            //    return RedirectToAction("Index");
            //}
            //catch
            //{
            //    return View();
            //}
        }

        // GET: Oferta/Delete/5
        
        public ActionResult Eliminar(int ID)
        {
            Oferta oferta = context.ofertas.Where(x=>x.id== ID).First();
            context.ofertas.Remove(oferta);
            context.SaveChanges();
            return RedirectToAction("Index");
        }

        public void ValidarOfer(Oferta oferta)
        {
            if (oferta.nombre == null || oferta.nombre == "")
                ModelState.AddModelError("Nombre", "El campo  es obligatorio");
            if (oferta.fechaInicio== null)
                ModelState.AddModelError("FechaInicio", "El campo  es obligatorio");
            if (oferta.fechaFin == null)
                ModelState.AddModelError("FechaFin", "El campo  es obligatorio");
            if (oferta.descripcion == null || oferta.descripcion == "")
                ModelState.AddModelError("Descripcion", "El campo  es obligatorio");

        }

    }
}
