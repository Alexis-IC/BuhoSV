//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Buho.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class CuentaPartida
    {
        public int ID_CuentaPartida { get; set; }
        public Nullable<int> ID_Partida { get; set; }
        public Nullable<int> ID_Cuenta { get; set; }
        public Nullable<int> ID_Venta { get; set; }
        public Nullable<int> ID_Compra { get; set; }
        public Nullable<decimal> Debe { get; set; }
        public Nullable<decimal> Haber { get; set; }
    
        public virtual Compras Compras { get; set; }
        public virtual Cuentas Cuentas { get; set; }
        public virtual Partidas Partidas { get; set; }
        public virtual Ventas Ventas { get; set; }
    }
}
