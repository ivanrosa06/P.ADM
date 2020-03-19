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
    public class contabilidadsController : Controller
    {
        private ADMEntities db = new ADMEntities();

        // GET: contabilidads
        public ActionResult Index()
        {
            var EActivos = from data in db.facturas where data.Estado == "Pagado" select data;
            var nomina = EActivos.Sum(item => item.Monto_total);
            ViewBag.Ingresos = nomina;
            var EActivos2 = from data in db.ordenesCompras where data.Estado == 1 select data;
            var nomina2 = EActivos2.Sum(item => item.Monto_total);
            ViewBag.Gastos = nomina2;
            return View(db.contabilidads.ToList());
        }
        public ActionResult IndexGastos()
        {
        
            return View(db.ordenesCompras.ToList());
        }
        public ActionResult IndexIngresos()
        {

            return View(db.facturas.ToList());
        }

        // GET: contabilidads/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            contabilidad contabilidad = db.contabilidads.Find(id);
            if (contabilidad == null)
            {
                return HttpNotFound();
            }
            return View(contabilidad);
        }

        // GET: contabilidads/Create
        public ActionResult Create()
        {
            var EActivos = from data in db.facturas where data.Estado == "Pagado" select data;
            var nomina = EActivos.Sum(item => item.Monto_total);
            ViewBag.Ingresos = nomina;
            var EActivos2 = from data in db.ordenesCompras where data.Estado == 1 select data;
            var nomina2 = EActivos2.Sum(item => item.Monto_total);
            ViewBag.Gastos = nomina2;
            return View();
        }

        // POST: contabilidads/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id_Contabilidad,Gastos,Ingresos,Fecha")] contabilidad contabilidad)
        {
           
            if (ModelState.IsValid)
            {
                db.contabilidads.Add(contabilidad);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(contabilidad);
        }

        // GET: contabilidads/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            contabilidad contabilidad = db.contabilidads.Find(id);
            if (contabilidad == null)
            {
                return HttpNotFound();
            }
            return View(contabilidad);
        }

        // POST: contabilidads/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id_Contabilidad,Gastos,Ingresos,Fecha")] contabilidad contabilidad)
        {
            if (ModelState.IsValid)
            {
                db.Entry(contabilidad).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(contabilidad);
        }

        // GET: contabilidads/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            contabilidad contabilidad = db.contabilidads.Find(id);
            if (contabilidad == null)
            {
                return HttpNotFound();
            }
            return View(contabilidad);
        }

        // POST: contabilidads/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            contabilidad contabilidad = db.contabilidads.Find(id);
            db.contabilidads.Remove(contabilidad);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult ExportGastos()
        {
            List<cotizacione> allOrdenes = new List<cotizacione>();
            allOrdenes = db.cotizaciones.ToList();


            ReportDocument rd = new ReportDocument();
            rd.Load(Path.Combine(Server.MapPath("~/Reporte"), "GastosReport.rpt"));

            var lista = from data in db.ordenesCompras

                        select data;

            //rd.SetDataSource(lista.ToList());



            Response.Buffer = false;
            Response.ClearContent();
            Response.ClearHeaders();


            Stream stream = rd.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
            stream.Seek(0, SeekOrigin.Begin);
            return File(stream, "application/pdf", "GastosReport.pdf");
        }

        public ActionResult ExportIngresos()
        {
            List<factura> allOrdenes = new List<factura>();
            allOrdenes = db.facturas.ToList();


            ReportDocument rd = new ReportDocument();
            rd.Load(Path.Combine(Server.MapPath("~/Reporte"), "IngresasReport.rpt"));

            var lista = from data in db.facturas

                        select data;

            //rd.SetDataSource(lista.ToList());



            Response.Buffer = false;
            Response.ClearContent();
            Response.ClearHeaders();


            Stream stream = rd.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
            stream.Seek(0, SeekOrigin.Begin);
            return File(stream, "application/pdf", "IngresasReport.pdf");
        }

        public ActionResult ExportContabilidad()
        {
            List<contabilidad> allOrdenes = new List<contabilidad>();
            allOrdenes = db.contabilidads.ToList();


            ReportDocument rd = new ReportDocument();
            rd.Load(Path.Combine(Server.MapPath("~/Reporte"), "ContabilidadReport.rpt"));

            var lista = from data in db.contabilidads

                        select data;

            //rd.SetDataSource(lista.ToList());



            Response.Buffer = false;
            Response.ClearContent();
            Response.ClearHeaders();


            Stream stream = rd.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
            stream.Seek(0, SeekOrigin.Begin);
            return File(stream, "application/pdf", "ContabilidadReport.pdf");
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
