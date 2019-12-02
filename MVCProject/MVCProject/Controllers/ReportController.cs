using CristalReportLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVCProject.Controllers
{
    public class ReportController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult ViewReport()
        {
            CRHelperClass cRHelper = new CRHelperClass();

            CRProductReport cRProduct = new CRProductReport();
            cRProduct.SetDataSource(cRHelper.GetData());

            System.IO.Stream model = cRProduct.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
            return File(model, "application/pdf");
        }
    }
}