using Buho.Models;
using Microsoft.Ajax.Utilities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Buho.Controllers
{
    public class ProductoController : Controller
    {
        // GET: Producto
        public ActionResult Index()
        {
            if (TempData["Error"] != null)
            {
                ViewBag.TituloError = "No se pudo eliminar el producto";
                ViewBag.Error = "Existen registros de facturas con este producto, deberá borrar primero esta información para evitar problemas mayores.";
            }

            List<ProductoCLS> listaProductos = null;

            using (BuhoDBEntities db = new BuhoDBEntities())
            {
                listaProductos = (from producto in db.Productos
                                  select new ProductoCLS
                                  {
                                      idProducto = producto.ID_Producto,
                                      nombre = producto.Nombre,
                                      descripcion = producto.Descripcion,
                                      imagen = producto.Imagen,
                                      precio = (decimal)producto.Precio
                                  }).ToList();
            }

            return View(listaProductos);
        }
        public ActionResult Eliminar(int id)
        {
            int nRegistros = 0;

            using (BuhoDBEntities db = new BuhoDBEntities())
            {
                nRegistros = db.DetalleVentas.Where(p => p.ID_Producto.Value.Equals(id)).Count();
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
                    Productos oProducto = db.Productos.Find(id);
                    db.Productos.Remove(oProducto);
                    db.SaveChanges();
                }
            }

            return RedirectToAction("Index");
        }
        public ActionResult Editar(int id)
        {
            ProductoCLS oProductoCLS = new ProductoCLS();

            using (BuhoDBEntities db = new BuhoDBEntities())
            {
                Productos oProducto = db.Productos.Where(p => p.ID_Producto.Equals(id)).First();

                oProductoCLS.idProducto = id;
                oProductoCLS.nombre = oProducto.Nombre;
                oProductoCLS.descripcion = oProducto.Descripcion;
                oProductoCLS.precio = (decimal)oProducto.Precio;
                oProductoCLS.imagen = oProducto.Imagen;
            }

            return View(oProductoCLS);
        }
        [HttpPost]
        public ActionResult Editar(ProductoCLS oProductoCLS)
        {
            if(!ModelState.IsValid)
            {
                return View(oProductoCLS);
            }
            else if(ExisteProducto(oProductoCLS.nombre,oProductoCLS.idProducto))
            {
                ViewBag.TituloError = "Problema con producto";
                ViewBag.Error = "Ya existe un producto con el mismo nombre.";
                return View(oProductoCLS);
            }

            if(oProductoCLS.archivoImagen != null)
            {
                string nombreImagen = Path.GetFileNameWithoutExtension(oProductoCLS.archivoImagen.FileName);
                string extension = Path.GetExtension(oProductoCLS.archivoImagen.FileName);

                nombreImagen += DateTime.Now.ToString("dMMyyyy-ff") + extension;
                oProductoCLS.imagen = "~/dist/img/productos/" + nombreImagen;

                string rutaGuardar = Path.Combine(Server.MapPath("~/dist/img/productos/"), nombreImagen);

                oProductoCLS.archivoImagen.SaveAs(rutaGuardar);
            }

            using (BuhoDBEntities db = new BuhoDBEntities())
            {
                Productos oProducto = db.Productos.Where(p => p.ID_Producto.Equals(oProductoCLS.idProducto)).First();
                oProducto.Nombre = oProductoCLS.nombre;
                oProducto.Imagen = oProductoCLS.imagen;
                oProducto.Descripcion = oProductoCLS.descripcion;
                oProducto.Precio = oProductoCLS.precio;
                db.SaveChanges();
            }

            return RedirectToAction("Index");
        }

        public ActionResult Agregar()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Agregar(ProductoCLS oProductoCLS)
        {
            if (!ModelState.IsValid)
            {
                return View(oProductoCLS);
            }

            using (BuhoDBEntities db = new BuhoDBEntities())
            {
                if (ExisteProducto(oProductoCLS.nombre))
                {
                    ViewBag.TituloError = "Problema con producto";
                    ViewBag.Error = "Ya existe un producto con el mismo nombre.";
                    return View(oProductoCLS);
                }

                string nombreImagen = Path.GetFileNameWithoutExtension(oProductoCLS.archivoImagen.FileName);
                string extension = Path.GetExtension(oProductoCLS.archivoImagen.FileName);

                nombreImagen += DateTime.Now.ToString("dMMyyyy-ff") + extension;
                oProductoCLS.imagen = "~/dist/img/productos/"+nombreImagen;

                string rutaGuardar = Path.Combine(Server.MapPath("~/dist/img/productos/"),nombreImagen);

                oProductoCLS.archivoImagen.SaveAs(rutaGuardar);

                Productos oProducto = new Productos();
                oProducto.Nombre = oProductoCLS.nombre;
                oProducto.Descripcion = oProductoCLS.descripcion;
                oProducto.Precio = oProductoCLS.precio;
                oProducto.Imagen = oProductoCLS.imagen;

                db.Productos.Add(oProducto);
                db.SaveChanges();
            }

            return RedirectToAction("Index");
        }

        private bool ExisteProducto(string nombre)
        {
            int nRegistros = 0;

            using (BuhoDBEntities db = new BuhoDBEntities())
            {
                nRegistros = db.Productos.Where(p => p.Nombre.Equals(nombre)).Count();
            }

            return nRegistros > 0;
        }

        private bool ExisteProducto(string nombre, int id)
        {
            int nRegistros = 0;

            using (BuhoDBEntities db = new BuhoDBEntities())
            {
                nRegistros = db.Productos.Where(p => p.Nombre.Equals(nombre) && !p.ID_Producto.Equals(id)).Count();
            }

            return nRegistros > 0;
        }


    }
}