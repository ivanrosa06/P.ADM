create database ADM
use ADM
Create table inventario(
Id_inventario int identity(1,1),
Marca varchar(50),
Nombre varchar(50),
Especificacion varchar(50),
Stock int,
Tipo varchar(50),
precio int,
primary key (Id_inventario)
);


Create table cliente(
Id_cliente int identity(1,1),
Nombre varchar(50),
Apellido varchar(50),
Telefono varchar(50),
Direccion varchar(50), 
Cedula int,
Tipo varchar(50),
primary key (Id_cliente)
);


Create table factura(
Id_factura int identity(1,1),
Id_cliente int ,
monto int,
impuesto int,
Monto_total int,
Estado varchar(50),
primary key (Id_factura),
constraint Cliente_Factura_FK FOREIGN KEY (Id_cliente) references cliente(Id_cliente)
);
Create table productos_factura(
Id_producto_factura int identity(1,1),
Id_inventario int,
Id_cotizacion int,
Cantidad varchar(50),
primary key (Id_producto_factura),
constraint Factura_factura_FK FOREIGN KEY (Id_cotizacion) references factura(Id_factura),
constraint productos_factura_FK FOREIGN KEY (Id_inventario) references inventario(Id_inventario)
);


Create table cotizaciones(
Id_cotizacion int identity(1,1),
Nombre varchar(50),
Apellido varchar(50),
Monto int,
Impuestos int,
Monto_total int,
Tipo varchar(50),
primary key (id_cotizacion)
);


Create table productos_Cotizaciones(
Id_producto_cotizacion int identity(1,1),
Id_inventario int,
Id_cotizacion int,
Cantidad varchar(50),
primary key (Id_producto_cotizacion),
constraint Cotizacion_Cotizaciones_FK FOREIGN KEY (Id_cotizacion) references cotizaciones(Id_cotizacion),
constraint productos_Cotizaciones_FK FOREIGN KEY (Id_inventario) references inventario(Id_inventario)
);


Create table proveedores(
Id_proveedor int identity(1,1),
Nombre varchar(50),
Apellido varchar(50),
Telefono varchar(50),
Direccion varchar(50), 
RNC int,
Tipo varchar(50),
primary key (Id_proveedor)
);


Create table ordenesCompras(
Id_Orden_compra int identity(1,1),
Id_proveedor int,
Estado int,
Monto varchar(50),
Impuesto int,
Monto_total int,
primary key (Id_Orden_compra),
constraint proveedores_ordenescompras_FK FOREIGN KEY (Id_proveedor) references proveedores(Id_proveedor)
);


Create table productos_OrdenesCompra(
Id_producto_orden_compra int identity(1,1),
Id_producto int,
Id_ordenesDeCopras int,
Cantidad varchar(50),
primary key (Id_producto_orden_compra),
constraint productos_ordenescompras_FK FOREIGN KEY (Id_producto) references Inventario(id_inventario),
constraint ordenes_ordenescompras_FK FOREIGN KEY (Id_ordenesDeCopras) references OrdenesCompras(Id_Orden_compra)
);


--Create table GastosEIngresos(
--Id_GastosEIngresos int identity(1,1),
--monto int,
--fecha date,
--tipo varchar(50),
--primary key (Id_GastosEIngresos)

--);


Create table contabilidad(
Id_Contabilidad int identity(1,1),
Gastos int,
Ingresos int,
Fecha date,
primary key (Id_Contabilidad)

);

--factura con sus productos
select f.Id_factura,pf.Id_inventario,i.Nombre,i.Especificacion,pf.Cantidad from factura f
left join productos_factura pf
on f.Id_factura= pf.Id_cotizacion
left join inventario i
on i.Id_inventario= pf.Id_inventario


select f.Id_factura,pf.Id_inventario,i.Nombre,i.Especificacion,pf.Cantidad from factura f
left join productos_factura pf
on f.Id_factura= pf.Id_cotizacion
left join inventario i
on i.Id_inventario= pf.Id_inventario
where f.Estado= 'no pagado'


select f.Id_factura,pf.Id_inventario,i.Nombre,i.Especificacion,pf.Cantidad from factura f
left join productos_factura pf
on f.Id_factura= pf.Id_cotizacion
left join inventario i
on i.Id_inventario= pf.Id_inventario
where f.Estado= 'pagado'



select f.Id_factura,pf.Id_inventario,i.Nombre,i.Especificacion,pf.Cantidad from factura f
left join productos_factura pf
on f.Id_factura= pf.Id_cotizacion
left join inventario i
on i.Id_inventario= pf.Id_inventario
where id