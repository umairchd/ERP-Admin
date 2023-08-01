﻿using System.Web.Mvc;

namespace ERP.Web.Controllers
{
    [Authorize(Roles = "Admin, Report")]
    public class ReportsController : Controller
    {
        // GET: Reports
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Sales()
        {
            return View();
        }
        public ActionResult Purchases()
        {
            return View();
        }
        public ActionResult Stocks()
        {
            return View();
        }
        public ActionResult Expenses()
        {
            return View();
        }
    }
}
