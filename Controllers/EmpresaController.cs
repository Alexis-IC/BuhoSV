using Buho.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Buho.Controllers
{
    public class EmpresaController : Controller
    {
        // GET: Empresa
        public ActionResult Index()
        {
            List<EmpresaCLS> listaEmpresas = null;
            
            using (BuhoDBEntities db = new BuhoDBEntities())
            {
                listaEmpresas = (from empresa in db.Empresas
                                 join regEstado in db.Estados
                                 on empresa.Estado equals regEstado.ID_Estado
                                 select new EmpresaCLS
                                 {
                                     idEmpresa = empresa.ID_Empresa,
                                     nombreAbreviado = empresa.NombreAbreviado,
                                     representanteLegal = empresa.RepresentanteLegal,
                                     actividadEmpresa = empresa.ActividadEmpresa,
                                     estado = regEstado.Nombre
                                 }).ToList();
            }

            return View(listaEmpresas);
        }
        [HttpPost]
        public ActionResult Eliminar(int id)
        {
            using (BuhoDBEntities db = new BuhoDBEntities())
            {
                Empresas oEmpresa = db.Empresas.Find(id);
                db.Empresas.Remove(oEmpresa);
                db.SaveChanges();
            }

            return RedirectToAction("Index");
        }
        public ActionResult Editar(int id)
        {
            EmpresaCLS oEmpresaCLS = new EmpresaCLS();
            using (BuhoDBEntities db = new BuhoDBEntities())
            {
                Empresas oEmpresa = db.Empresas.Where(p=>p.ID_Empresa.Equals(id)).First();

                oEmpresaCLS.idEmpresa = id;
                oEmpresaCLS.nombreComercial = oEmpresa.NombreComercial;
                oEmpresaCLS.nombreAbreviado = oEmpresa.NombreAbreviado;
                oEmpresaCLS.representanteLegal = oEmpresa.RepresentanteLegal;
                oEmpresaCLS.actividadEmpresa = oEmpresa.ActividadEmpresa;
                oEmpresaCLS.direccion = oEmpresa.Direccion;
                oEmpresaCLS.telefono = oEmpresa.Telefono;
                oEmpresaCLS.email = oEmpresa.Email;
                oEmpresaCLS.nit = oEmpresa.NIT;
                oEmpresaCLS.registro = oEmpresa.Registro;
                oEmpresaCLS.capAutorizado = (decimal)oEmpresa.CapAutorizado;
                oEmpresaCLS.capSuscrito = (decimal)oEmpresa.CapSuscrito;
                oEmpresaCLS.capPagado = (decimal)oEmpresa.CapPagado;
                oEmpresaCLS.idEstado = 1;
            }

            return View(oEmpresaCLS);
        }
        [HttpPost]
        public ActionResult Editar(EmpresaCLS oEmpresaCLS)
        {
            if(!ModelState.IsValid)
            {
                return View(oEmpresaCLS);
            }

            else if (ExisteCorreo(oEmpresaCLS.email,oEmpresaCLS.idEmpresa))
            {
                ViewBag.Error = "Ya existe una empresa con ese correo.";
                return View(oEmpresaCLS);
            }
            else if (ExisteNit(oEmpresaCLS.nit,oEmpresaCLS.idEmpresa))
            {
                ViewBag.Error = "Ya existe una empresa con ese NIT.";
                return View(oEmpresaCLS);
            }

            using (BuhoDBEntities db = new BuhoDBEntities())
            {
                Empresas oEmpresa = db.Empresas.Where(p => p.ID_Empresa.Equals(oEmpresaCLS.idEmpresa)).First();

                oEmpresa.NombreComercial = oEmpresaCLS.nombreComercial;
                oEmpresa.NombreAbreviado = oEmpresaCLS.nombreAbreviado;
                oEmpresa.RepresentanteLegal = oEmpresaCLS.representanteLegal;
                oEmpresa.ActividadEmpresa = oEmpresaCLS.actividadEmpresa;
                oEmpresa.Direccion = oEmpresaCLS.direccion;
                oEmpresa.Telefono = oEmpresaCLS.telefono;
                oEmpresa.Email = oEmpresaCLS.email;
                oEmpresa.NIT = oEmpresaCLS.nit;
                oEmpresa.Registro = oEmpresaCLS.registro;
                oEmpresa.CapAutorizado = oEmpresaCLS.capAutorizado;
                oEmpresa.CapSuscrito = oEmpresaCLS.capSuscrito;
                oEmpresa.CapPagado = oEmpresaCLS.capPagado;

                db.SaveChanges();
            }

            return RedirectToAction("Index");
        }
        public ActionResult Agregar()
        {

            return View();
        }
        [HttpPost]
        public ActionResult Agregar(EmpresaCLS oEmpresaCLS)
        {
            if (Session["Usuario"] == null)
            {
                return RedirectToAction("Login", "Home");
            }
            else
            {
                if (!ModelState.IsValid)
                {
                    return View(oEmpresaCLS);
                }
                else if (ExisteCorreo(oEmpresaCLS.email))
                {
                    ViewBag.Error = "Ya existe una empresa con ese correo.";
                    return View(oEmpresaCLS);
                }
                else if (ExisteNit(oEmpresaCLS.nit))
                {
                    ViewBag.Error = "Ya existe una empresa con ese NIT.";
                    return View(oEmpresaCLS);
                }
                else
                {
                    Usuarios usuario = (Usuarios)Session["Usuario"];

                    using (BuhoDBEntities db = new BuhoDBEntities())
                    {
                        Empresas oEmpresa = new Empresas();
                        oEmpresa.ID_Usuario = usuario.ID_Usuario;
                        oEmpresa.NombreComercial = oEmpresaCLS.nombreComercial;
                        oEmpresa.NombreAbreviado = oEmpresaCLS.nombreAbreviado;
                        oEmpresa.RepresentanteLegal = oEmpresaCLS.representanteLegal;
                        oEmpresa.ActividadEmpresa = oEmpresaCLS.actividadEmpresa;
                        oEmpresa.Direccion = oEmpresaCLS.direccion;
                        oEmpresa.Telefono = oEmpresaCLS.telefono;
                        oEmpresa.Email = oEmpresaCLS.email;
                        oEmpresa.NIT = oEmpresaCLS.nit;
                        oEmpresa.Registro = oEmpresaCLS.registro;
                        oEmpresa.CapAutorizado = oEmpresaCLS.capAutorizado;
                        oEmpresa.CapSuscrito = oEmpresaCLS.capSuscrito;
                        oEmpresa.CapPagado = oEmpresaCLS.capPagado;
                        oEmpresa.Estado = 1;

                        db.Empresas.Add(oEmpresa);
                        db.SaveChanges();
                    }
                }
            }

            return RedirectToAction("Agregar","Empresa");
        }

        private Boolean ExisteCorreo(string correo)
        {
            using (BuhoDBEntities db = new BuhoDBEntities())
            {
                int nRegistros = db.Empresas.Where(p => p.Email.Equals(correo)).Count();
                return nRegistros > 0;
            }
        }

        private Boolean ExisteCorreo(string correo, int id)
        {
            using (BuhoDBEntities db = new BuhoDBEntities())
            {
                int nRegistros = db.Empresas.Where(p => p.Email.Equals(correo) && !p.ID_Empresa.Equals(id)).Count();
                return nRegistros > 0;
            }
        }

        private Boolean ExisteNit(string nit)
        {
            using (BuhoDBEntities db = new BuhoDBEntities())
            {
                int nRegistros = db.Empresas.Where(p => p.NIT.Equals(nit)).Count();
                return nRegistros > 0;
            }
        }
        private Boolean ExisteNit(string nit,int id)
        {
            using (BuhoDBEntities db = new BuhoDBEntities())
            {
                int nRegistros = db.Empresas.Where(p => p.NIT.Equals(nit) && !p.ID_Empresa.Equals(id)).Count();
                return nRegistros > 0;
            }
        }
    }
}