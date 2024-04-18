using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Track_Kwality.Models;

namespace Track_Kwality.Controllers
{
    public class DashboardController : BaseController
    {
        // GET: Dashboard
        public ActionResult SaleOrderDashboard()
        {
            return View();
        }

        public ActionResult PurchaseDashboard()
        {
            return View();
        }
        [Authorize]
        public ActionResult dashboard()
        {
            return View();
        }


    }
}