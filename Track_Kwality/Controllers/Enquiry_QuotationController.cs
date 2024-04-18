using Newtonsoft.Json;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Track_Kwality.Filters;
using DAL;
using BLL;


namespace Track_Kwality.Controllers
{
   
    [CustomAuthenticationFilter]
    [Authorize]
    public class Enquiry_QuotationController : BaseController
    {
        [OutputCache(Duration = 120)]
        public ActionResult OldInquiry()
        {
            return View();
        }


        public async Task<ActionResult> Inquiry()
        {
            var userID = ViewBag.UserID;           
            InquiryBLL apiDal = new InquiryBLL();
            ViewBag.PartyEnqCode = await apiDal.GenerateEnqCode(userID);
            ViewBag.country = await apiDal.GetCountryList();
            return View();
        }

        public async Task<ActionResult> InquiryList(string SearchText = "", int page = 1, int pageSize = 4, string userID = "")
        {
           
            if(userID == "")
            {
                 userID = ViewBag.UserID;
            }
            if (page < 1)
                page = 1;
            InquiryBLL apiDal = new InquiryBLL();
            var inquiries = await apiDal.GetInquiries(userID);
            var query = inquiries.AsQueryable();
            if (!string.IsNullOrEmpty(SearchText))
            {
                query = inquiries.Where(p => p.Party_Enq_Code.Contains(SearchText)).AsQueryable();
            }
            int totalRecords = query.Count();
            var data = query.Skip((page - 1) * pageSize).Take(pageSize).ToList();
            Pager pager = new Pager(totalRecords, page, pageSize, "Enquiry_Quotation", "InquiryList");
            ViewBag.Pager = pager;
            return View(data);
        }

        public async Task<ActionResult> InquiryItemList(string id = "")
        {
            InquiryBLL apiDal = new InquiryBLL();
            var products = await apiDal.GetProducts(id);
            ViewBag.patyEnqCode = id;
            return View(products);
        }
    
