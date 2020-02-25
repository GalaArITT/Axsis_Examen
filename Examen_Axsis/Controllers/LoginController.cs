using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Examen_Axsis.Models;

namespace Examen_Axsis.Controllers
{
    public class LoginController : Controller
    {
        private BD_CRUD_AxsisEntities db = new BD_CRUD_AxsisEntities();

        // GET: Login
        public ActionResult Index(usuarios_axsis usuarios)
        {
            return View();
        }
        [HttpPost]
        public ActionResult AutorizarIngreso(usuarios_axsis usuarios)
        {
            //hacer consulta para obtener UN usuario
            var usuarioLogin = db.usuarios_axsis.Where(s => s.usuario == usuarios.usuario
            && s.contrasena == usuarios.contrasena && s.estatus == true).FirstOrDefault();
            //si llega a mostrar 
            if (usuarioLogin != null)
            {
                Session["id"] = usuarios.id;
                Session["usuario"] = usuarios.usuario;
                int idUsuario = Convert.ToInt32(Session["id"]);
                FormsAuthentication.SetAuthCookie(idUsuario.ToString(), false);
                RedirectToAction("Index", "Home");

            }
            //si no obtiene datos
            else
            {
                ViewBag.Message = "Datos incorrectos, verifique infiormacion";
                return View("Index", usuarios);
            }
            //si la sesion es detectada 
            if (Session["id"] != null)
            {
                Response.Redirect("~/Home/Index");
            }
            return null; 
        }
        public ActionResult CerrarSesion()
        {
            Session.Abandon();
            return RedirectToAction("Index","Login");
        }


        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
