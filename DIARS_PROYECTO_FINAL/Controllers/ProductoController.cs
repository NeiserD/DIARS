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
    public class ProductoController : Controller
    {
        public StoreContext context = new StoreContext();
        
        public ActionResult Index()
        {
            var productos = context.Productos.ToList();
            return View(productos);
        }

        public ActionResult Especificaciones(int ID)
        {
            Producto producto = context.Productos.Find(ID);

            return View(producto);
        }

        // GET: Producto/Create
       
        public ActionResult Crear()
        {
            ViewBag.Categoria = context.Categorias;
            return View(new Producto());
        }

        // POST: Producto/Create
    
        [HttpPost]
        public ActionResult Crear(Producto producto, HttpPostedFileBase file)
        {
            ViewBag.Categoria = context.Categorias;
            if (file != null && file.ContentLength > 0)
            {
                string ruta = Path.Combine(Server.MapPath("~/imagenes"), Path.GetFileName(file.FileName));
                file.SaveAs(ruta);
                producto.imagen = "/imagenes/" + Path.GetFileName(file.FileName);
            }

            if (ModelState.IsValid) {
                producto.fecha = DateTime.Now;
                producto.isActive = true;
                context.Productos.Add(producto);
                context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(producto);
        }

        // GET: Producto/Edit/5
      

        // POST: Producto/Edit/5
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
            Producto producto = context.Productos.Where(x => x.Id == ID).First();
            context.Productos.Remove(producto);
            context.SaveChanges();
            return RedirectToAction("Index");
        }

    }
}
