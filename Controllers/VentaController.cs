using Buho.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Buho.Controllers
{
    public class VentaController : Controller
    {
        // GET: Venta
        public List<SelectListItem> LlenarCombo()
        {
            List<SelectListItem> combo = null;

            using (BuhoDBEntities db = new BuhoDBEntities())
            {
                combo = (from clientes in db.Clientes
                         select new SelectListItem
                         {
                             Text = clientes.DUI,
                             Value = clientes.ID_Cliente.ToString()
                         }).ToList();
            }

            return combo;
        }
        public ActionResult Agregar()
        {
            ViewBag.lista = LlenarCombo();
            return View();
        }
        [HttpPost]
        public ActionResult Agregar(VentaCLS oVentaCLS)
        {
            if(!ModelState.IsValid)
            {
                ViewBag.lista = LlenarCombo();
                return View(oVentaCLS);
            }
            else
            {
                using (BuhoDBEntities db = new BuhoDBEntities())
                {
                    Ventas oVenta = new Ventas();
                    oVenta.ID_Cliente = oVentaCLS.idCliente;
                    oVenta.ID_Impuesto = 1;
                    oVenta.Fecha = oVentaCLS.fecha;

                    db.Ventas.Add(oVenta);
                    db.SaveChanges();
                }
            }
            return RedirectToAction("Index","Cliente");
        }
    }
}