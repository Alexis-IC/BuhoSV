using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Buho.Models;

namespace Buho.Controllers
{
    public class CompraController : Controller
    {
        // GET: Compra
        public ActionResult Index()
        {
            List<CompraCLS> listaCompras = null;
            using (BuhoDBEntities db = new BuhoDBEntities())
            {
                listaCompras = (from compra in db.Compras
                               join proveedor in db.Proveedores
                               on compra.ID_Proveedor equals proveedor.ID_Proveedor
                               join impuesto in db.Impuestos
                               on compra.ID_Impuesto equals impuesto.ID_Impuesto
                               select new CompraCLS
                               {
                                   idCompra = compra.ID_Compra,
                                   idProveedor = (int)compra.ID_Proveedor,
                                   idImpuesto = (int)compra.ID_Impuesto,
                                   nombreImpuesto = impuesto.Nombre,
                                   nombreProveedor = proveedor.Nombre,
                                   fecha = (DateTime)compra.Fecha
                               }).ToList();
            }

            return View(listaCompras);
        }

        public ActionResult Detalle(int id)
        {
            List<CompraCLS> listaDetalle = null;
            using (BuhoDBEntities db = new BuhoDBEntities())
            {
                listaDetalle = (from detalle in db.DetalleCompras
                                where detalle.ID_Compra == id
                                select new CompraCLS
                                {
                                    nombreProducto = detalle.Nombre,
                                    cantProducto = (int)detalle.Cantidad,
                                    precio = (decimal)detalle.Precio
                                }).ToList();
            }

            return View(listaDetalle);
        }
    }
}