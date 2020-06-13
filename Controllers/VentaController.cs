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

        public ActionResult Index()
        {
            List<VentaCLS> listaVentas = null;
            using (BuhoDBEntities db = new BuhoDBEntities())
            {
                listaVentas = (from venta in db.Ventas
                               join cliente in db.Clientes
                               on venta.ID_Cliente equals cliente.ID_Cliente
                               join impuesto in db.Impuestos
                               on venta.ID_Impuesto equals impuesto.ID_Impuesto
                               select new VentaCLS
                               {
                                   idVenta = venta.ID_Venta,
                                   nombreCliente = cliente.Nombre,
                                   duiCliente = cliente.DUI,
                                   nombreImpuesto = impuesto.Nombre,
                                   fecha = (DateTime)venta.Fecha
                               }).ToList();
            }

            return View(listaVentas);
        }
        
        public ActionResult Detalle(int id)
        {
            List<DetalleVentaCLS> listaDetalle = null;
            using (BuhoDBEntities db = new BuhoDBEntities())
            {
                listaDetalle = (from detalle in db.DetalleVentas
                                join producto in db.Productos
                                on detalle.ID_Producto equals producto.ID_Producto
                                where detalle.ID_Venta == id
                                select new DetalleVentaCLS
                                {
                                    idDetalleVenta = detalle.ID_DetalleVenta,
                                    idVenta = id,
                                    detalleProducto = producto.Descripcion,
                                    cantidad = (int)detalle.Cantidad,
                                    nombreProducto = producto.Nombre,
                                    precio = (decimal)producto.Precio,
                                    idProducto = producto.ID_Producto,
                                    imagenProducto = producto.Imagen
                                }).ToList();
            }

            return View(listaDetalle);
        }

        public ActionResult Editar(int id)
        {
            ViewBag.lista = LlenarCombo();
            VentaCLS oVentaCLS = new VentaCLS();

            using (BuhoDBEntities db = new BuhoDBEntities())
            {
                Ventas oVenta = db.Ventas.Where(p => p.ID_Venta.Equals(id)).First();
                oVentaCLS.idVenta = id;
                oVentaCLS.idCliente = (int)oVenta.ID_Cliente;
                oVentaCLS.fecha = (DateTime)oVenta.Fecha;
            }

            return View(oVentaCLS);
        }
        
        [HttpPost]
        public ActionResult Editar(VentaCLS oVentaCLS)
        {
            if(!ModelState.IsValid)
            {
                ViewBag.lista = LlenarCombo();
                return View(oVentaCLS);
            }

            using (BuhoDBEntities db = new BuhoDBEntities())
            {
                Ventas oVenta = db.Ventas.Find(oVentaCLS.idVenta);
                oVenta.ID_Cliente = oVentaCLS.idCliente;
                oVenta.Fecha = oVentaCLS.fecha;
                db.SaveChanges();
            }

            return RedirectToAction("Index");
        }

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
            return RedirectToAction("Index");
        }
    }
}