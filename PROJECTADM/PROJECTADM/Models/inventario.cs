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
    
    public partial class inventario
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public inventario()
        {
            this.productos_Cotizaciones = new HashSet<productos_Cotizaciones>();
            this.productos_factura = new HashSet<productos_factura>();
            this.productos_OrdenesCompra = new HashSet<productos_OrdenesCompra>();
        }
    
        public int Id_inventario { get; set; }
        public string Marca { get; set; }
        public string Nombre { get; set; }
        public string Especificacion { get; set; }
        public Nullable<int> Stock { get; set; }
        public string Tipo { get; set; }
        public Nullable<int> precio { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<productos_Cotizaciones> productos_Cotizaciones { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<productos_factura> productos_factura { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<productos_OrdenesCompra> productos_OrdenesCompra { get; set; }
    }
}
