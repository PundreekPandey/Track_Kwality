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
    public class OrderStatusBLL
    {
        private const string Username = "SampleUser";
        private const string PasswordHash = "SamplePasswordHash";
        private const string BaseUrl = "https://103.80.32.71/testapi/";

        public async Task<List<GroupList>> GetByGO(string GroupOrder,string userID)
        {
            var base64Credentials = Convert.ToBase64String(Encoding.ASCII.GetBytes($"{Username}:{PasswordHash}"));
            var inquiryList = new List<GroupList>();
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", base64Credentials);
                ServicePointManager.ServerCertificateValidationCallback += (sender, certificate, chain, sslPolicyErrors) => true;
                var apiUrl = $"{BaseUrl}Demo/GetGroupSearchByPartyList?partyCode={userID}&groupcode={GroupOrder}";
                var response = await client.GetAsync(apiUrl);
                if (response.IsSuccessStatusCode)
                {
                    var inquiryData = await response.Content.ReadAsStringAsync();
                    inquiryList = JsonConvert.DeserializeObject<List<GroupList>>(inquiryData);
                }
            }
            return inquiryList;
        }

        public async Task<List<workList>> GetByWO(string WorkOrder, string userID)
        {
            var base64Credentials = Convert.ToBase64String(Encoding.ASCII.GetBytes($"{Username}:{PasswordHash}"));
            var inquiryList = new List<workList>();
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", base64Credentials);
                ServicePointManager.ServerCertificateValidationCallback += (sender, certificate, chain, sslPolicyErrors) => true;
                var apiUrl = $"{BaseUrl}Demo/GetWorkOrderSearchByPartyList?partyCode={userID}&workOrderNo={WorkOrder}";
                var response = await client.GetAsync(apiUrl);
                if (response.IsSuccessStatusCode)
                {
                    var inquiryData = await response.Content.ReadAsStringAsync();
                    inquiryList = JsonConvert.DeserializeObject<List<workList>>(inquiryData);
                }
            }
            return inquiryList;
        }

        public async Task<List<workList>> GetByBatch(string batch, string userID)
        {
            var base64Credentials = Convert.ToBase64String(Encoding.ASCII.GetBytes($"{Username}:{PasswordHash}"));
            var inquiryList = new List<workList>();
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", base64Credentials);
                ServicePointManager.ServerCertificateValidationCallback += (sender, certificate, chain, sslPolicyErrors) => true;
                var apiUrl = $"{BaseUrl}Demo/GetBatchNumberByPartyList?partycod={userID}&batchno={batch}";
                var response = await client.GetAsync(apiUrl);
                if (response.IsSuccessStatusCode)
                {
                    var inquiryData = await response.Content.ReadAsStringAsync();
                    inquiryList = JsonConvert.DeserializeObject<List<workList>>(inquiryData);
                }
            }
            return inquiryList;
        }


        public async Task<List<workList>> GetByCombi(string combi, string userID)
        {
            var base64Credentials = Convert.ToBase64String(Encoding.ASCII.GetBytes($"{Username}:{PasswordHash}"));
            var inquiryList = new List<workList>();
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", base64Credentials);
                ServicePointManager.ServerCertificateValidationCallback += (sender, certificate, chain, sslPolicyErrors) => true;
                var apiUrl = $"{BaseUrl}Demo/GetCombiPartyList?Party={userID}&Combi={combi}";
                var response = await client.GetAsync(apiUrl);
                if (response.IsSuccessStatusCode)
                {
                    var inquiryData = await response.Content.ReadAsStringAsync();
                    inquiryList = JsonConvert.DeserializeObject<List<workList>>(inquiryData);
                }
            }
            return inquiryList;
        }

        public async Task<List<ItemList>> GetItemList(string Section)
        {
            var base64Credentials = Convert.ToBase64String(Encoding.ASCII.GetBytes($"{Username}:{PasswordHash}"));
            var inquiryList = new List<ItemList>();
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", base64Credentials);
                ServicePointManager.ServerCertificateValidationCallback += (sender, certificate, chain, sslPolicyErrors) => true;
                var apiUrl = $"{BaseUrl}Demo/GetItemName?drugDosageForm={Section}";
                var response = await client.GetAsync(apiUrl);
                if (response.IsSuccessStatusCode)
                {
                    var inquiryData = await response.Content.ReadAsStringAsync();
                    inquiryList = JsonConvert.DeserializeObject<List<ItemList>>(inquiryData);
                }
            }
            return inquiryList;
        }

        public async Task<List<workList>> GetBySectionParty(string dsgform, string userID)
        {
            var base64Credentials = Convert.ToBase64String(Encoding.ASCII.GetBytes($"{Username}:{PasswordHash}"));
            var inquiryList = new List<workList>();
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", base64Credentials);
                ServicePointManager.ServerCertificateValidationCallback += (sender, certificate, chain, sslPolicyErrors) => true;
                var apiUrl = $"{BaseUrl}Demo/Get_SectionPartyList?PartyCodes={userID}&DSG_Forms={dsgform}";
                var response = await client.GetAsync(apiUrl);
                if (response.IsSuccessStatusCode)
                {
                    var inquiryData = await response.Content.ReadAsStringAsync();
                    inquiryList = JsonConvert.DeserializeObject<List<workList>>(inquiryData);
                }
            }
            return inquiryList;
        }

        public async Task<List<workList>> GetBySectionItemParty(string corrgrn, string dsgform,string userID)
        {
            var base64Credentials = Convert.ToBase64String(Encoding.ASCII.GetBytes($"{Username}:{PasswordHash}"));
            var inquiryList = new List<workList>();
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", base64Credentials);
                ServicePointManager.ServerCertificateValidationCallback += (sender, certificate, chain, sslPolicyErrors) => true;
                var apiUrl = $"{BaseUrl}Demo/GetDataByPartywiseSectionItem?PartyCode={userID}&Cor_Gen_Name={corrgrn}&DSG_Form={dsgform}";
                var response = await client.GetAsync(apiUrl);
                if (response.IsSuccessStatusCode)
                {
                    var inquiryData = await response.Content.ReadAsStringAsync();
                    inquiryList = JsonConvert.DeserializeObject<List<workList>>(inquiryData);
                }
            }
            return inquiryList;
        }

        public async Task<List<workList>> GetByDateWise(string FromDate, string ToDate, string userID)
        {
            var base64Credentials = Convert.ToBase64String(Encoding.ASCII.GetBytes($"{Username}:{PasswordHash}"));
            var inquiryList = new List<workList>();
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", base64Credentials);
                ServicePointManager.ServerCertificateValidationCallback += (sender, certificate, chain, sslPolicyErrors) => true;
                var apiUrl = $"{BaseUrl}Demo/DateWiseSeachByParty?Party={userID}&fromDate={FromDate}&todate={ToDate}";
                var response = await client.GetAsync(apiUrl);
                if (response.IsSuccessStatusCode)
                {
                    var inquiryData = await response.Content.ReadAsStringAsync();
                    inquiryList = JsonConvert.DeserializeObject<List<workList>>(inquiryData);
                }
            }
            return inquiryList;
        }


        public async Task<List<workList>> GetBySecItemDateWise(string section,string item, string FromDate, string ToDate, string userID)
        {
            var base64Credentials = Convert.ToBase64String(Encoding.ASCII.GetBytes($"{Username}:{PasswordHash}"));
            var inquiryList = new List<workList>();
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", base64Credentials);
                ServicePointManager.ServerCertificateValidationCallback += (sender, certificate, chain, sslPolicyErrors) => true;
                var apiUrl = $"{BaseUrl}Demo/SectioItemDateWiseSeachByParty?fromDate={FromDate}&todate={ToDate}&dsg={section}&corrgen={item}&Party={userID}";
                var response = await client.GetAsync(apiUrl);
                if (response.IsSuccessStatusCode)
                {
                    var inquiryData = await response.Content.ReadAsStringAsync();
                    inquiryList = JsonConvert.DeserializeObject<List<workList>>(inquiryData);
                }
            }
            return inquiryList;
        }



        public async Task<List<Designer>> DesignerStats(string Work_order)
        {
            var base64Credentials = Convert.ToBase64String(Encoding.ASCII.GetBytes($"{Username}:{PasswordHash}"));
            var inquiryList = new List<Designer>();
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", base64Credentials);
                ServicePointManager.ServerCertificateValidationCallback += (sender, certificate, chain, sslPolicyErrors) => true;
                var apiUrl = $"{BaseUrl}Demo/DesignerStatusByWorkOrder?workOrder={Work_order}";
                var response = await client.GetAsync(apiUrl);
                if (response.IsSuccessStatusCode)
                {
                    var inquiryData = await response.Content.ReadAsStringAsync();
                    inquiryList = JsonConvert.DeserializeObject<List<Designer>>(inquiryData);
                }
            }
            return inquiryList;
        }

        public async Task<List<Testing>> TestingStats(string Work_order)
        {
            var base64Credentials = Convert.ToBase64String(Encoding.ASCII.GetBytes($"{Username}:{PasswordHash}"));
            var inquiryList = new List<Testing>();
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", base64Credentials);
                ServicePointManager.ServerCertificateValidationCallback += (sender, certificate, chain, sslPolicyErrors) => true;
                var apiUrl = $"{BaseUrl}Demo/TestingStatusByWorkOrder?workorder={Work_order}";
                var response = await client.GetAsync(apiUrl);
                if (response.IsSuccessStatusCode)
                {
                    var inquiryData = await response.Content.ReadAsStringAsync();
                    inquiryList = JsonConvert.DeserializeObject<List<Testing>>(inquiryData);
                }
            }
            return inquiryList;
        }
        public async Task<List<RM>> RMStats(string Work_order)
        {
            var base64Credentials = Convert.ToBase64String(Encoding.ASCII.GetBytes($"{Username}:{PasswordHash}"));
            var inquiryList = new List<RM>();
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", base64Credentials);
                ServicePointManager.ServerCertificateValidationCallback += (sender, certificate, chain, sslPolicyErrors) => true;
                var apiUrl = $"{BaseUrl}Demo/Get_RMStatusList?Work_order_number={Work_order}";
                var response = await client.GetAsync(apiUrl);
                if (response.IsSuccessStatusCode)
                {
                    var inquiryData = await response.Content.ReadAsStringAsync();
                    inquiryList = JsonConvert.DeserializeObject<List<RM>>(inquiryData);
                }
            }
            return inquiryList;
        }


        public async Task<List<PM>> PMStats(string Work_order)
        {
            var base64Credentials = Convert.ToBase64String(Encoding.ASCII.GetBytes($"{Username}:{PasswordHash}"));
            var inquiryList = new List<PM>();
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", base64Credentials);
                ServicePointManager.ServerCertificateValidationCallback += (sender, certificate, chain, sslPolicyErrors) => true;
                var apiUrl = $"{BaseUrl}Demo/Get_PMStatusList?Work_order_numbering={Work_order}";
                var response = await client.GetAsync(apiUrl);
                if (response.IsSuccessStatusCode)
                {
                    var inquiryData = await response.Content.ReadAsStringAsync();
                    inquiryList = JsonConvert.DeserializeObject<List<PM>>(inquiryData);
                }
            }
            return inquiryList;
        }

        public async Task<List<ProductionStatus>> ProdStats(string Work_order)
        {
            var base64Credentials = Convert.ToBase64String(Encoding.ASCII.GetBytes($"{Username}:{PasswordHash}"));
            var inquiryList = new List<ProductionStatus>();
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", base64Credentials);
                ServicePointManager.ServerCertificateValidationCallback += (sender, certificate, chain, sslPolicyErrors) => true;
                var apiUrl = $"{BaseUrl}Demo/Get_ProductionStatusList?Work_order_numbers={Work_order}";
                var response = await client.GetAsync(apiUrl);
                if (response.IsSuccessStatusCode)
                {
                    var inquiryData = await response.Content.ReadAsStringAsync();
                    inquiryList = JsonConvert.DeserializeObject<List<ProductionStatus>>(inquiryData);
                }
            }
            return inquiryList;
        }

        public async Task<List<PackingS>> PackStats(string Work_order)
        {
            var base64Credentials = Convert.ToBase64String(Encoding.ASCII.GetBytes($"{Username}:{PasswordHash}"));
            var inquiryList = new List<PackingS>();
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", base64Credentials);
                ServicePointManager.ServerCertificateValidationCallback += (sender, certificate, chain, sslPolicyErrors) => true;
                var apiUrl = $"{BaseUrl}Demo/Get_PackingStatusList?Work_order_numberd={Work_order}";
                var response = await client.GetAsync(apiUrl);
                if (response.IsSuccessStatusCode)
                {
                    var inquiryData = await response.Content.ReadAsStringAsync();
                    inquiryList = JsonConvert.DeserializeObject<List<PackingS>>(inquiryData);
                }
            }
            return inquiryList;
        }

        public async Task<List<Invoice>> InvoiceStats(string Work_order)
        {
            var base64Credentials = Convert.ToBase64String(Encoding.ASCII.GetBytes($"{Username}:{PasswordHash}"));
            var inquiryList = new List<Invoice>();
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", base64Credentials);
                ServicePointManager.ServerCertificateValidationCallback += (sender, certificate, chain, sslPolicyErrors) => true;
                var apiUrl = $"{BaseUrl}Demo/Get_InvoiceStatusList?Work_order={Work_order}";
                var response = await client.GetAsync(apiUrl);
                if (response.IsSuccessStatusCode)
                {
                    var inquiryData = await response.Content.ReadAsStringAsync();
                    inquiryList = JsonConvert.DeserializeObject<List<Invoice>>(inquiryData);
                }
            }
            return inquiryList;
        }

        public async Task<List<Invoice>> PaymentStats(string Work_order)
        {
            var base64Credentials = Convert.ToBase64String(Encoding.ASCII.GetBytes($"{Username}:{PasswordHash}"));
            var inquiryList = new List<Invoice>();
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", base64Credentials);
                ServicePointManager.ServerCertificateValidationCallback += (sender, certificate, chain, sslPolicyErrors) => true;
                var apiUrl = $"{BaseUrl}Demo/Get_PaymentStatusList?Works_orders={Work_order}";
                var response = await client.GetAsync(apiUrl);
                if (response.IsSuccessStatusCode)
                {
                    var inquiryData = await response.Content.ReadAsStringAsync();
                    inquiryList = JsonConvert.DeserializeObject<List<Invoice>>(inquiryData);
                }
            }
            return inquiryList;
        }


        public async Task<List<UserDetails>> PartyDetails(string userID)
        {
            var base64Credentials = Convert.ToBase64String(Encoding.ASCII.GetBytes($"{Username}:{PasswordHash}"));
            var inquiryList = new List<UserDetails>();
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", base64Credentials);
                ServicePointManager.ServerCertificateValidationCallback += (sender, certificate, chain, sslPolicyErrors) => true;
                var apiUrl = $"{BaseUrl}Demo/UserProfile?partycode={userID}";
                var response = await client.GetAsync(apiUrl);
                if (response.IsSuccessStatusCode)
                {
                    var inquiryData = await response.Content.ReadAsStringAsync();
                    inquiryList = JsonConvert.DeserializeObject<List<UserDetails>>(inquiryData);
                }
            }
            return inquiryList;
        }

    }
}