        public async Task<ActionResult> ExportToExcel(string id)
        {
            InquiryBLL apiDal = new InquiryBLL();
            var tableData = await apiDal.GetProducts(id);// Get your table data here (for example, from a database)


           // var tableData = await GetProducts(id);

            // Create a new Excel package
            using (var package = new ExcelPackage())
            {
                // Add a new worksheet to the package
                var worksheet = package.Workbook.Worksheets.Add("Sheet1");

                // Set the column headers
                worksheet.Cells["A1"].Value = "S/N";
                worksheet.Cells["B1"].Value = "Date";

                worksheet.Cells["C1"].Value = "Inquiry Status";
                worksheet.Cells["D1"].Value = "Item Name";

                worksheet.Cells["E1"].Value = "Dose Form";
                worksheet.Cells["F1"].Value = "Unit";

                worksheet.Cells["G1"].Value = "Size";
                worksheet.Cells["H1"].Value = "Qty. Per Pack";
                worksheet.Cells["I1"].Value = "No of Pack";
                worksheet.Cells["J1"].Value = "Total Unit";
                worksheet.Cells["K1"].Value = "Image";


                var hborder = worksheet.Cells[1, 1, 1, 11].Style.Border;
                hborder.Top.Style = hborder.Left.Style = hborder.Bottom.Style = hborder.Right.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;

                // Add more columns if needed

                // Add the table data to the worksheet
                //foreach (var item in tableData)
                //{
                //    worksheet.Cells[row, col++].Value = item.Property1;
                //    worksheet.Cells[row, col++].Value = item.Property2;
                //    // ... add other properties

                //    row++;
                //    col = 1; // Reset column for next row
                //}


                for (int i = 0; i < tableData.Count(); i++)
                {

                    worksheet.Cells[i + 2, 1].Value = i + 1;
                    worksheet.Cells[i + 2, 2].Value = tableData[i].date;
                    worksheet.Cells[i + 2, 2].Style.Numberformat.Format = "yyyy-mm-dd hh:mm:ss";
                    worksheet.Cells[i + 2, 3].Value = tableData[i].Enquiry_Status;

                    worksheet.Cells[i + 2, 4].Value = tableData[i].Party_item_name;
                    worksheet.Cells[i + 2, 5].Value = tableData[i].dsg_form;

                    worksheet.Cells[i + 2, 6].Value = tableData[i].unit;
                    worksheet.Cells[i + 2, 7].Value = tableData[i].Size;

                    worksheet.Cells[i + 2, 8].Value = tableData[i].Qtydosg_unit_perpack;
                    worksheet.Cells[i + 2, 9].Value = tableData[i].No_of_pack;

                    worksheet.Cells[i + 2, 10].Value = tableData[i].Total_unit;
                    // worksheet.Cells[i + 2, 11].Value = tableData[i].Imagedata;


                    var imageCell = worksheet.Cells[i + 2, 11];
                    imageCell.Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                    imageCell.Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.White); // Set background color to white
                    imageCell.Style.Border.BorderAround(OfficeOpenXml.Style.ExcelBorderStyle.Thin); // Add border
                    imageCell.Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                    imageCell.Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;
                    if (!string.IsNullOrEmpty(tableData[i].Imagedata))
                    {
                        byte[] imageBytes = Convert.FromBase64String(tableData[i].Imagedata);
                        var image = Image.FromStream(new MemoryStream(imageBytes));

                        //worksheet.Row(i + 2).Height = image.Height / 15;
                        var picture = worksheet.Drawings.AddPicture($"image_{i}", new MemoryStream(imageBytes));
                        picture.SetSize(100, 100);
                        picture.SetPosition(i + 1, 0, 10, 0);
                    }
                    //worksheet.Cells[i + 2, 1, i + 2, 11].Style.WrapText = true;
                    worksheet.Row(i + 2).Height = 90;
                    var border = worksheet.Cells[i + 2, 1, i + 2, 11].Style.Border;
                    border.Top.Style = border.Left.Style = border.Bottom.Style = border.Right.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;

                    worksheet.Cells[i + 2, 1, i + 2, 11].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                    worksheet.Cells[i + 2, 1, i + 2, 11].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;
                    // Add more columns if needed
                }

                // Apply wrap text to all cells in the table



                // Auto fit columns for better readability
                worksheet.Cells[worksheet.Dimension.Address].AutoFitColumns();

                // Save the Excel file
                var memoryStream = new MemoryStream();
                package.SaveAs(memoryStream);
                memoryStream.Position = 0;

                // Set the response content type
                Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                // Set the content disposition header
                Response.AddHeader("content-disposition", "attachment;  filename=Inquiry_Item_List.xlsx");

                // Write the Excel file to the response
                memoryStream.CopyTo(Response.OutputStream);

                // Return the result
                return new EmptyResult();
            }
        }

        public async Task<ActionResult> GetAirPort(string country)
        {
            var airportDAL = new InquiryBLL();
            var airportList = await airportDAL.GetAirportsByCountry(country);
            if (airportList.Count > 0)
            {
                return Json(airportList);
            }
            return Json(new { result = "failed" });
        }

        public async Task<ActionResult> GetSeaPort(string country)
        {
            var seaportDAL = new InquiryBLL();
            var seaportList = await seaportDAL.GetSeaportsByCountry(country);
            if (seaportList.Count > 0)
            {
                return Json(seaportList);
            }
            return Json(new { result = "failed" });
        }

        public async Task<ActionResult> GetEnqCode()
        {
            var userID = ViewBag.UserID;
            InquiryBLL apiDal = new InquiryBLL();
            var EnqCode = await apiDal.GetEnqCode(userID);
            if (!string.IsNullOrEmpty(EnqCode))
            {
                return Json(EnqCode);
            }
            return Json(new { result = "failed" });
        }

        public async Task<ActionResult> GetUnits()
        {
            var unitDAL = new InquiryBLL();
            var unitList = await unitDAL.GetUnitList();
            if (unitList.Count > 0)
            {
                return Json(unitList);
            }
            return Json(new { result = "failed" });
        }
        
