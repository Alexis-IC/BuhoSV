using Buho.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Web;
using System.Web.Mvc;

namespace Buho.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Login()
        {
            return View();
        }
        
        [HttpPost]
        public ActionResult Login(UsuarioCLS oUsuarioCLS)
        {
            using (BuhoDBEntities db = new BuhoDBEntities())
            {
                Usuarios objUsuario = (from usuario in db.Usuarios
                            where usuario.Usuario == oUsuarioCLS.usuario
                            && usuario.Clave == oUsuarioCLS.clave
                            select usuario).FirstOrDefault();

                if(objUsuario == null)
                {
                    ViewBag.Error = "Usuario y/o clave incorrectos";
                    return View();
                }
                else
                {   
                    Session["Usuario"] = objUsuario;
                }
            }

            return RedirectToAction("Index","Home");
        }

        public ActionResult Registro()
        {
            return View();
        }
        
        [HttpPost]
        public ActionResult Registro(UsuarioCLS oUsuarioCLS)
        {
            if(!ModelState.IsValid)
            {
                return View(oUsuarioCLS);
            }
            else
            {
                if(ExisteCorreo(oUsuarioCLS.email))
                {
                    ViewBag.Error = "Ya existe un usuario con ese correo.";
                    return View(oUsuarioCLS);
                }
                else if(ExisteDui(oUsuarioCLS.dui))
                {
                    ViewBag.Error = "Ya existe un usuario con ese DUI.";
                    return View(oUsuarioCLS);
                }
                else if(ExisteUsuario(oUsuarioCLS.usuario))
                {
                    ViewBag.Error = "Ya existe un registro con ese nombre de usuario.";
                    return View(oUsuarioCLS);
                }
                else
                {
                    using (BuhoDBEntities db = new BuhoDBEntities())
                    {
                        Usuarios oUsuario = new Usuarios();
                        oUsuario.Nombre = oUsuarioCLS.nombre;
                        oUsuario.Apellido = oUsuarioCLS.apellido;
                        oUsuario.Direccion = oUsuarioCLS.direccion;
                        oUsuario.DUI = oUsuarioCLS.dui;
                        oUsuario.Email = oUsuarioCLS.email;
                        oUsuario.Usuario = oUsuarioCLS.usuario;
                        oUsuario.Clave = oUsuarioCLS.clave;
                        db.Usuarios.Add(oUsuario);
                        db.SaveChanges();
                    }
                }
            }
            
            return RedirectToAction("Index","Home");
        }

        private Boolean ExisteCorreo(string correo)
        {
            using (BuhoDBEntities db = new BuhoDBEntities())
            {
                int nRegistros = db.Usuarios.Where(p => p.Email.Equals(correo)).Count();
                return nRegistros > 0;
            }
        }

        private Boolean ExisteDui(string dui)
        {
            using (BuhoDBEntities db = new BuhoDBEntities())
            {
                int nRegistros = db.Usuarios.Where(p => p.DUI.Equals(dui)).Count();
                return nRegistros > 0;
            }
        }

        private Boolean ExisteUsuario(string usuario)
        {
            using (BuhoDBEntities db = new BuhoDBEntities())
            {
                int nRegistros = db.Usuarios.Where(p => p.Nombre.Equals(usuario)).Count();
                return nRegistros > 0;
            }
        }
    }
}