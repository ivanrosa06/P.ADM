//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace PROJECTADM.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class ordenesCompra
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public ordenesCompra()
        {
            this.productos_OrdenesCompra = new HashSet<productos_OrdenesCompra>();
        }
    
        public int Id_Orden_compra { get; set; }
        public Nullable<int> Id_proveedor { get; set; }
        public Nullable<int> Estado { get; set; }
        public string Monto { get; set; }
        public Nullable<int> Impuesto { get; set; }
        public Nullable<int> Monto_total { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<productos_OrdenesCompra> productos_OrdenesCompra { get; set; }
        public virtual proveedore proveedore { get; set; }
    }
}
