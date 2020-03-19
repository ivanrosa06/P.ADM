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
    public class ordenesComprasController : Controller
    {
        private ADMEntities db = new ADMEntities();

        // GET: ordenesCompras
        public ActionResult Index()
        {
            var ordenesCompras = db.ordenesCompras.Include(o => o.proveedore);
            return View(ordenesCompras.ToList());
        }
        public ActionResult Index2()
        {
            var ordenesCompras = db.ordenesCompras.Include(o => o.proveedore).Where(f=>f.Estado==0);
            return View(ordenesCompras.ToList());
        }
        // GET: ordenesCompras/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ordenesCompra ordenesCompra = db.ordenesCompras.Find(id);
            if (ordenesCompra == null)
            {
                return HttpNotFound();
            }
            return View(ordenesCompra);
        }

        // GET: ordenesCompras/Create
        public ActionResult Create()
        {
            ViewBag.Id_proveedor = new SelectList(db.proveedores, "Id_proveedor", "Nombre");
            return View();
        }

        // POST: ordenesCompras/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id_Orden_compra,Id_proveedor,Estado,Monto,Impuesto,Monto_total")] ordenesCompra ordenesCompra,Models.parametros par)
        {
           
          
            if (ModelState.IsValid)
            {
                db.ordenesCompras.Add(ordenesCompra);
                db.SaveChanges();
                par.Orden = ordenesCompra.Id_Orden_compra;
                Console.WriteLine(par.Orden);
                return RedirectToAction("Index", "productos_OrdenesCompra");
               
            }

            ViewBag.Id_proveedor = new SelectList(db.proveedores, "Id_proveedor", "Nombre", ordenesCompra.Id_proveedor);
            return View(ordenesCompra);
        }

        // GET: ordenesCompras/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ordenesCompra ordenesCompra = db.ordenesCompras.Find(id);
            if (ordenesCompra == null)
            {
                return HttpNotFound();
            }
            ViewBag.Id_proveedor = new SelectList(db.proveedores, "Id_proveedor", "Nombre", ordenesCompra.Id_proveedor);
            return View(ordenesCompra);
        }

        // POST: ordenesCompras/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id_Orden_compra,Id_proveedor,Estado,Monto,Impuesto,Monto_total")] ordenesCompra ordenesCompra)
        {
            if (ModelState.IsValid)
            {
                db.Entry(ordenesCompra).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Id_proveedor = new SelectList(db.proveedores, "Id_proveedor", "Nombre", ordenesCompra.Id_proveedor);
            return View(ordenesCompra);
        }

        // GET: ordenesCompras/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ordenesCompra ordenesCompra = db.ordenesCompras.Find(id);
            if (ordenesCompra == null)
            {
                return HttpNotFound();
            }
            return View(ordenesCompra);
        }

        // POST: ordenesCompras/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ordenesCompra ordenesCompra = db.ordenesCompras.Find(id);
            db.ordenesCompras.Remove(ordenesCompra);
            db.SaveChanges();
            return RedirectToAction("Index");
        }


        public ActionResult ExportOrdenes()
        {
            List<ordenesCompra> allOrdenes = new List<ordenesCompra>();
            allOrdenes = db.ordenesCompras.ToList();


            ReportDocument rd = new ReportDocument();
            rd.Load(Path.Combine(Server.MapPath("~/Reporte"), "OrdenesReport.rpt"));

            var lista = from data in db.ordenesCompras

                        select data;

            //rd.SetDataSource(lista.ToList());



            Response.Buffer = false;
            Response.ClearContent();
            Response.ClearHeaders();

         
            Stream stream = rd.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
            stream.Seek(0, SeekOrigin.Begin);
            return File(stream, "application/pdf", "OrdenesReport.pdf");
        }


        public ActionResult ExportProveedores()
        {
            List<proveedore> allOrdenes = new List<proveedore>();
            allOrdenes = db.proveedores.ToList();


            ReportDocument rd = new ReportDocument();
            rd.Load(Path.Combine(Server.MapPath("~/Reporte"), "ProveedoresReport.rpt"));

            var lista = from data in db.proveedores

                        select data;

            rd.SetDataSource(lista.ToList());



            Response.Buffer = false;
            Response.ClearContent();
            Response.ClearHeaders();


            Stream stream = rd.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
            stream.Seek(0, SeekOrigin.Begin);
            return File(stream, "application/pdf", "ProveedoresReport.pdf");
        }

        public ActionResult ExportFactura()
        {
            List<cotizacione> allOrdenes = new List<cotizacione>();
            allOrdenes = db.cotizaciones.ToList();


            ReportDocument rd = new ReportDocument();
            rd.Load(Path.Combine(Server.MapPath("~/Reporte"), "OrdenesCompraFactura.rpt"));

            var lista = from data in db.proveedores

                        select data;

            //rd.SetDataSource(db2.FacturaCotizacion3(14).ToList());



            Response.Buffer = false;
            Response.ClearContent();
            Response.ClearHeaders();


            Stream stream = rd.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
            stream.Seek(0, SeekOrigin.Begin);
            return File(stream, "application/pdf", "OrdenCompraFactura.pdf");
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
