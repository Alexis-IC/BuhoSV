using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Buho.Models
{
    public class DetalleVentaCLS
    {
        public int idDetalleVenta { get; set; }
        public int idVenta { get; set; }
        public int idProducto { get; set; }
        [Display(Name = "Cantidad")]
        public int cantidad { get; set; }
        [Display(Name = "Precio unitario")]
        public decimal precio { get; set; }

        //Extras
        [Display(Name ="Producto")]
        public string nombreProducto { get; set; }
        [Display(Name = "Descripción")]
        public string detalleProducto { get; set; }
        [Display(Name = "Imagen")]
        public string imagenProducto { get; set; }
    }
}