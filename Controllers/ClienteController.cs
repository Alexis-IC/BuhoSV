using Buho.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Buho.Controllers
{
    public class ClienteController : Controller
    {
        // GET: Cliente

        public ActionResult Index()
        {
            if(TempData["Error"]!=null)
            {
                ViewBag.TituloError = "No se pudo eliminar cliente";
                ViewBag.Error = "Existen registros de facturas a nombre de este cliente, deberá borrar primero esta información para evitar problemas mayores.";
            }

            List<ClienteCLS> listaClientes = null;
            
            using (BuhoDBEntities db = new BuhoDBEntities())
            {
                listaClientes = (from cliente in db.Clientes
                                 select new ClienteCLS
                                 {
                                     idCliente = cliente.ID_Cliente,
                                     nombre = cliente.Nombre,
                                     apellido = cliente.Apellido,
                                     direccion = cliente.Direccion,
                                     dui = cliente.DUI,
                                     email = cliente.Email,
                                     razonSocial = cliente.RazonSocial
                                 }).ToList();
            }

            return View(listaClientes);
        }

        public ActionResult Eliminar(int id)
        {
            int nRegistros = 0;

            using (BuhoDBEntities db = new BuhoDBEntities())
            {
                nRegistros = db.Ventas.Where(p => p.ID_Cliente.Value.Equals(id)).Count();
            }

            if (nRegistros > 0)
            {
                TempData["Error"] = "Existe";
                return RedirectToAction("Index");
            }

            else
            {
                using (BuhoDBEntities db = new BuhoDBEntities())
                {
                    Clientes oCliente = db.Clientes.Find(id);
                    db.Clientes.Remove(oCliente);
                    db.SaveChanges();
                }
            }

            return RedirectToAction("Index");
        }

        public ActionResult Editar(int id)
        {
            ClienteCLS oClienteCLS = new ClienteCLS();

            using (BuhoDBEntities db = new BuhoDBEntities())
            {
                Clientes oCliente = db.Clientes.Where(p => p.ID_Cliente.Equals(id)).First();

                oClienteCLS.idCliente = id;
                oClienteCLS.nombre = oCliente.Nombre;
                oClienteCLS.apellido = oCliente.Apellido;
                oClienteCLS.dui = oCliente.DUI;
                oClienteCLS.direccion = oCliente.Direccion;
                oClienteCLS.email = oCliente.Email;
                oClienteCLS.razonSocial = oCliente.RazonSocial;
            }

            return View(oClienteCLS);
        }
        [HttpPost]
        public ActionResult Editar(ClienteCLS oClienteCLS)
        {
            if(!ModelState.IsValid)
            {
                return View(oClienteCLS);
            }

            else if (ExisteCorreo(oClienteCLS.email,oClienteCLS.idCliente))
            {
                ViewBag.TituloError = "Registro fallido";
                ViewBag.Error = "Ya existe un cliente con ese correo, por favor revísa tu información.";
                return View(oClienteCLS);
            }
            else if (ExisteDui(oClienteCLS.dui,oClienteCLS.idCliente))
            {
                ViewBag.TituloError = "Registro fallido";
                ViewBag.Error = "Ya existe un cliente con ese número de DUI, por favor revísa tu información.";
                return View(oClienteCLS);
            }

            using (BuhoDBEntities db = new BuhoDBEntities())
            {
                Clientes oCliente = db.Clientes.Where(p => p.ID_Cliente.Equals(oClienteCLS.idCliente)).First();                
                
                oCliente.Nombre = oClienteCLS.nombre;
                oCliente.Apellido = oClienteCLS.apellido;
                oCliente.DUI = oClienteCLS.dui;
                oCliente.Direccion = oClienteCLS.direccion;
                oCliente.Email = oClienteCLS.email;
                oCliente.RazonSocial = oClienteCLS.razonSocial;

                db.SaveChanges();
            }

            return RedirectToAction("Index");
        }

        public ActionResult Agregar()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Agregar(ClienteCLS oClienteCLS)
        {
            if (!ModelState.IsValid)
            {
                return View(oClienteCLS);
            }
            else
            {
                if (ExisteCorreo(oClienteCLS.email))
                {
                    ViewBag.TituloError = "Registro fallido";
                    ViewBag.Error = "Ya existe un cliente con ese correo, por favor revísa tu información.";
                    return View(oClienteCLS);
                }
                else if (ExisteDui(oClienteCLS.dui))
                {
                    ViewBag.TituloError = "Registro fallido";
                    ViewBag.Error = "Ya existe un cliente con ese número de DUI, por favor revísa tu información.";
                    return View(oClienteCLS);
                }
                else
                {
                    using (BuhoDBEntities db = new BuhoDBEntities())
                    {
                        Clientes oCliente = new Clientes();
                        oCliente.Nombre = oClienteCLS.nombre;
                        oCliente.Apellido = oClienteCLS.apellido;
                        oCliente.DUI = oClienteCLS.dui;
                        oCliente.Direccion = oClienteCLS.direccion;
                        oCliente.Email = oClienteCLS.email;
                        oCliente.RazonSocial = oClienteCLS.razonSocial;
                        
                        db.Clientes.Add(oCliente);
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
                int nRegistros = db.Clientes.Where(p => p.Email.Equals(correo)).Count();
                return nRegistros > 0;
            }
        }

        private Boolean ExisteDui(string dui)
        {
            using (BuhoDBEntities db = new BuhoDBEntities())
            {
                int nRegistros = db.Clientes.Where(p => p.DUI.Equals(dui)).Count();
                return nRegistros > 0;
            }
        }

        private Boolean ExisteCorreo(string correo,int id)
        {
            using (BuhoDBEntities db = new BuhoDBEntities())
            {
                int nRegistros = db.Clientes.Where(p => p.Email.Equals(correo) && !p.ID_Cliente.Equals(id)).Count();
                return nRegistros > 0;
            }
        }

        private Boolean ExisteDui(string dui,int id)
        {
            using (BuhoDBEntities db = new BuhoDBEntities())
            {
                int nRegistros = db.Clientes.Where(p => p.DUI.Equals(dui) && !p.ID_Cliente.Equals(id)).Count();
                return nRegistros > 0;
            }
        }
    }
}