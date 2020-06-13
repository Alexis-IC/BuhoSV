using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Buho.Models
{
    public class VentaCLS
    {
        [Display(Name = "ID")]
        public int idVenta { get; set; }
        [Display(Name ="ID Cliente")]
        public int idCliente { get; set; }
        public int idImpuesto { get; set; }
        [Display(Name ="Fecha Expedida")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode=true)]
        public DateTime fecha { get; set; }


        //Complementario
        [Display(Name ="Cliente")]
        public string nombreCliente { get; set; }
        [Display(Name = "DUI Cliente")]
        public string duiCliente { get; set; }
        [Display(Name = "Impuesto")]
        public string nombreImpuesto { get; set; }
    }
}