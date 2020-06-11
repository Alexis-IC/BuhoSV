using Buho.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Buho.Controllers
{
    public class ProveedorController : Controller
    {
        // GET: Proveedor

        public ActionResult Index()
        {
            List<ProveedorCLS> listaProveedores = null;

            using (BuhoDBEntities db = new BuhoDBEntities())
            {
                listaProveedores = (from proveedor in db.Proveedores
                                 select new ProveedorCLS
                                 {
                                     idProveedor = proveedor.ID_Proveedor,
                                     nombre = proveedor.Nombre,
                                     apellido = proveedor.Apellido,
                                     direccion = proveedor.Direccion,
                                     dui = proveedor.DUI,
                                     email = proveedor.Email,
                                     razonSocial = proveedor.RazonSocial
                                 }).ToList();
            }

            return View(listaProveedores);
        }
        [HttpPost]
        public ActionResult Eliminar(int idProveedor)
        {
            using (BuhoDBEntities db = new BuhoDBEntities())
            {
                Proveedores oProveedor = db.Proveedores.Where(p => p.ID_Proveedor.Equals(idProveedor)).First();
                db.Proveedores.Remove(oProveedor);
                db.SaveChanges();
            }
            
            return RedirectToAction("Index");
        }

        public ActionResult Editar(int id)
        {
            ProveedorCLS oProveedorCLS = new ProveedorCLS();
            using (BuhoDBEntities db = new BuhoDBEntities())
            {
                Proveedores oProveedor = db.Proveedores.Where(p=>p.ID_Proveedor.Equals(id)).First();

                oProveedorCLS.idProveedor = id;
                oProveedorCLS.nombre = oProveedor.Nombre;
                oProveedorCLS.apellido = oProveedor.Apellido;
                oProveedorCLS.dui = oProveedor.DUI;
                oProveedorCLS.direccion = oProveedor.Direccion;
                oProveedorCLS.email = oProveedor.Email;
                oProveedorCLS.razonSocial = oProveedor.RazonSocial;
            }
            
            return View(oProveedorCLS);
        }

        [HttpPost]
        public ActionResult Editar(ProveedorCLS oProveedorCLS)
        {
            if(!ModelState.IsValid)
            {
                return View(oProveedorCLS);
            }
            
            else if (ExisteCorreo(oProveedorCLS.email,oProveedorCLS.idProveedor))
            {
                ViewBag.Error = "Ya existe un proveedor con ese correo.";
                return View(oProveedorCLS);
            }

            else if (ExisteDui(oProveedorCLS.dui,oProveedorCLS.idProveedor))
            {
                ViewBag.Error = "Ya existe un proveedor con ese DUI.";
                return View(oProveedorCLS);
            }
            using (BuhoDBEntities db = new BuhoDBEntities())
            {
                Proveedores oProveedor = db.Proveedores.Where(p => p.ID_Proveedor.Equals(oProveedorCLS.idProveedor)).First();

                oProveedor.Nombre = oProveedorCLS.nombre;
                oProveedor.Apellido = oProveedorCLS.apellido;
                oProveedor.DUI = oProveedorCLS.dui;
                oProveedor.Direccion = oProveedorCLS.direccion;
                oProveedor.Email = oProveedorCLS.email;
                oProveedor.RazonSocial = oProveedorCLS.razonSocial;

                db.SaveChanges();
            }

            return RedirectToAction("Index");
        }

        public ActionResult Agregar()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Agregar(ProveedorCLS oProveedorCLS)
        {
            if (!ModelState.IsValid)
            {
                return View(oProveedorCLS);
            }
            else
            {
                if (ExisteCorreo(oProveedorCLS.email))
                {
                    ViewBag.Error = "Ya existe un proveedor con ese correo.";
                    return View(oProveedorCLS);
                }
                else if (ExisteDui(oProveedorCLS.dui))
                {
                    ViewBag.Error = "Ya existe un proveedor con ese DUI.";
                    return View(oProveedorCLS);
                }
                else
                {
                    using (BuhoDBEntities db = new BuhoDBEntities())
                    {
                        Proveedores oProveedor = new Proveedores();
                        oProveedor.Nombre = oProveedorCLS.nombre;
                        oProveedor.Apellido = oProveedorCLS.apellido;
                        oProveedor.DUI = oProveedorCLS.dui;
                        oProveedor.Direccion = oProveedorCLS.direccion;
                        oProveedor.Email = oProveedorCLS.email;
                        oProveedor.RazonSocial = oProveedorCLS.razonSocial;

                        db.Proveedores.Add(oProveedor);
                        db.SaveChanges();
                    }
                }
            }

            return RedirectToAction("Index");
        }

        private Boolean ExisteCorreo(string correo)
        {
            using (BuhoDBEntities db = new BuhoDBEntities())
            {
                int nRegistros = db.Proveedores.Where(p=>p.Email.Equals(correo)).Count();
                return nRegistros>0;
            }
        }

        private Boolean ExisteDui(string dui)
        {
            using (BuhoDBEntities db = new BuhoDBEntities())
            {
                int nRegistros = db.Proveedores.Where(p => p.DUI.Equals(dui)).Count();
                return nRegistros > 0;
            }
        }

        private Boolean ExisteCorreo(string correo,int id)
        {
            using (BuhoDBEntities db = new BuhoDBEntities())
            {
                int nRegistros = db.Proveedores.Where(p => p.Email.Equals(correo) && !p.ID_Proveedor.Equals(id)).Count();
                return nRegistros > 0;
            }
        }

        private Boolean ExisteDui(string dui,int id)
        {
            using (BuhoDBEntities db = new BuhoDBEntities())
            {
                int nRegistros = db.Proveedores.Where(p => p.DUI.Equals(dui) && !p.ID_Proveedor.Equals(id)).Count();
                return nRegistros > 0;
            }
        }
    }
}