        public async Task<ActionResult> GetDoseForm()
        {
            var doseDAL = new InquiryBLL();
            var doseList = await doseDAL.GetDoseList();
            if (doseList.Count > 0)
            {
                return Json(doseList);
            }
            return Json(new { result = "failed" });
        }


        public async Task<ActionResult> GetEnqCodeList()
        {
            var userID = ViewBag.UserID;
            var inquiryDAL = new InquiryBLL();
            var inquiryCodeList = await inquiryDAL.GetInquiryCodeList(userID);
            if (inquiryCodeList.Count > 0)
            {
                return Json(inquiryCodeList);
            }
            return Json(new { result = "failed" });
        }

        public async Task<ActionResult> OldInquiryList()
        {
            var userID = ViewBag.UserID;
            var inquiryDAL = new InquiryBLL();
            var inquiryOldList = await inquiryDAL.GetOldInquiryList(userID);
            if (inquiryOldList.Count > 0)
            {
                return Json(inquiryOldList);
            }
            return Json(new { result = "failed" });
        }

        public async Task<ActionResult> OldInquiryPendingList()
        {
            var userID = ViewBag.UserID;
            var inquiryDAL = new InquiryBLL();
            var inquiryPendingList = await inquiryDAL.GetPendingInquiryList(userID);
            if (inquiryPendingList.Count > 0)
            {
                return Json(inquiryPendingList);
            }
            return Json(new { result = "failed" });
        }

        public async Task<ActionResult> OldInquiryApprovedList()
        {
            var userID = ViewBag.UserID;
            var inquiryDAL = new InquiryBLL();
            var inquiryApprovedList = await inquiryDAL.GetApprovedInquiryList(userID);
            if (inquiryApprovedList.Count > 0)
            {
                return Json(inquiryApprovedList);
            }
            return Json(new { result = "failed" });
        }


        public async Task<ActionResult> OldInquiryCodeWiseList(string inquiryCode)
        {
            var inquiryDAL = new InquiryBLL();
            var inquiryCodeWiseList = await inquiryDAL.GetInqCodeWiseInquiryList(inquiryCode);
            if (inquiryCodeWiseList.Count > 0)
            {
                return Json(inquiryCodeWiseList);
            }
            return Json(new { result = "failed" });
        }

        public async Task<ActionResult> OldInquiryDosformWiseList(string dosageForm)
        {
            var userID = ViewBag.UserID;
            var inquiryDAL = new InquiryBLL();
            var inquiryDoseWiseList = await inquiryDAL.GetDoseWiseInquiryList(dosageForm, userID);
            if (inquiryDoseWiseList.Count > 0)
            {
                return Json(inquiryDoseWiseList);
            }
            return Json(new { result = "failed" });
        }

        public async Task<ActionResult> OldInquiryDateWiseList(string startDate,string endDate)
        {
            var userID = ViewBag.UserID;
            var inquiryDAL = new InquiryBLL();
            var inquiryDateWiseList = await inquiryDAL.GetDateWiseInquiryList(startDate, endDate, userID);
            if (inquiryDateWiseList.Count>0)
            {
                return Json(inquiryDateWiseList);
            }
            return Json(new { result = "failed" });
        }

        [HttpPost]
        public async Task<ActionResult> GenerateInquiryFirst(GenInqFirst model)
        {
            var saveFirstDAL = new InquiryBLL();
            var isSuccess = await saveFirstDAL.GenerateInquiryFirst(model);
            if (isSuccess)
            {
                return Json("Success");
            }
            else
            {
                return Json("Failure");
            }
        }

        [HttpPost]
        public async Task<ActionResult> GenerateInquirySecond(List<PlaceInquiry> model)
        {
            var saveSecondDAL = new InquiryBLL();
            var result = await saveSecondDAL.GenerateInquirySecond(model);
            if (result > 0)
            {
                return Json(result);
            }
            else
            {
                return Json("Failure");
            }
        }
    }
}