using Track_Kwality.Filters;
using BLL;
using System.Threading.Tasks;
using System.Web.Mvc;


namespace Track_Kwality.Controllers
{
    [CustomAuthenticationFilter]
    [Authorize]
    public class OrderStatusController : BaseController
    {
        // GET: OrderStatus
        [OutputCache(Duration = 120)]
        public ActionResult Order_Status()
        {
            return View();
        }

        public async Task<ActionResult> GetByGO(string GroupOrder)
        {
            var userID = ViewBag.UserID;
            var doseDAL = new OrderStatusBLL();
            var doseList = await doseDAL.GetByGO(GroupOrder, userID);
            if (doseList.Count > 0)
            {
                return Json(doseList);
            }
            return Json(new { result = "failed" });
        }

        public async Task<ActionResult> GetByWO(string WorkOrder)
        {
            var userID = ViewBag.UserID;
            var doseDAL = new OrderStatusBLL();
            var doseList = await doseDAL.GetByWO(WorkOrder, userID);
            if (doseList.Count > 0)
            {
                return Json(doseList);
            }
            return Json(new { result = "failed" });
        }

        public async Task<ActionResult> GetByBatch(string batch)
        {
            var userID = ViewBag.UserID;
            var doseDAL = new OrderStatusBLL();
            var doseList = await doseDAL.GetByBatch(batch, userID);
            if (doseList.Count > 0)
            {
                return Json(doseList);
            }
            return Json(new { result = "failed" });
        }

        public async Task<ActionResult> GetByCombi(string combi)
        {
            var userID = ViewBag.UserID;
            var doseDAL = new OrderStatusBLL();
            var doseList = await doseDAL.GetByCombi(combi, userID);
            if (doseList.Count > 0)
            {
                return Json(doseList);
            }
            return Json(new { result = "failed" });
        }

        public async Task<ActionResult> GetItemList(string Section)
        {
            var doseDAL = new OrderStatusBLL();
            var doseList = await doseDAL.GetItemList(Section);
            if (doseList.Count > 0)
            {
                return Json(doseList);
            }
            return Json(new { result = "failed" });
        }

        public async Task<ActionResult> GetBySectionParty(string dsgform)
        {
            var userID = ViewBag.UserID;
            var doseDAL = new OrderStatusBLL();
            var doseList = await doseDAL.GetBySectionParty(dsgform, userID);
            if (doseList.Count > 0)
            {
                return Json(doseList);
            }
            return Json(new { result = "failed" });
        }

        public async Task<ActionResult> GetBySectionItemParty(string corrgrn, string dsgform)
        {
            var userID = ViewBag.UserID;
            var doseDAL = new OrderStatusBLL();
            var doseList = await doseDAL.GetBySectionItemParty(corrgrn, dsgform,userID);
            if (doseList.Count > 0)
            {
                return Json(doseList);
            }
            return Json(new { result = "failed" });
        }

        public async Task<ActionResult> GetByDateWiseSearch(string FromDate, string ToDate)
        {
            var userID = ViewBag.UserID;
            var doseDAL = new OrderStatusBLL();
            var doseList = await doseDAL.GetByDateWise(FromDate, ToDate, userID);
            if (doseList.Count > 0)
            {
                return Json(doseList);
            }
            return Json(new { result = "failed" });
        }

        public async Task<ActionResult> GetBySectionItemPartyDateWiseSearch(string section, string item, string FromDate, string ToDate)
        {
            var userID = ViewBag.UserID;
            var doseDAL = new OrderStatusBLL();
            var doseList = await doseDAL.GetBySecItemDateWise(section, item,FromDate, ToDate, userID);
            if (doseList.Count > 0)
            {
                return Json(doseList);
            }
            return Json(new { result = "failed" });
        }

        public async Task<ActionResult> DesignerStatus(string Work_order)
        {
            var doseDAL = new OrderStatusBLL();
            var doseList = await doseDAL.DesignerStats(Work_order);
            if (doseList.Count > 0)
            {
                return Json(doseList);
            }
            return Json(new { result = "failed" });
        }

        public async Task<ActionResult> TestingStatus(string Work_order)
        {
            var doseDAL = new OrderStatusBLL();
            var doseList = await doseDAL.TestingStats(Work_order);
            if (doseList == null)
            {
                return Json(new { result = "failed" });
            }
            if (doseList.Count > 0 || doseList !=null)
            {
                return Json(doseList);
            }
            return Json(new { result = "failed" });

        }

        public async Task<ActionResult> RMStatus(string Work_order)
        {
            var doseDAL = new OrderStatusBLL();
            var doseList = await doseDAL.RMStats(Work_order);
            if (doseList.Count > 0)
            {
                return Json(doseList);
            }
            return Json(new { result = "failed" });
        }

        public async Task<ActionResult> PMStatus(string PMWorkorder)
        {
            var doseDAL = new OrderStatusBLL();
            var doseList = await doseDAL.PMStats(PMWorkorder);
            if (doseList.Count > 0)
            {
                return Json(doseList);
            }
            return Json(new { result = "failed" });
        }

        public async Task<ActionResult> ProdStatus(string ProdWorkorder)
        {
            var doseDAL = new OrderStatusBLL();
            var doseList = await doseDAL.ProdStats(ProdWorkorder);
            if (doseList.Count > 0)
            {
                return Json(doseList);
            }
            return Json(new { result = "failed" });
        }

        public async Task<ActionResult> PackingStatus(string PackSWorkorder)
        {
            var doseDAL = new OrderStatusBLL();
            var doseList = await doseDAL.PackStats(PackSWorkorder);
            if (doseList.Count > 0)
            {
                return Json(doseList);
            }
            return Json(new { result = "failed" });
        }

        public async Task<ActionResult> InvoiceStatus(string InvoiceWorkorder)
        {
            var doseDAL = new OrderStatusBLL();
            var doseList = await doseDAL.InvoiceStats(InvoiceWorkorder);
            if (doseList.Count > 0)
            {
                return Json(doseList);
            }
            return Json(new { result = "failed" });
        }

        public async Task<ActionResult> PaymentStatus(string PaymentWorkorder)
        {
            var doseDAL = new OrderStatusBLL();
            var doseList = await doseDAL.PaymentStats(PaymentWorkorder);
            if (doseList.Count > 0)
            {
                return Json(doseList);
            }
            return Json(new { result = "failed" });
        }

        public async Task<ActionResult> DisplayPartyName()
        {
            var userID = ViewBag.UserID;
            var doseDAL = new OrderStatusBLL();
            var doseList = await doseDAL.PartyDetails(userID);
            if (doseList.Count > 0)
            {
                return Json(doseList);
            }
            return Json(new { result = "failed" });
        }
    }
}