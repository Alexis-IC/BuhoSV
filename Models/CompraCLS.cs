using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Buho.Models
{
    public class CompraCLS
    {
        [Display(Name ="ID")]
        public int idCompra { get; set; }
        public int idProveedor { get; set; }
        public int idImpuesto { get; set; }
        [Display(Name = "Fecha")]
        public DateTime fecha { get; set; }

        //Extras
        [Display(Name = "Nombre del producto")]
        public string nombreProducto { get; set; }
        [Display(Name = "Cantidad")]
        public int cantProducto { get; set; }
        [Display(Name = "Precio unitario")]
        public decimal precio { get; set; }
        [Display(Name = "Proveedor")]
        public string nombreProveedor { get; set; }
        [Display(Name = "Impuesto")]
        public string nombreImpuesto { get; set; }
    }
}