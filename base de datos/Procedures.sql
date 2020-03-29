use ADM

go

create procedure FacturaCotizacion
@Id_cotizacion int
 as 
 Select *From productos_Cotizaciones pc
 left join cotizaciones c
 on c.Id_cotizacion = pc.Id_cotizacion
 where PC.Id_cotizacion=@Id_cotizacion



 create procedure FacturaCotizacion2
@Id_cotizacion int
 as 
 Select *From productos_Cotizaciones pc
 where PC.Id_cotizacion=@Id_cotizacion

  create procedure FacturaCotizacion3
@Id_cotizacion int
 as 
 Select *From cotizaciones pc
 where PC.Id_cotizacion=@Id_cotizacion


 create procedure FacturaOrdenes
@Id_Orden_compra int
 as 
 Select *From productos_OrdenesCompra pc
 left join ordenesCompras c
 on c.Id_Orden_compra = pc.Id_ordenesDeCopras
 where PC.Id_ordenesDeCopras=@Id_Orden_compra

   create procedure Facturaordenes3
@Id_Orden_compra int
 as 
 Select *From ordenesCompras pc
 where PC.Id_Orden_compra=@Id_Orden_compra

   create procedure Facturaordenes2
@Id_Orden_compra int
 as 
 Select *From productos_OrdenesCompra pc
 where PC.Id_ordenesDeCopras=@Id_Orden_compra




   create procedure Factura3
@Id_factura int
 as 
 Select *From factura pc
 where PC.Id_factura=@Id_factura

   create procedure Factura2
@Id_factura int
 as 
 Select *From productos_factura pc
 where PC.Id_cotizacion=@Id_factura
