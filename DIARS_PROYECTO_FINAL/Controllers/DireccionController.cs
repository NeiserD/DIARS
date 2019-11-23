using DIARS_PROYECTO_FINAL.BD;
using DIARS_PROYECTO_FINAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DIARS_PROYECTO_FINAL.Controllers
{
    public class DireccionController : Controller
    {
        StoreContext context =  StoreContext.getInstance();
        public ActionResult Index()
        {
            var direcciones = context.Direccioness.ToList();    
            return View(direcciones);
        }
        
        
        public ActionResult Crear()
        {
            return View(new Direcciones ());
        }
        
        [HttpPost]
        public ActionResult Crear(Direcciones direcciones)
        {
            if (ModelState.IsValid)
            {
                context.Direccioness.Add(direcciones);
                context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View();
        }

        // GET: Direccion/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Direccion/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
        
        public ActionResult Eliminar(int ID)
        {
            Direcciones dir= context.Direccioness.Where(x => x.Id == ID).First();
            context.Direccioness.Remove(dir);
            context.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
