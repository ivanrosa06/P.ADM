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
    

    public class HomeController : Controller
    {
        private ADMEntities db = new ADMEntities();
        public ActionResult IndexFacturacion()
        {
            return View();
        }
        public ActionResult IndexOrdenCompra()
        {
            return View();
        }
        public ActionResult IndexReporteFacturacion()
        {
            return View();
        }
        public ActionResult IndexReporteOrdenCompra()
        {
            return View();
        }
        public ActionResult IndexContabilidad()
        {
            return View();
        }
        public ActionResult IndexReportesContabilidad()
        {
            return View();
        }
        public ActionResult IndexReportesCotizacion()
        {
            
            return View();
        }
    
        public ActionResult IndexCotizacion()
        {
            //db.Database.ExecuteSqlCommand("DELETE FROM productos_Cotizaciones WHERE Id_producto_cotizacion != 200");
            //db.Database.ExecuteSqlCommand("DELETE FROM cotizaciones WHERE Id_cotizacion != 200");
            return View();
        }
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}