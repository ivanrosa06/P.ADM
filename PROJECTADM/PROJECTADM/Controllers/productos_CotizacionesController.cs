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
    public class productos_CotizacionesController : Controller
    {
        private ADMEntities db = new ADMEntities();

        // GET: productos_Cotizaciones
        public ActionResult Index(string busqueda1)
        {
            int orden = 20;
            var productosEspecificos = from data in db.productos_Cotizaciones where data.Id_producto_cotizacion == orden select data;
            var productos_Cotizaciones = db.productos_Cotizaciones.Include(p => p.cotizacione).Include(p => p.inventario);
            var Lista = from data in db.productos_Cotizaciones select data;
            if (string.IsNullOrEmpty(busqueda1))
            {
                return View(productosEspecificos.ToList());
            }
            else
            {
                Lista = Lista.Where(a => a.Id_cotizacion.Equals(Convert.ToInt32(busqueda1)));
                return View(productosEspecificos.ToList());

            }

            
        }

        // GET: productos_Cotizaciones/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            productos_Cotizaciones productos_Cotizaciones = db.productos_Cotizaciones.Find(id);
            if (productos_Cotizaciones == null)
            {
                return HttpNotFound();
            }
            return View(productos_Cotizaciones);
        }

        // GET: productos_Cotizaciones/Create
        public ActionResult Create()
        {
            ViewBag.Id_cotizacion = new SelectList(db.cotizaciones, "Id_cotizacion", "Nombre");
            ViewBag.Id_inventario = new SelectList(db.inventarios, "Id_inventario", "Marca");
            return View();
        }

        // POST: productos_Cotizaciones/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id_producto_cotizacion,Id_inventario,Id_cotizacion,Cantidad")] productos_Cotizaciones productos_Cotizaciones,int Id_cotizacion, int Id_inventario)
        {
            
            if (ModelState.IsValid)
            {
                var query = (from a in db.cotizaciones
                             where a.Id_cotizacion == Id_cotizacion
                             select a).FirstOrDefault();
                var precio =  (from a in db.inventarios
                             where a.Id_inventario == Id_inventario
                               select a).FirstOrDefault();

                query.Monto =query.Monto + precio.precio * Convert.ToInt32(productos_Cotizaciones.Cantidad) ;
                db.SaveChanges();
                query.Impuestos = query.Monto * 18/100;
                db.SaveChanges();
                query.Monto_total = query.Monto + query.Impuestos;
              
                db.productos_Cotizaciones.Add(productos_Cotizaciones);

                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Id_cotizacion = new SelectList(db.cotizaciones, "Id_cotizacion", "Nombre", productos_Cotizaciones.Id_cotizacion);
            ViewBag.Id_inventario = new SelectList(db.inventarios, "Id_inventario", "Marca", productos_Cotizaciones.Id_inventario);
            return View(productos_Cotizaciones);
        }

        // GET: productos_Cotizaciones/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            productos_Cotizaciones productos_Cotizaciones = db.productos_Cotizaciones.Find(id);
            if (productos_Cotizaciones == null)
            {
                return HttpNotFound();
            }
            ViewBag.Id_cotizacion = new SelectList(db.cotizaciones, "Id_cotizacion", "Nombre", productos_Cotizaciones.Id_cotizacion);
            ViewBag.Id_inventario = new SelectList(db.inventarios, "Id_inventario", "Marca", productos_Cotizaciones.Id_inventario);
            return View(productos_Cotizaciones);
        }

        // POST: productos_Cotizaciones/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id_producto_cotizacion,Id_inventario,Id_cotizacion,Cantidad")] productos_Cotizaciones productos_Cotizaciones)
        {
            if (ModelState.IsValid)
            {
                db.Entry(productos_Cotizaciones).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Id_cotizacion = new SelectList(db.cotizaciones, "Id_cotizacion", "Nombre", productos_Cotizaciones.Id_cotizacion);
            ViewBag.Id_inventario = new SelectList(db.inventarios, "Id_inventario", "Marca", productos_Cotizaciones.Id_inventario);
            return View(productos_Cotizaciones);
        }

        // GET: productos_Cotizaciones/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            productos_Cotizaciones productos_Cotizaciones = db.productos_Cotizaciones.Find(id);
            if (productos_Cotizaciones == null)
            {
                return HttpNotFound();
            }
            return View(productos_Cotizaciones);
        }

        // POST: productos_Cotizaciones/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            productos_Cotizaciones productos_Cotizaciones = db.productos_Cotizaciones.Find(id);
            db.productos_Cotizaciones.Remove(productos_Cotizaciones);
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
