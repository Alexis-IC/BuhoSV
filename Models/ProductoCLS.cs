using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Buho.Models
{
    public class ProductoCLS
    {
        [Display(Name ="ID")]
        public int idProducto { get; set; }
        [Display(Name = "Nombre")]
        public string nombre { get; set; }
        [Display(Name = "Detalle")]
        public string descripcion { get; set; }
        [Display(Name = "Imagen")]
        public string imagen { get; set; }
        [Display(Name = "Precio")]
        public decimal precio { get; set; }

        public HttpPostedFileBase archivoImagen { get; set; }
    }
}