using DIARS_PROYECTO_FINAL.BD;
using DIARS_PROYECTO_FINAL.Models;
using DIARS_PROYECTO_FINAL.Sources;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace DIARS_PROYECTO_FINAL.Controllers
{
    public class UserController : Controller
    {
        public StoreContext context = StoreContext.getInstance();


        [Authorize]
        public ActionResult Index()
        {
            var users = context.Usuarios.ToList();
            return View(users);
        }

        [Authorize]
        [HttpGet]
        public ActionResult ProfileDetails()
        {

            if (Session["Usuario"] == null) return RedirectToAction("Index");

            var usuario_loggeado = (Usuario)Session["Usuario"];    
            var users = context.Usuarios.Where(a => a.Id == usuario_loggeado.Id).FirstOrDefault();
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
            Usuario usuario = context.Usuarios.Where(x => x.Id == ID).First();
            context.Usuarios.Remove(usuario);
            context.SaveChanges();
            return RedirectToAction("Index");
            
        }

        [HttpGet]
        public ActionResult Login()
        {
            if (Session["Usuario"] != null) return View ("Producto", "Index");
            return View(new Usuario());
        }

        [HttpPost]
        public ActionResult Login(Usuario objUser)
        {

            if (Session["Usuario"] != null) return View("Producto", "Index");
            validar(objUser);
            if (ModelState.IsValid)
            {
                var contrasenna_hasheado = Hash.ComputeSha256Hash( objUser.password);
                var usuario_confirmado = context.Usuarios.Where(a => a.username.Equals(objUser.username) && a.password.Equals(contrasenna_hasheado)).FirstOrDefault();


                if (usuario_confirmado != null)
                {
                    //cambié las dos sesiones por una sola 
                    Session["Usuario"] = usuario_confirmado;
                    FormsAuthentication.SetAuthCookie(usuario_confirmado.username, false);
                    return RedirectToAction("Index","Home");
                    
                }
            }
            validarambos(objUser);
            return View(objUser);
        }
        [Authorize]
        public ActionResult Salir()
        {
            Session.Abandon();
            FormsAuthentication.SignOut();
            Session.Clear();
            return RedirectToAction("", "Home");
         
        }
        [HttpGet]
        public ViewResult Registrar()
        {

            return View(new Usuario());
        }
        [HttpPost]
        public ActionResult Registrar(Usuario usuario)
        {
            validarCampos(usuario);
            validar_campos_unicosbd(usuario);

            if (ModelState.IsValid)
            {
            
                usuario.IdRol = 2;
                usuario.password = Hash.ComputeSha256Hash(usuario.password);
                context.Usuarios.Add(usuario);
                context.SaveChanges();
                return RedirectToAction("Login");
                
            }
            return View(usuario);
        }

        [HttpPost]
        [Authorize]
        public string contrasennaPasword(string datos )
        {
            try
            {
                var usuario = (Usuario)Session["Usuario"];
                var IsUser = context.Usuarios.Where(a => a.Id == usuario.Id).FirstOrDefault();
                IsUser.password = Hash.ComputeSha256Hash(datos);
                context.SaveChanges();
                return "ok";
            }
            catch
            {
                return "error";
            }

        }

        [Authorize]
        public PartialViewResult changePassword() {

            return PartialView();
        }
        [Authorize]
        [HttpGet]
        public String  VerifiPassword(string pass)
        {
            var usuario = (Usuario)Session["Usuario"];
            return (usuario.password == Hash.ComputeSha256Hash(pass))?"ok":"error";

        }
      

        [HttpGet]
        [Authorize]
        public PartialViewResult EditProfile() {
            var usuario = (Usuario)Session["Usuario"];
            return PartialView(context.Usuarios.Where(a=>a.Id == usuario.Id ).FirstOrDefault());
        }
       
        [Authorize]
        public String EditarCelular(String nombres)
        {
            try
            {
                var usuario = (Usuario)Session["Usuario"];
                var usuarioDb = (context.Usuarios.Where(a => a.Id == usuario.Id).FirstOrDefault());
                usuarioDb.celular = nombres;
                context.SaveChanges();
                return "ok";
            }
            catch (Exception e) {
                return e.ToString();
            }
        }

        [Authorize]
        public String EditarNombre(String nombre)
        {
            try
            {
                var usuario = (Usuario)Session["Usuario"];
                var usuarioDb = (context.Usuarios.Where(a => a.Id == usuario.Id).FirstOrDefault());
                usuarioDb.nombres = nombre;
                context.SaveChanges();
                return "ok";
            }
            catch (Exception e)
            {
                return e.ToString();
            }
        }


        //valida que locs campos sean unicos 
        public void validar_campos_unicosbd(Usuario usuario) {
         
            //valida username 
            var Isusername = context.Usuarios.Where(a => a.username == usuario.username).FirstOrDefault();
            if (Isusername != null) ModelState.AddModelError("validation_de_username", "El nombre ingresado ya está en uso");

            //valida email 
            var IsMail = context.Usuarios.Where(a => a.email == usuario.email).FirstOrDefault();
            if (IsMail != null) ModelState.AddModelError("validation_de_mail", "El correo ingresado ya está en uso");

            //valida dni
            var IsDni = context.Usuarios.Where(a => a.dni == usuario.dni).FirstOrDefault();
            if (IsDni != null) ModelState.AddModelError("validation_de_dni", "El DNI ingresado ya está en uso");

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
        public void validarCampos(Usuario usuario)
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