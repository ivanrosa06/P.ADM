using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using CrystalDecisions.CrystalReports.Engine;
using PROJECTADM.Models;

namespace PROJECTADM.Controllers
{
    public class cotizacionesController : Controller
    {
        private ADMEntities db = new ADMEntities();
        private ADMEntities3 db2 = new ADMEntities3();

        // GET: cotizaciones
        public ActionResult Index()
        {
            return View(db.cotizaciones.ToList());
        }
        public ActionResult ProductosCotizaciones()
        {
         
            return View();
        }
        // GET: cotizaciones/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            cotizacione cotizacione = db.cotizaciones.Find(id);
            if (cotizacione == null)
            {
                return HttpNotFound();
            }
            return View(cotizacione);
        }

        // GET: cotizaciones/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: cotizaciones/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id_cotizacion,Nombre,Apellido,Monto,Impuestos,Monto_total,Tipo")] cotizacione cotizacione)
        {
            if (ModelState.IsValid)
            {
                parametros parametros = new parametros();
                parametros.Cotizacion = cotizacione.Id_cotizacion;
                db.cotizaciones.Add(cotizacione);
                db.SaveChanges();
                return RedirectToAction("Index", "productos_Cotizaciones", new { id = cotizacione.Id_cotizacion });
               
            }

            return View(cotizacione);
        }

        // GET: cotizaciones/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            cotizacione cotizacione = db.cotizaciones.Find(id);
            if (cotizacione == null)
            {
                return HttpNotFound();
            }
            return View(cotizacione);
        }

        // POST: cotizaciones/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id_cotizacion,Nombre,Apellido,Monto,Impuestos,Monto_total,Tipo")] cotizacione cotizacione)
        {
            if (ModelState.IsValid)
            {
                db.Entry(cotizacione).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(cotizacione);
        }

        // GET: cotizaciones/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            cotizacione cotizacione = db.cotizaciones.Find(id);
            if (cotizacione == null)
            {
                return HttpNotFound();
            }
            return View(cotizacione);
        }

        // POST: cotizaciones/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            cotizacione cotizacione = db.cotizaciones.Find(id);
            db.cotizaciones.Remove(cotizacione);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult ExportCotizaciones()
        {
            List<cotizacione> allOrdenes = new List<cotizacione>();
            allOrdenes = db.cotizaciones.ToList();


            ReportDocument rd = new ReportDocument();
            rd.Load(Path.Combine(Server.MapPath("~/Reporte"), "CotizacionesReport.rpt"));

            var lista = from data in db.cotizaciones

                        select data;

            //rd.SetDataSource(lista.ToList());



            Response.Buffer = false;
            Response.ClearContent();
            Response.ClearHeaders();


            Stream stream = rd.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
            stream.Seek(0, SeekOrigin.Begin);
            return File(stream, "application/pdf", "CotizacionesReport.pdf");
        }
        public ActionResult ExportFactura()
        {
            List<cotizacione> allOrdenes = new List<cotizacione>();
            allOrdenes = db.cotizaciones.ToList();


            ReportDocument rd = new ReportDocument();
            rd.Load(Path.Combine(Server.MapPath("~/Reporte"), "CotizacionFactura.rpt"));

            var lista = from data in db.proveedores

                        select data;

            //rd.SetDataSource(db2.FacturaCotizacion3(14).ToList());



            Response.Buffer = false;
            Response.ClearContent();
            Response.ClearHeaders();


            Stream stream = rd.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
            stream.Seek(0, SeekOrigin.Begin);
            return File(stream, "application/pdf", "CotizacionFactura.pdf");
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
