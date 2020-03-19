using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using PROJECTADM.Models;

namespace PROJECTADM.Controllers
{
    public class productos_OrdenesCompraController : Controller
    {
        private ADMEntities db = new ADMEntities();

        // GET: productos_OrdenesCompra
        public ActionResult Index(Models.parametros par)
        {
            
            var productos_OrdenesCompra = db.productos_OrdenesCompra.Include(p => p.inventario).Include(p => p.ordenesCompra);
            int orden = 9;
            var productosEspecificos = from data in db.productos_OrdenesCompra where data.Id_ordenesDeCopras == orden select data;
            return View(productosEspecificos.ToList());
        }

        // GET: productos_OrdenesCompra/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            productos_OrdenesCompra productos_OrdenesCompra = db.productos_OrdenesCompra.Find(id);
            if (productos_OrdenesCompra == null)
            {
                return HttpNotFound();
            }
            return View(productos_OrdenesCompra);
        }

        // GET: productos_OrdenesCompra/Create
        public ActionResult Create()
        {
            ViewBag.Id_producto = new SelectList(db.inventarios, "Id_inventario", "Marca");
            ViewBag.Id_ordenesDeCopras = new SelectList(db.ordenesCompras, "Id_Orden_compra", "Id_Orden_compra");
            return View();
        }

        // POST: productos_OrdenesCompra/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id_producto_orden_compra,Id_producto,Id_ordenesDeCopras,Cantidad")] productos_OrdenesCompra productos_OrdenesCompra,int Id_ordenesDeCopras, int Id_producto)
        {
            
            ViewBag.orden = productos_OrdenesCompra.Id_ordenesDeCopras;
            if (ModelState.IsValid)
            {
                var query = (from a in db.ordenesCompras
                             where a.Id_Orden_compra == Id_ordenesDeCopras
                             select a).FirstOrDefault();
                var precio = (from a in db.inventarios
                              where a.Id_inventario == Id_producto
                              select a).FirstOrDefault();

                query.Monto = query.Monto + precio.precio * Convert.ToInt32(productos_OrdenesCompra.Cantidad);
                db.SaveChanges();
                query.Impuesto = Convert.ToInt32(query.Monto) * 18 / 100;
                db.SaveChanges();
                query.Monto_total = Convert.ToInt32(query.Monto) + query.Impuesto;
                db.productos_OrdenesCompra.Add(productos_OrdenesCompra);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Id_producto = new SelectList(db.inventarios, "Id_inventario", "Marca", productos_OrdenesCompra.Id_producto);
            ViewBag.Id_ordenesDeCopras = new SelectList(db.ordenesCompras, "Id_Orden_compra", "Monto", productos_OrdenesCompra.Id_ordenesDeCopras);
            return View(productos_OrdenesCompra);
        }

        // GET: productos_OrdenesCompra/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            productos_OrdenesCompra productos_OrdenesCompra = db.productos_OrdenesCompra.Find(id);
            if (productos_OrdenesCompra == null)
            {
                return HttpNotFound();
            }
            ViewBag.Id_producto = new SelectList(db.inventarios, "Id_inventario", "Marca", productos_OrdenesCompra.Id_producto);
            ViewBag.Id_ordenesDeCopras = new SelectList(db.ordenesCompras, "Id_Orden_compra", "Monto", productos_OrdenesCompra.Id_ordenesDeCopras);
            return View(productos_OrdenesCompra);
        }

        // POST: productos_OrdenesCompra/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id_producto_orden_compra,Id_producto,Id_ordenesDeCopras,Cantidad")] productos_OrdenesCompra productos_OrdenesCompra)
        {
            if (ModelState.IsValid)
            {
                db.Entry(productos_OrdenesCompra).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Id_producto = new SelectList(db.inventarios, "Id_inventario", "Marca", productos_OrdenesCompra.Id_producto);
            ViewBag.Id_ordenesDeCopras = new SelectList(db.ordenesCompras, "Id_Orden_compra", "Monto", productos_OrdenesCompra.Id_ordenesDeCopras);
            return View(productos_OrdenesCompra);
        }

        // GET: productos_OrdenesCompra/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            productos_OrdenesCompra productos_OrdenesCompra = db.productos_OrdenesCompra.Find(id);
            if (productos_OrdenesCompra == null)
            {
                return HttpNotFound();
            }
            return View(productos_OrdenesCompra);
        }

        // POST: productos_OrdenesCompra/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            productos_OrdenesCompra productos_OrdenesCompra = db.productos_OrdenesCompra.Find(id);
            db.productos_OrdenesCompra.Remove(productos_OrdenesCompra);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
