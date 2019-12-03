using DIARS_PROYECTO_FINAL.BD;
using DIARS_PROYECTO_FINAL.Models;
using System;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;

namespace DIARS_PROYECTO_FINAL.Controllers
{
    public class CategoriaController : Controller
    {

        public StoreContext context = StoreContext.getInstance();
        [Authorize]
        public ViewResult Index()
        {
            var categorias = context.Categorias.ToList();
            return View(categorias);
        }




        [Authorize]
        public ViewResult Crear()
        {
            return View(new Categoria());
        }

        [Authorize]
        [HttpPost]
        public ActionResult Crear(Categoria categoria)
        {
            ValidarCate(categoria);
            if (ModelState.IsValid)
            {
                context.Categorias.Add(categoria);
                context.SaveChanges();

                return RedirectToAction("Index");
            }
            return View(categoria);
        }

        [Authorize]
        [HttpGet]
        public ViewResult Editar(int ID)
        {
            var categoria = context.Categorias.Where(x => x.id == ID).First();
            return View(categoria);
        }

        [Authorize]
        [HttpPost]
        public ActionResult Editar(Categoria categoria)
        {
            ValidarCate(categoria);
            if (ModelState.IsValid)
            {
                context.Entry(categoria).State = EntityState.Modified;
                context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(categoria);

        }
        [Authorize]
        [HttpGet]
        public ActionResult Eliminar(int ID)
        {
            Categoria categoria = context.Categorias.Where(x => x.id == ID).First();
            context.Categorias.Remove(categoria);
            context.SaveChanges();
            return RedirectToAction("Index");
        }

        public void ValidarCate(Categoria categoria)
        {
            if (categoria.nombre == null || categoria.nombre == "")
                ModelState.AddModelError("Nombre", "El campo  es obligatorio");
            if (categoria.descripcion == null || categoria.descripcion == "")
                ModelState.AddModelError("Descripcion", "El campo  es obligatorio");
        }
    }
}
