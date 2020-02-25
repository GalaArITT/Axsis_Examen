using Examen_Axsis.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace Examen_Axsis.Controllers
{

    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            IEnumerable<usuarios_axsis> llamada_sp;
            using (BD_CRUD_AxsisEntities db = new BD_CRUD_AxsisEntities())
            {
                llamada_sp = db.Database.SqlQuery<usuarios_axsis>("exec sp_mostrarDatos").ToList();
            }
            return View(llamada_sp);
        }
        [HttpGet]
        public ActionResult AltaUsuarios()
        {
            usuarios_axsis usuarios = new usuarios_axsis();
            return View(usuarios);
        }
        [HttpPost]
        public ActionResult AltaUsuarios(usuarios_axsis usuarios)
        {
            if (ModelState.IsValid)
            {
                using (BD_CRUD_AxsisEntities db = new BD_CRUD_AxsisEntities())
                {
                    if (!db.usuarios_axsis.Any(x=>x.usuario == usuarios.usuario))
                    {
                        usuarios.fecha_creacion = DateTime.Now;//insertar fecha inicial al llegar aqui
                        usuarios.estatus = true; //inicializarlo en true
                        usuarios.contrasena = Cifrado.ComputeHash(usuarios.contrasena, "SHA512", GetBytes("MyDemo"));
                        usuarios.contrasena_confirm = Cifrado.ComputeHash(usuarios.contrasena_confirm, "SHA512", GetBytes("MyDemo"));
                        db.usuarios_axsis.Add(usuarios); //agregar los datos al modelo
                        db.SaveChanges();
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        ViewBag.UsuarioExiste = "Este usuario ya existe";
                    }
                }
            }
            return View(usuarios);
        }
        [HttpGet]
        public ActionResult ModificarUsuario(int? id)
        {
            usuarios_axsis usuarios = null; //almacenar el objeto del usuario 
            using (BD_CRUD_AxsisEntities db = new BD_CRUD_AxsisEntities())
            {
                usuarios = db.usuarios_axsis.Find(id); //traer el id con Find 
                if (id==null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                if (usuarios == null)
                {
                    return HttpNotFound();
                }
            }
            return View(usuarios);
        }
        public ActionResult ModificarUsuario(usuarios_axsis usuarios)
        {
            if (ModelState.IsValid)
            {
                using (BD_CRUD_AxsisEntities db = new BD_CRUD_AxsisEntities())
                {
                    //si quito esta linea de codigo el estatusPermiso me lo pone como null y no se muestra en el index
                    usuarios.contrasena = Cifrado.ComputeHash(usuarios.contrasena, "SHA512", GetBytes("MyDemo"));
                    usuarios.contrasena_confirm = Cifrado.ComputeHash(usuarios.contrasena_confirm, "SHA512", GetBytes("MyDemo"));
                    db.Entry(usuarios).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            return View(usuarios);
        }

        [HttpGet]
        public ActionResult EliminarUsuario(usuarios_axsis usuarios, int id)
        {
            using (BD_CRUD_AxsisEntities db = new BD_CRUD_AxsisEntities())
            {
                //mandar llamar parametros
                SqlParameter _id = new SqlParameter("@id", id); //@id de nombre del sp
                //llamar al sp
                var sp_eliminar = db.Database.ExecuteSqlCommand("exec sp_inactivarUsuario @id", _id);
            }
            return RedirectToAction("Index");
        }
        private static byte[] GetBytes(string str)
        {
            byte[] bytes = new byte[str.Length * sizeof(char)];
            System.Buffer.BlockCopy(str.ToCharArray(), 0, bytes, 0, bytes.Length);
            return bytes;
        }
    }
}