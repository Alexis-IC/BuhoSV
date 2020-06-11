using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;


namespace Buho.Models
{
    public class ProveedorCLS
    {
        [Display(Name ="ID")]
        public int idProveedor { get; set; }
        [Required]
        [Display(Name = "Nombre")]
        [StringLength(25, ErrorMessage = "Longitud permitida entre 3 y 25 caracteres", MinimumLength = 3)]
        public string nombre { get; set; }
        [Required]
        [Display(Name = "Apellido")]
        [StringLength(25, ErrorMessage = "Longitud permitida entre 3 y 25 caracteres", MinimumLength = 3)]
        public string apellido { get; set; }
        [Required]
        [Display(Name = "DUI")]
        [RegularExpression(@"^\d{9}$", ErrorMessage = "Debe ingresar algo como 000000001")]
        [StringLength(9, ErrorMessage = "Debe contener 9 números exactamente", MinimumLength = 9)]

        public string dui { get; set; }

        [Required]
        [Display(Name = "Dirección")]
        [DataType(DataType.MultilineText)]
        [StringLength(50, ErrorMessage = "Longitud permitida entre 10 y 50 caracteres", MinimumLength = 10)]
        public string direccion { get; set; }
        [Required]
        [Display(Name = "Correo")]
        [RegularExpression(@"\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*", ErrorMessage = "Dirección de Correo electrónico incorrecta.")]
        [StringLength(50, ErrorMessage = "Longitud permitida entre 10 y 50 caracteres", MinimumLength = 10)]
        public string email { get; set; }

        [Required]
        [Display(Name = "Razón Social")]
        [StringLength(25, ErrorMessage = "Longitud permitida entre 5 y 25 caracteres", MinimumLength = 5)]
        public string razonSocial { get; set; }
    }
}