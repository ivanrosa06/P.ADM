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
    
    public partial class factura
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public factura()
        {
            this.productos_factura = new HashSet<productos_factura>();
        }
    
        public int Id_factura { get; set; }
        public Nullable<int> Id_cliente { get; set; }
        public Nullable<int> monto { get; set; }
        public Nullable<int> impuesto { get; set; }
        public Nullable<int> Monto_total { get; set; }
        public string Estado { get; set; }
    
        public virtual cliente cliente { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<productos_factura> productos_factura { get; set; }
    }
}
