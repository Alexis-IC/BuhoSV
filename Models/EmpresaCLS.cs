using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Buho.Models
{
    public class EmpresaCLS
    {
        [Display(Name ="ID")]
        public int idEmpresa { get; set; }
        public int idUsuario { get; set; }
        [Required]
        [Display(Name = "N.Comercial")]
        [StringLength(50, ErrorMessage = "Longitud permitida entre 3 y 50 caracteres", MinimumLength = 3)]
        public string nombreComercial { get; set; }
        [Display(Name = "N.Abreviado")]
        [Required]
        [StringLength(25, ErrorMessage = "Longitud permitida entre 3 y 25 caracteres", MinimumLength = 3)]
        public string nombreAbreviado { get; set; }
        [Required]
        [Display(Name ="Representante")]
        [StringLength(25, ErrorMessage = "Longitud permitida entre 3 y 25 caracteres", MinimumLength = 3)]
        public string representanteLegal { get; set; }
        [Required]
        [Display(Name = "Actividad")]
        [StringLength(25, ErrorMessage = "Longitud permitida entre 3 y 25 caracteres", MinimumLength = 3)]
        public string actividadEmpresa { get; set; }
        [Required]
        [Display(Name = "Dirección")]
        [DataType(DataType.MultilineText)]
        [StringLength(100, ErrorMessage = "Longitud permitida entre 10 y 100 caracteres", MinimumLength = 10)]
        public string direccion { get; set; }
        [Required]
        [Display(Name = "Teléfono")]
        [RegularExpression(@"^\d{8}$", ErrorMessage = "Teléfono incorrecto.")]
        public string telefono { get; set; }
        [Required]
        [Display(Name = "Correo")]
        [RegularExpression(@"\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*",ErrorMessage = "Dirección de Correo electrónico incorrecta.")]
        public string email { get; set; }
        [Required]
        [Display(Name = "NIT")]
        [RegularExpression(@"^\d{14}$", ErrorMessage = "NIT incorrecto.")]
        public string nit { get; set; }
        [Required]
        [Display(Name = "Registro")]
        [StringLength(50, ErrorMessage = "Longitud permitida entre 5 y 50 caracteres", MinimumLength = 5)]
        public string registro { get; set; }
        [Display(Name = "C.Autorizado")]
        [Range(0,999999.99,ErrorMessage ="Cantidad errónea.")]                                      
        public decimal capAutorizado { get; set; }
        [Display(Name = "C.Suscrito")]
        [Range(0, 999999.99, ErrorMessage = "Cantidad errónea.")]
        public decimal capSuscrito { get; set; }
        [Display(Name = "C.Pagado")]
        [Range(0, 999999.99, ErrorMessage = "Cantidad errónea.")]
        public decimal capPagado { get; set; }
        
        public int idEstado { get; set; }

        //InnerJoin
        [Display(Name = "Usuario")]
        public string usuario { get; set; }
        [Display(Name = "Estado")]
        public string estado { get; set; }

    }
}