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
      //  [Authorize]
        public ActionResult Index()
        {
            var usuuario = (Usuario)Session["Usuario"];
            try
            {
                if (usuuario.IdRol != 2 && usuuario.IdRol != null )
                {
                    var categorias = context.Categorias.ToList();
                    return View(categorias);
                }
                else
                {
                    return Redirect("~/");
                }
            }
            catch (Exception)
            {
                return Redirect("~/");
            }
        }
        
        //[Authorize]
        public ActionResult Crear()
        {
            var usuuario = (Usuario)Session["Usuario"];
            try
            {
                if (usuuario.IdRol != 2 && usuuario.IdRol != null)
                {
                    return View(new Categoria());
                }
                else
                {
                    return Redirect("~/");
                }
            }
            catch (Exception)
            {
                return Redirect("~/");
            }
        }

        //[Authorize]
        [HttpPost]
        public ActionResult Crear(Categoria categoria)
        {
            var usuuario = (Usuario)Session["Usuario"];
            try
            {
                if (usuuario.IdRol != 2 && usuuario.IdRol != null)
                {
                    ValidarCate(categoria);
                    if (ModelState.IsValid)
                    {
                        context.Categorias.Add(categoria);
                        context.SaveChanges();

                        return RedirectToAction("Index");
                    }
                    else {
                        return View(categoria);
                    }
                }
                return Redirect("~/");
            }
            catch (Exception)
            {
                return Redirect("~/");
            }
        }

        //[Authorize]
        [HttpGet]
        public ViewResult Editar(int ID)
        {
            var categoria = context.Categorias.Where(x => x.id == ID).First();
            return View(categoria);
        }

        //[Authorize]
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
        //[Authorize]
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
