using DIARS_PROYECTO_FINAL.BD;
using DIARS_PROYECTO_FINAL.Models;
using DIARS_PROYECTO_FINAL.Sources;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net.Mail;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace DIARS_PROYECTO_FINAL.Controllers
{
    public class UserController : Controller
    {
        String urlDomain = "http://localhost:49852/";
        public StoreContext context = StoreContext.getInstance();


        [Authorize]
        public ActionResult Index()
        {
            var usuuario = (Usuario)Session["Usuario"];
            if (usuuario.IdRol != 2)
            {
                var users = context.Usuarios.ToList();
                return View(users);
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
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
                var usuario_confirmado = context.Usuarios.Include(a=>a.ListaCarritos).Where(a => a.username.Equals(objUser.username) && a.password.Equals(contrasenna_hasheado)).FirstOrDefault();


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
            try
            {
                validarUsuarios(usuario);
                if (ModelState.IsValid)
                {
                    usuario.IdRol = 1;
                    usuario.password = Hash.ComputeSha256Hash(usuario.password);
                    context.Usuarios.Add(usuario);
                    context.SaveChanges();
                    var oUser = context.Usuarios.Where(d => d.email == usuario.email).FirstOrDefault();
                    var cUser = context.Usuarios.Where(d => d.username == usuario.username).FirstOrDefault();
                    //enviar correo 
                    BienvenidoEmail(oUser.email,cUser.username);
                    return RedirectToAction("Login");
                }
            }
            catch (Exception)
            {
                return View(usuario);
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
        public String  VerifiPassword(string pass)
        {
            var usuario = (Usuario)Session["Usuario"];
            return (usuario.password == Hash.ComputeSha256Hash(pass))?"ok":"error";

        }
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

        [Authorize]
        public String addCarrito(int id,int mcantidad) {
            try {
                var usuario = (Usuario)Session["Usuario"];
                context.Carritos.Add(new Carrito { IdUsuario = usuario.Id, cantidad = mcantidad, IdProducto = id });
                context.SaveChanges();
                return "ok";
            } catch (Exception e) { return "error"; }
            

        }
        [Authorize]
        public ActionResult DeleteCarrito(int id)
        {

            var valor = context.Carritos.Where(a => a.Id == id).FirstOrDefault();
            context.Carritos.Remove(valor);
            context.SaveChanges();

            return RedirectToAction("CarritoCompras");
        }

        [Authorize]
        public ActionResult CarritoCompras() {
            var usuario = (Usuario)Session["Usuario"];
            return View(context.Carritos.Include(a=>a.Producto).Where(a=>a.IdUsuario ==usuario.Id));
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
        public void validarUsuarios(Usuario usuario)
        {
            if (usuario.email == null || usuario.email == "")
            {
                ModelState.AddModelError("Email1", "El campo Email es requerido");
            }
            if (usuario.username == null || usuario.username== "")
            {
                ModelState.AddModelError("UserName1", "El campo Nombre de Usuario es requerido");
            }
            if (usuario.dni == null || usuario.dni== "")
            {
                ModelState.AddModelError("DNI", "El campo DNI es requerido");
            }
            if (usuario.password == null || usuario.password == "")
            {
                ModelState.AddModelError("Password1", "El campo Password es requerido");
            }
            if (usuario.nombres == null || usuario.nombres == "")
            {
                ModelState.AddModelError("Nombre1", "Este campo es requerido");
            }
            if (!validarLetras(usuario.nombres)) { 
               ModelState.AddModelError("Nombre2", "Solo ingrese caracteres alfabeticos");
            }
            if (!validarnUMEROS(usuario.dni))
            {
                ModelState.AddModelError("NumeroDocumento", "Solo Ingrese numeros");
            }
          
           
        }


        //Metodos de validación
        public bool validarLetras(string numString)
        {

            string parte = numString.Trim();
            int count = parte.Count(s => s == ' ');
            if (parte == "")
            {
                return false;
            }
            else if (count > 1)
            {
                return false;
            }
            char[] charArr = parte.ToCharArray();
            foreach (char cd in charArr)
            {
                if (!char.IsLetter(cd) && !char.IsSeparator(cd))
                    return false;
            }
            return true;
        }
        public bool validarnUMEROS(string numString)
        {
            char[] charArr = numString.ToCharArray();
            foreach (char cd in charArr)
            {
                if (!char.IsNumber(cd))
                    return false;
            }
            return true;
        }
        ////*********************************RECUPERAR CONTRASEÑA***********************///////////////////
        [HttpGet]
        public ActionResult startRecovery()
        {
            Recovery model = new Recovery();
            return View(model);
        }

        [HttpPost]
        public ActionResult startRecovery(Recovery model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            //creamos un token haciendo uso de una propiedad de entity 
            string token = GetSHA256(Guid.NewGuid().ToString());
            var oUser = context.Usuarios.Where(d => d.email == model.Email).FirstOrDefault();
            if (oUser != null)
            {
                oUser.TokenRecovery = token;
                context.Entry(oUser).State = EntityState.Modified;
                context.SaveChanges();

                //enviar correo 
                SendEmail(oUser.email, token);

            }

            return View();
        }

        [HttpGet]
        public ActionResult Recovery(string token)
        {
            RecoveryPassword model = new RecoveryPassword();
            model.token = token;
            if (model.token == null || model.token.Trim().Equals(""))
            {

                return View();
            }
            var oUser = context.Usuarios.Where(d => d.TokenRecovery == model.token).FirstOrDefault();
            if (oUser == null)
            {
                ViewBag.Error = "Token expirado";
                return View();
            }

            model.token = token;
            return View(model);
        }

        [HttpPost]
        public ActionResult Recovery(RecoveryPassword model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(model);
                }
                var oUser = context.Usuarios.Where(d => d.TokenRecovery == model.token).FirstOrDefault();
                if (oUser != null)
                {
                    oUser.password = model.Password;
                    oUser.TokenRecovery = null;
                    context.Entry(oUser).State = EntityState.Modified;
                    context.SaveChanges();
                }
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }

            ViewBag.Message = "Contraseña modificada con exito";
            return View();
        }

        //para encriptar el token 
        public static string GetSHA256(string str)
        {
          /*  SHA256 sha256 = SHA256Managed.Create();
            ASCIIEncoding encoding = new ASCIIEncoding();
            byte[] stream = null;
            StringBuilder sb = new StringBuilder();
            stream = sha256.ComputeHash(encoding.GetBytes(str));
            for (int i = 0; i < stream.Length; i++) sb.AppendFormat("{0:x2}", stream[i]);*/

            return Hash.ComputeSha256Hash(str);
        }

        //enviar correros 

        private void SendEmail(string EmailDestino, string token)
        {
            string EmailOrigen = "envioprueba3@gmail.com";
            string Contraseña = "@dyars123";
            string Url = urlDomain + "/User/Recovery/?token=" + token;
            MailMessage oMailMessage = new MailMessage(EmailOrigen, EmailDestino, "Recuperacion de Contraseña",
                "<p>Correo recuperacion de contraseña </p><br>" +
                "<a href='" + Url + "'>Click para recuperar </a>");

            oMailMessage.IsBodyHtml = true;
            SmtpClient oSmtpClient = new SmtpClient("smtp.gmail.com");
            oSmtpClient.EnableSsl = true;
            oSmtpClient.UseDefaultCredentials = false;

            oSmtpClient.Port = 587;
            oSmtpClient.Credentials = new System.Net.NetworkCredential(EmailOrigen, Contraseña);
            oSmtpClient.Send(oMailMessage);
            oSmtpClient.Dispose();
        }


        //*****************************BIENVENIDO****************************************************************************

        private void BienvenidoEmail(string EmailDestino,string user)
        {
            string EmailOrigen = "envioprueba3@gmail.com";
            string Contraseña = "@dyars123";
            
            MailMessage oMailMessage = new MailMessage(EmailOrigen, EmailDestino, "Bienvenido a PolonyStore",
                "<p>¡BIENVENIDO "+ user + " A POLONYSTORE!</p><br>");

            oMailMessage.IsBodyHtml = true;
            SmtpClient oSmtpClient = new SmtpClient("smtp.gmail.com");
            oSmtpClient.EnableSsl = true;
            oSmtpClient.UseDefaultCredentials = false;

            oSmtpClient.Port = 587;
            oSmtpClient.Credentials = new System.Net.NetworkCredential(EmailOrigen, Contraseña);
            oSmtpClient.Send(oMailMessage);
            oSmtpClient.Dispose();
        }

    }
}