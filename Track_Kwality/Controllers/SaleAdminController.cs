using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Track_Kwality.Filters;
using System.Data;
using BLL;
using DAL;

namespace Track_Kwality.Controllers
{
   
    //[CustomAuthenticationFilter]
    [Authorize]
    public class SaleAdminController : BaseController
    {
        public ActionResult Quotation(string id = "")
        {
            ViewBag.Message = "Quotation";
            return View();
        }
        public async Task<ActionResult> ItemListQuotation(int page = 1, int pageSize = 4, string id = "")
        {
            SaleAdminBLL apiDal = new SaleAdminBLL();
            var products = await apiDal.GetProducts(id);
            ViewBag.patyEnqCode = id;
            if (page < 1)
                page = 1;
            var query = products.AsQueryable();
            int totalRecords = query.Count();
            var data = query.Skip((page - 1) * pageSize).Take(pageSize).ToList();
            Pager pager = new Pager(totalRecords, page, pageSize, "SaleAdmin", "ItemListQuotation");
            ViewBag.Pager = pager;
            return View(data);
        }

        public async Task<ActionResult> Inquiry_Quotation(string SearchText = "", int page = 1, int pageSize = 4, string selectedValue = "")
        {
            SaleAdminBLL apiDal = new SaleAdminBLL();
            var inquiries = await apiDal.GetInquiry(selectedValue);
            if (page < 1)
                page = 1;
            var query = inquiries.AsQueryable();
            if (!string.IsNullOrEmpty(SearchText))
            {
                query = inquiries.Where(p => p.Party_Enq_Code.Contains(SearchText)).AsQueryable();
            }
            int totalRecords = query.Count();
            var data = query.Skip((page - 1) * pageSize).Take(pageSize).ToList();
            Pager pager = new Pager(totalRecords, page, pageSize, "SaleAdmin", "Inquiry_Quotation");
            ViewBag.Pager = pager;
            return View(data);
        }

        public async Task<ActionResult> GetCorGenName(string DosForm)
        {
            var saleDAL = new SaleAdminBLL();
            var CorGenNameList = await saleDAL.GetCorGenName(DosForm);
            if (CorGenNameList.Count > 0)
            {
                return Json(CorGenNameList);
            }
            return Json(new { result = "failed" });
        }

        public async Task<ActionResult> GetPackType(string DosForm)
        {
            var saleDAL = new SaleAdminBLL();
            var packList = await saleDAL.GetPackType(DosForm);
            if (packList.Count > 0)
            {
                return Json(packList);
            }
            return Json(new { result = "failed" });
        }

        public async Task<ActionResult> GetDescription(string WorkOrder)
        {
            var saleDAL = new SaleAdminBLL();
            var description = await saleDAL.GetDescription(WorkOrder);
            if (description !=null)
            {
                return Json(description);
            }
            return Json(new { result = "failed" });
        }

        public async Task<ActionResult> GetPartyListDescription(string GenCode)
        {
            var saleDAL = new SaleAdminBLL();
            var partyList = await saleDAL.GetPartyListDescription(GenCode);
            if (partyList.Count > 0)
            {
                return Json(partyList);
            }
            return Json(new { result = "failed" });
        }

        public async Task<ActionResult> GetWOListtoAssign(string party_code,string dsg_form,string gen_code)
        {
            var Username = "SampleUser";
            var PasswordHash = "SamplePasswordHash";
            var base64Credentials = Convert.ToBase64String(Encoding.ASCII.GetBytes($"{Username}:{PasswordHash}"));
            //var logedinUser = (CustomIdentity)Session["customIdentity"];
            List<DescriptionWO> description = new List<DescriptionWO>();
             //DataTable dt = new DataTable();
            //string EnqCode = "";
            using (var client = new HttpClient())
            {
                var apiUrl = "https://103.80.32.71/testapi/Demo/Get_DataByGenCodeLIst?party_code=" + party_code + "&dsg_form=" + dsg_form + "&gen_code=" + gen_code;

                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Basic", base64Credentials);
                ServicePointManager.ServerCertificateValidationCallback += (sender, certificate, chain, sslPolicyErrors) => true;


                // Send the GET request to the API
                var response = await client.GetAsync(apiUrl);

                if (response.IsSuccessStatusCode)
                {
                    //Storing the response details recieved from web api   
                    var cost = await response.Content.ReadAsStringAsync();

                    //Deserializing the response recieved from web api and storing into the Employee list  
                    //description = JsonConvert.DeserializeObject<List<DescriptionWO>>(cost);
                    // ViewBag.country = AirPortList;
                    return Json(cost);
                }
                return Json(new { result = "failed" });
            }
        }

        public async Task<ActionResult> GetCosting(string WorkOrder)
        {
            var saleDAL = new SaleAdminBLL();
            var costing = await saleDAL.GetCosting(WorkOrder);
            if (costing != null)
            {
                return Json(costing);
            }
            return Json(new { result = "failed" });
        }

        public async Task<ActionResult> UpdateItem(InquiryItemUpdate model)
        {

            var saveInquiryDAL = new SaleAdminBLL();
            var isSuccess = await saveInquiryDAL.SaveCompleteInquiry(model);
            if (isSuccess)
            {
                return Json(new { success = true, message = "Row updated successfully" });
            }
            else
            {
                return Json("Failure");
            }
        }
    }
}
