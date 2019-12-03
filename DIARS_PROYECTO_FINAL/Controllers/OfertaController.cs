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
        StoreContext context = StoreContext.getInstance();
       
        public ActionResult Index()
        {
            var oferta = context.Ofertas.ToList();
            return View(oferta);
        }
        //Ajax for home index
        public ActionResult Ofertas()
        {
            var oferta = context.Ofertas.ToList();
            return View(oferta);
        }

        public ActionResult Especificaciones(int ID)
        {
            Oferta oferta = context.Ofertas.Find(ID);

            return View(oferta);
        }

        public ActionResult Crear()
        {
            return View(new Oferta());
        }

        [HttpPost]
        public ActionResult Crear(Oferta oferta, HttpPostedFileBase file)
        {
           
            if (file != null && file.ContentLength > 0)
            {
                string ruta = Path.Combine(Server.MapPath("~/imagenes"), Path.GetFileName(file.FileName));
                file.SaveAs(ruta);
                oferta.imagen = "/imagenes/" + Path.GetFileName(file.FileName);
            }
          
            ValidarOferta(oferta);
            if (ModelState.IsValid)
            {
                calcularOferta(oferta);
                oferta.fechaInicio = DateTime.Now;
                context.Ofertas.Add(oferta);
                context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(oferta);
        }
        
        public ActionResult Editar(int ID)
        {
            var oferta = context.Ofertas.Where(o => o.id == ID).First();
            return View(oferta);
        }
        
        [HttpPost]
        public ActionResult Editar(Oferta oferta)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    Oferta ofert= context.Ofertas.Where(x => x.id == oferta.id).First();
                    ofert.nombre = oferta.nombre;
                    ofert.descripcion = oferta.descripcion;
                    ofert.fechaFin = oferta.fechaFin;
                    ofert.fechaInicio = oferta.fechaInicio;
                    context.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            catch (Exception)
            {
                return View(oferta);
            }
            return View(oferta);
        }
        
        public ActionResult Eliminar(int ID)
        {
            Oferta oferta = context.Ofertas.Where(x => x.id == ID).First();
            context.Ofertas.Remove(oferta);
            context.SaveChanges();
            return RedirectToAction("Index");
        }

        public void ValidarOfer(Oferta oferta)
        {
            if (oferta.nombre == null || oferta.nombre == "")
                ModelState.AddModelError("Nombre", "El campo  es obligatorio");
            if (oferta.fechaInicio == null)
                ModelState.AddModelError("FechaInicio", "El campo  es obligatorio");
            if (oferta.fechaFin == null)
                ModelState.AddModelError("FechaFin", "El campo  es obligatorio");
            if (oferta.descripcion == null || oferta.descripcion == "")
                ModelState.AddModelError("Descripcion", "El campo  es obligatorio");

        }
        public void ValidarOferta(Oferta oferta)
        {
            if (oferta.nombre == null || oferta.nombre == "")
                ModelState.AddModelError("Nombre", "El campo  es obligatorio");
            if (oferta.precioNormal.ToString() == null || oferta.precioNormal.ToString() == "")
                ModelState.AddModelError("PrecioN", "El campo  es obligatorio");
            if (oferta.precioOferta.ToString() == null|| oferta.precioOferta.ToString()=="")
                ModelState.AddModelError("PrecioO", "El campo  es obligatorio");
            if (oferta.descripcion == null || oferta.descripcion == "")
                ModelState.AddModelError("Descripcion", "El campo  es obligatorio");
            if (oferta.imagen == null || oferta.imagen == "")
                ModelState.AddModelError("Image", "Imagen necesaria");
        }


        public int calcularOferta(Oferta oferta)
        {
            decimal pNormal = oferta.precioNormal;
            decimal pOferta = oferta.precioOferta;
            decimal porOferta = (pOferta * 100) / pNormal;
            int porcenOferta = Convert.ToInt32(porOferta);
            int ofertaFinal = 100 - porcenOferta;
            return oferta.porcentajeOferta = ofertaFinal;
        }


    }
}
