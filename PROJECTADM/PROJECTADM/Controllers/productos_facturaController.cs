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
    public class productos_facturaController : Controller
    {
        private ADMEntities db = new ADMEntities();

        // GET: productos_factura
        public ActionResult Index(int? id)
        {
            int orden = 1;
            var productosEspecificos = from data in db.productos_factura where data.Id_cotizacion == id select data;
            var productos_factura = db.productos_factura.Include(p => p.factura).Include(p => p.inventario);
            return View(productosEspecificos.ToList());
        }

        // GET: productos_factura/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            productos_factura productos_factura = db.productos_factura.Find(id);
            if (productos_factura == null)
            {
                return HttpNotFound();
            }
            return View(productos_factura);
        }

        // GET: productos_factura/Create
        public ActionResult Create()
        {
            ViewBag.Id_cotizacion = new SelectList(db.facturas, "Id_factura", "Id_factura");
            ViewBag.Id_inventario = new SelectList(db.inventarios, "Id_inventario", "Marca");
            return View();
        }

        // POST: productos_factura/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id_producto_factura,Id_inventario,Id_cotizacion,Cantidad")] productos_factura productos_factura,int Id_cotizacion, int Id_inventario)
        {
            if (ModelState.IsValid)
            {
                var query = (from a in db.facturas
                             where a.Id_factura == Id_cotizacion
                             select a).FirstOrDefault();
                var precio = (from a in db.inventarios
                              where a.Id_inventario == Id_inventario
                              select a).FirstOrDefault();

                query.monto = query.monto + precio.precio * Convert.ToInt32(productos_factura.Cantidad);
                db.SaveChanges();
                query.impuesto = Convert.ToInt32(query.monto) * 18 / 100;
                db.SaveChanges();
                query.Monto_total = Convert.ToInt32(query.monto) + query.monto;
                db.productos_factura.Add(productos_factura);
                db.SaveChanges();
                return RedirectToAction("Index", new { id = productos_factura.Id_cotizacion });
            }

            ViewBag.Id_cotizacion = new SelectList(db.facturas, "Id_factura", "Estado", productos_factura.Id_cotizacion);
            ViewBag.Id_inventario = new SelectList(db.inventarios, "Id_inventario", "Marca", productos_factura.Id_inventario);
            return View(productos_factura);
        }

        // GET: productos_factura/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            productos_factura productos_factura = db.productos_factura.Find(id);
            if (productos_factura == null)
            {
                return HttpNotFound();
            }
            ViewBag.Id_cotizacion = new SelectList(db.facturas, "Id_factura", "Estado", productos_factura.Id_cotizacion);
            ViewBag.Id_inventario = new SelectList(db.inventarios, "Id_inventario", "Marca", productos_factura.Id_inventario);
            return View(productos_factura);
        }

        // POST: productos_factura/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id_producto_factura,Id_inventario,Id_cotizacion,Cantidad")] productos_factura productos_factura)
        {
            if (ModelState.IsValid)
            {
                db.Entry(productos_factura).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index", new { id = productos_factura.Id_cotizacion });
            }
            ViewBag.Id_cotizacion = new SelectList(db.facturas, "Id_factura", "Estado", productos_factura.Id_cotizacion);
            ViewBag.Id_inventario = new SelectList(db.inventarios, "Id_inventario", "Marca", productos_factura.Id_inventario);
            return View(productos_factura);
        }

        // GET: productos_factura/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            productos_factura productos_factura = db.productos_factura.Find(id);
            if (productos_factura == null)
            {
                return HttpNotFound();
            }
            return View(productos_factura);
        }

        // POST: productos_factura/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            productos_factura productos_factura = db.productos_factura.Find(id);
            db.productos_factura.Remove(productos_factura);
            db.SaveChanges();
            return RedirectToAction("Index", new { id = productos_factura.Id_cotizacion });
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
