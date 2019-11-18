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
    public class UserController : Controller
    {
        public StoreContext context = new StoreContext();
        public ActionResult Index()
        {
            
                var users = context.Usuarios.ToList();
                return View(users);
        }

        [HttpGet]
        public ActionResult ProfileDetails(int ID)
        {
            var users = context.Usuarios.Where(a => a.Id == ID).FirstOrDefault();
            ViewBag.Id = users.Id;
            return View(users);
            
        }

        [HttpPost]
        public ActionResult ProfileDetails(Usuario usuario)
        {
            if (ModelState.IsValid)
            {
                context.Entry(usuario).State = EntityState.Modified;
                context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(usuario);
        }

        

        [HttpGet]
        public ActionResult Eliminar(int ID)
        {
            using (StoreContext context = new StoreContext())
            {
                Usuario usuario = context.Usuarios.Where(x => x.Id == ID).First();
                context.Usuarios.Remove(usuario);
                context.SaveChanges();
                return RedirectToAction("Index");
            }
        }

        [HttpGet]
        public ActionResult Login()
        {
            return View(new Usuario());
        }

        [HttpPost]
        public ActionResult Login(Usuario objUser)
        {
            validar(objUser);
            if (ModelState.IsValid)
            {
                using (StoreContext db = new StoreContext())
                {
                    var obj = db.Usuarios.Where(a => a.username.Equals(objUser.username) && a.password.Equals(objUser.password)).FirstOrDefault();
                    if (obj != null)
                    {
                        Session["Id"] = obj.Id.ToString();
                        Session["username"] = obj.nombres.ToString();
                        return RedirectToAction("Index","Home");
                    }
                }
            }
            validarambos(objUser);
            return View(objUser);
        }
        
        public ActionResult Salir()
        {
            Session.Abandon();
            Response.Cookies.Add(new HttpCookie("ASP.NET_SessionId", ""));
            return RedirectToAction("", "Home");
            //ModelState.AddModelError("", "Sesión Cerrada");
        }
        [HttpGet]
        public ViewResult Registrar()
        {

            return View(new Usuario());
        }
        [HttpPost]
        public ActionResult Registrar(Usuario usuario)
        {
            validarUsuarios(usuario);
            if (ModelState.IsValid)
            {
                using (StoreContext context = new StoreContext())
                {
                    usuario.IdRol = 2;
                    context.Usuarios.Add(usuario);
                    context.SaveChanges();
                    return RedirectToAction("Login");
                }
            }
            return View(usuario);
        }

        public void validar(Usuario objUser)
        {
            if (objUser.username == null || objUser.username == " ")
            {
                ModelState.AddModelError("username", "Ingrese su nombre de usuario");
            }

            if (objUser.password == null || objUser.password == " ")
            {
                ModelState.AddModelError("password", "Ingrese su contraseña");
            }
        }
        public void validarambos(Usuario objUser)
        {
            if (objUser.username != null && objUser.username != "" || objUser.password != null && objUser.password != "")
            {
                ModelState.AddModelError("invalid", "Usuario y/o Contraseña incorrecta");
            }
        }

        public void validarUsuarios(Usuario usuario)
        {
            if (usuario.nombres == null || usuario.nombres == "")
            {
                ModelState.AddModelError("Nombre1", "Este campo es requerido");
            }
            if (usuario.apellidos == null || usuario.apellidos == "")
            {
                ModelState.AddModelError("Apellido1", "El campo Apellidos es requerido");
            }
            if (usuario.dni == null || usuario.dni == "")
            {
                ModelState.AddModelError("DNI", "El campo DNI es requerido");
            }
            //if (usuario.celular == null || usuario.celular == "")
            //{
            //    ModelState.AddModelError("Celular", "El campo Celular es requerido");
            //}
            if (usuario.email == null || usuario.email == "")
            {
                ModelState.AddModelError("Email1", "El campo Email es requerido");
            }
            if (usuario.email == null || usuario.email == "")
            {
                ModelState.AddModelError("UserName1", "El campo Nombre de Usuario es requerido");
            }
            if (usuario.password == null || usuario.password == "")
            {
                ModelState.AddModelError("Password1", "El campo Password es requerido");
            }
        }
    }
}