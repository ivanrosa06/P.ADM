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
    public class facturasController : Controller
    {
        private ADMEntities db = new ADMEntities();

        // GET: facturas
        public ActionResult Index()
        {
            var facturas = db.facturas.Include(f => f.cliente);
            return View(facturas.ToList());
        }
        public ActionResult Index2()
        {
            var facturas = db.facturas.Include(f => f.cliente).Where(f=>f.Estado=="no pagado");

            return View(facturas.ToList());
        }

        // GET: facturas/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            factura factura = db.facturas.Find(id);
            if (factura == null)
            {
                return HttpNotFound();
            }
            return View(factura);
        }

        // GET: facturas/Create
        public ActionResult Create()
        {
            ViewBag.Id_cliente = new SelectList(db.clientes, "Id_cliente", "Nombre");
            return View();
        }

        // POST: facturas/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id_factura,Id_cliente,monto,impuesto,Monto_total,Estado")] factura factura)
        {
            if (ModelState.IsValid)
            {
                db.facturas.Add(factura);
                db.SaveChanges();
                return RedirectToAction("Index","productos_factura", new { id = factura.Id_factura });
            }

            ViewBag.Id_cliente = new SelectList(db.clientes, "Id_cliente", "Nombre", factura.Id_cliente);
            return View(factura);
        }

        // GET: facturas/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            factura factura = db.facturas.Find(id);
            if (factura == null)
            {
                return HttpNotFound();
            }
            ViewBag.Id_cliente = new SelectList(db.clientes, "Id_cliente", "Nombre", factura.Id_cliente);
            return View(factura);
        }

        // POST: facturas/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id_factura,Id_cliente,monto,impuesto,Monto_total,Estado")] factura factura)
        {
            if (ModelState.IsValid)
            {
                db.Entry(factura).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index2");
            }
            ViewBag.Id_cliente = new SelectList(db.clientes, "Id_cliente", "Nombre", factura.Id_cliente);
            return View(factura);
        }

        // GET: facturas/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            factura factura = db.facturas.Find(id);
            if (factura == null)
            {
                return HttpNotFound();
            }
            return View(factura);
        }

        // POST: facturas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            factura factura = db.facturas.Find(id);
            db.facturas.Remove(factura);
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
        public ActionResult ExportFactura()
        {
            List<cotizacione> allOrdenes = new List<cotizacione>();
            allOrdenes = db.cotizaciones.ToList();


            ReportDocument rd = new ReportDocument();
            rd.Load(Path.Combine(Server.MapPath("~/Reporte"), "FacturaF.rpt"));

            var lista = from data in db.proveedores

                        select data;

            //rd.SetDataSource(db2.FacturaCotizacion3(14).ToList());



            Response.Buffer = false;
            Response.ClearContent();
            Response.ClearHeaders();


            Stream stream = rd.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
            stream.Seek(0, SeekOrigin.Begin);
            return File(stream, "application/pdf", "FacturaF.pdf");
        }

        public ActionResult ExportFacturacion()
        {
            List<cotizacione> allOrdenes = new List<cotizacione>();
            allOrdenes = db.cotizaciones.ToList();


            ReportDocument rd = new ReportDocument();
            rd.Load(Path.Combine(Server.MapPath("~/Reporte"), "Facturaciones.rpt"));

            var lista = from data in db.proveedores

                        select data;

            //rd.SetDataSource(db2.FacturaCotizacion3(14).ToList());



            Response.Buffer = false;
            Response.ClearContent();
            Response.ClearHeaders();


            Stream stream = rd.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
            stream.Seek(0, SeekOrigin.Begin);
            return File(stream, "application/pdf", "Facturaciones.pdf");
        }

    }
}
