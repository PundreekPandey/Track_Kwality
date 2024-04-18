using DAL;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class SaleAdminBLL
    {
        private const string Username = "SampleUser";
        private const string PasswordHash = "SamplePasswordHash";
        private const string BaseUrl = "https://103.80.32.71/testapi/";
        public async Task<List<ItemQuotation>> GetProducts(string id)
        {
            var base64Credentials = Convert.ToBase64String(Encoding.ASCII.GetBytes($"{Username}:{PasswordHash}"));
            List<ItemQuotation> products = new List<ItemQuotation>();

            using (var client = new HttpClient())
            {
                var apiUrl = $"{BaseUrl}Demo/Get_DataByPartyEnqList?PartyEnqCode={id}";

                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Basic", base64Credentials);
                ServicePointManager.ServerCertificateValidationCallback += (sender, certificate, chain, sslPolicyErrors) => true;

                var response = await client.GetAsync(apiUrl);

                if (response.IsSuccessStatusCode)
                {
                    var productData = await response.Content.ReadAsStringAsync();
                    products = JsonConvert.DeserializeObject<List<ItemQuotation>>(productData);
                }
            }

            foreach (var item in products)
            {
                if (item.Product_Image != Array.Empty<byte>())
                {
                    item.Imagedata = Convert.ToBase64String(item.Product_Image);
                }
            }

            return products;
        }

        public async Task<List<Inquiry>> GetInquiry(string selectedValue)
        {
            var base64Credentials = Convert.ToBase64String(Encoding.ASCII.GetBytes($"{Username}:{PasswordHash}"));
            List<Inquiry> inquiries = new List<Inquiry>();
            using (var client = new HttpClient())
            {
                var apiUrl = "";
                if (selectedValue == "All" || selectedValue == "")
                {
                    apiUrl = $"{BaseUrl}Demo/AllEnquryList";
                }
                else if (selectedValue == "Approved")
                {
                    apiUrl = $"{BaseUrl}Demo/ApprovedList";
                }
                else if (selectedValue == "Pending")
                {
                    apiUrl = $"{BaseUrl}Demo/PendingList";
                }
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", base64Credentials);
                ServicePointManager.ServerCertificateValidationCallback += (sender, certificate, chain, sslPolicyErrors) => true;

                var response = await client.GetAsync(apiUrl);

                if (response.IsSuccessStatusCode)
                {
                    var productData = await response.Content.ReadAsStringAsync();
                    inquiries = JsonConvert.DeserializeObject<List<Inquiry>>(productData);
                }
            }
            return inquiries;

        }

        public async Task<List<CorGenName>> GetCorGenName(string DosForm)
        {
            var base64Credentials = Convert.ToBase64String(Encoding.ASCII.GetBytes($"{Username}:{PasswordHash}"));
            var CorGenNameList = new List<CorGenName>();

            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", base64Credentials);
                ServicePointManager.ServerCertificateValidationCallback += (sender, certificate, chain, sslPolicyErrors) => true;

                var apiUrl = $"{BaseUrl}Demo/GetDataByCorGenNameList?CorGenName={DosForm}";
                var response = await client.GetAsync(apiUrl);

                if (response.IsSuccessStatusCode)
                {
                    var corGentData = await response.Content.ReadAsStringAsync();
                    CorGenNameList = JsonConvert.DeserializeObject<List<CorGenName>>(corGentData);
                }
            }

            return CorGenNameList;
        }


        public async Task<List<PackType>> GetPackType(string DosForm)
        {
            var base64Credentials = Convert.ToBase64String(Encoding.ASCII.GetBytes($"{Username}:{PasswordHash}"));
            var PackList = new List<PackType>();

            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", base64Credentials);
                ServicePointManager.ServerCertificateValidationCallback += (sender, certificate, chain, sslPolicyErrors) => true;

                var apiUrl = $"{BaseUrl}Demo/GetDataByPackingTypeList?DsgForm={DosForm}";
                var response = await client.GetAsync(apiUrl);

                if (response.IsSuccessStatusCode)
                {
                    var corGentData = await response.Content.ReadAsStringAsync();
                    PackList = JsonConvert.DeserializeObject<List<PackType>>(corGentData);
                }
            }

            return PackList;
        }

        public async Task<AssignDescription> GetDescription(string WorkOrder)
        {
            var base64Credentials = Convert.ToBase64String(Encoding.ASCII.GetBytes($"{Username}:{PasswordHash}"));
            AssignDescription description = new AssignDescription();

            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", base64Credentials);
                ServicePointManager.ServerCertificateValidationCallback += (sender, certificate, chain, sslPolicyErrors) => true;

                var apiUrl = $"{BaseUrl}Demo/PackingListByWorkOrder?Work={WorkOrder}";
                var response = await client.GetAsync(apiUrl);

                if (response.IsSuccessStatusCode)
                {
                    var desData = await response.Content.ReadAsStringAsync();
                    description = JsonConvert.DeserializeObject<AssignDescription>(desData);
                }
            }

            return description;
        }

        public async Task<List<PartyListforDescription>> GetPartyListDescription(string GenCode)
        {
            var base64Credentials = Convert.ToBase64String(Encoding.ASCII.GetBytes($"{Username}:{PasswordHash}"));
            var PartyList = new List<PartyListforDescription>();
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", base64Credentials);
                ServicePointManager.ServerCertificateValidationCallback += (sender, certificate, chain, sslPolicyErrors) => true;
                var apiUrl = $"{BaseUrl}Demo/GetDataBygencodes?gencode={GenCode}";
                var response = await client.GetAsync(apiUrl);
                if (response.IsSuccessStatusCode)
                {
                    var partyData = await response.Content.ReadAsStringAsync();
                    PartyList = JsonConvert.DeserializeObject<List<PartyListforDescription>>(partyData);
                }
            }
            return PartyList;
        }

        public async Task<CostingResult> GetCosting(string WorkOrder)
        {
            var base64Credentials = Convert.ToBase64String(Encoding.ASCII.GetBytes($"{Username}:{PasswordHash}"));
            CostingResult costing = new CostingResult();
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", base64Credentials);
                ServicePointManager.ServerCertificateValidationCallback += (sender, certificate, chain, sslPolicyErrors) => true;
                var apiUrl = $"{BaseUrl}Demo/Costing?WorkOrder={WorkOrder}";
                var response = await client.GetAsync(apiUrl);
                if (response.IsSuccessStatusCode)
                {
                    var costingData = await response.Content.ReadAsStringAsync();
                    costing = JsonConvert.DeserializeObject<CostingResult>(costingData);
                }
            }
            return costing;
        }

        public async Task<bool> SaveCompleteInquiry(InquiryItemUpdate model)
        {
            var base64Credentials = Convert.ToBase64String(Encoding.ASCII.GetBytes($"{Username}:{PasswordHash}"));
            string json = JsonConvert.SerializeObject(model);
            using (var client = new HttpClient())
            {
                var apiUrl = $"{BaseUrl}Demo/UpdateInquiryItem";
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", base64Credentials);
                ServicePointManager.ServerCertificateValidationCallback += (sender, certificate, chain, sslPolicyErrors) => true;

                var method = new HttpMethod("PATCH");
                var request = new HttpRequestMessage(method, apiUrl)
                {
                    Content = content
                };
                var response = await client.SendAsync(request);
                return response.IsSuccessStatusCode;
            }
        }



    }
}
