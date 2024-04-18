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
    public class InquiryBLL
    {
        private const string Username = "SampleUser";
        private const string PasswordHash = "SamplePasswordHash";
        private const string BaseUrl = "https://103.80.32.71/testapi/";
        private readonly HttpClient _client=new HttpClient();
        public async Task<string> GenerateEnqCode(string userID)
        {
            string EnqCode = "";
            var base64Credentials = Convert.ToBase64String(Encoding.ASCII.GetBytes($"{Username}:{PasswordHash}"));
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", base64Credentials);
            ServicePointManager.ServerCertificateValidationCallback += (sender, certificate, chain, sslPolicyErrors) => true;
            var apiUrl = $"{BaseUrl}Demo/GenerateEnqCode?Party={userID}";
            var response = await _client.GetAsync(apiUrl);

            if (response.IsSuccessStatusCode)
            {
                var Inq=await response.Content.ReadAsStringAsync();
                EnqCode = JsonConvert.DeserializeObject<string>(Inq);
            }
            return EnqCode;
        }

        public async Task<List<CountryAirSea>> GetCountryList()
        {
            var base64Credentials = Convert.ToBase64String(Encoding.ASCII.GetBytes($"{Username}:{PasswordHash}"));
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", base64Credentials);
            ServicePointManager.ServerCertificateValidationCallback += (sender, certificate, chain, sslPolicyErrors) => true;
            var apiUrl = $"{BaseUrl}Demo/GetddlCountryList";
            var response = await _client.GetAsync(apiUrl);

            if (response.IsSuccessStatusCode)
            {
                var countryList = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<List<CountryAirSea>>(countryList);
            }

            return new List<CountryAirSea>();
        }


        public async Task<List<Inquiry>> GetInquiries(string userID)
        {
            var base64Credentials = Convert.ToBase64String(Encoding.ASCII.GetBytes($"{Username}:{PasswordHash}"));
            List<Inquiry> inquiries = new List<Inquiry>();

            using (var client = new HttpClient())
            {
                var apiUrl = $"{BaseUrl}Demo/GetDataByPartyEnqCodeList?Partycodes={userID}";

                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Basic", base64Credentials);
                ServicePointManager.ServerCertificateValidationCallback += (sender, certificate, chain, sslPolicyErrors) => true;

                var response = await client.GetAsync(apiUrl);

                if (response.IsSuccessStatusCode)
                {
                    var inquiryData = await response.Content.ReadAsStringAsync();
                    inquiries = JsonConvert.DeserializeObject<List<Inquiry>>(inquiryData);
                }
            }

            return inquiries;
        }


        public async Task<List<Product>> GetProducts(string id)
        {
            var base64Credentials = Convert.ToBase64String(Encoding.ASCII.GetBytes($"{Username}:{PasswordHash}"));
            List<Product> products = new List<Product>();

            using (var client = new HttpClient())
            {
                var apiUrl = $"{BaseUrl}Demo/GetdatabtPartyEnqList?PartyEnq={id}";

                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Basic", base64Credentials);
                ServicePointManager.ServerCertificateValidationCallback += (sender, certificate, chain, sslPolicyErrors) => true;

                var response = await client.GetAsync(apiUrl);

                if (response.IsSuccessStatusCode)
                {
                    var inquiryData = await response.Content.ReadAsStringAsync();
                    products = JsonConvert.DeserializeObject<List<Product>>(inquiryData);
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


        public async Task<List<CountryAirSea>> GetAirportsByCountry(string country)
        {
            var base64Credentials = Convert.ToBase64String(Encoding.ASCII.GetBytes($"{Username}:{PasswordHash}"));
            var airportList = new List<CountryAirSea>();

            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", base64Credentials);
                ServicePointManager.ServerCertificateValidationCallback += (sender, certificate, chain, sslPolicyErrors) => true;

                var apiUrl = $"{BaseUrl}Demo/ddlChangedAirportList?coun_code={country}";
                var response = await client.GetAsync(apiUrl);

                if (response.IsSuccessStatusCode)
                {
                    var airportData = await response.Content.ReadAsStringAsync();
                    airportList = JsonConvert.DeserializeObject<List<CountryAirSea>>(airportData);
                }
            }

            return airportList;
        }


        public async Task<List<CountryAirSea>> GetSeaportsByCountry(string country)
        {
            var base64Credentials = Convert.ToBase64String(Encoding.ASCII.GetBytes($"{Username}:{PasswordHash}"));
            var SeaPortList = new List<CountryAirSea>();

            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", base64Credentials);
                ServicePointManager.ServerCertificateValidationCallback += (sender, certificate, chain, sslPolicyErrors) => true;

                var apiUrl = $"{BaseUrl}Demo/GetddlChangedSeaportList?coun_code={country}";
                var response = await client.GetAsync(apiUrl);

                if (response.IsSuccessStatusCode)
                {
                    var airportData = await response.Content.ReadAsStringAsync();
                    SeaPortList = JsonConvert.DeserializeObject<List<CountryAirSea>>(airportData);
                }
            }

            return SeaPortList;
        }


        public async Task<string> GetEnqCode(string userID)
        {
            var base64Credentials = Convert.ToBase64String(Encoding.ASCII.GetBytes($"{Username}:{PasswordHash}"));
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", base64Credentials);
            ServicePointManager.ServerCertificateValidationCallback += (sender, certificate, chain, sslPolicyErrors) => true;
            var apiUrl = $"{BaseUrl}Demo/EnqCode?Party={userID}";
            var response = await _client.GetAsync(apiUrl);
            string EnqCode = "";
            if (response.IsSuccessStatusCode)
            {
                var Inq = await response.Content.ReadAsStringAsync();
                EnqCode = JsonConvert.DeserializeObject<string>(Inq);
            }
            return EnqCode;
        }

        public async Task<List<UnitsViewModel>> GetUnitList()
        {
            var base64Credentials = Convert.ToBase64String(Encoding.ASCII.GetBytes($"{Username}:{PasswordHash}"));
            var UnitList = new List<UnitsViewModel>();
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", base64Credentials);
                ServicePointManager.ServerCertificateValidationCallback += (sender, certificate, chain, sslPolicyErrors) => true;
                var apiUrl = $"{BaseUrl}Demo/unit_masterList";
                var response = await client.GetAsync(apiUrl);
                if (response.IsSuccessStatusCode)
                {
                    var unitData = await response.Content.ReadAsStringAsync();
                    UnitList = JsonConvert.DeserializeObject<List<UnitsViewModel>>(unitData);
                }
            }
            return UnitList;
        }


        public async Task<List<DossageForm>> GetDoseList()
        {
            var base64Credentials = Convert.ToBase64String(Encoding.ASCII.GetBytes($"{Username}:{PasswordHash}"));
            var DossageList = new List<DossageForm>();
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", base64Credentials);
                ServicePointManager.ServerCertificateValidationCallback += (sender, certificate, chain, sslPolicyErrors) => true;
                var apiUrl = $"{BaseUrl}Demo/GetSectionName";
                var response = await client.GetAsync(apiUrl);
                if (response.IsSuccessStatusCode)
                {
                    var doseData = await response.Content.ReadAsStringAsync();
                    DossageList = JsonConvert.DeserializeObject<List<DossageForm>>(doseData);
                }
            }
            return DossageList;
        }


        public async Task<List<Inquiry>> GetInquiryCodeList(string userID)
        {
            var base64Credentials = Convert.ToBase64String(Encoding.ASCII.GetBytes($"{Username}:{PasswordHash}"));
            var inquiryList = new List<Inquiry>();
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", base64Credentials);
                ServicePointManager.ServerCertificateValidationCallback += (sender, certificate, chain, sslPolicyErrors) => true;
                var apiUrl = $"{BaseUrl}Demo/GetEnqByPartydatalistdata?party={userID}";
                var response = await client.GetAsync(apiUrl);
                if (response.IsSuccessStatusCode)
                {
                    var inquiryData = await response.Content.ReadAsStringAsync();
                    inquiryList = JsonConvert.DeserializeObject<List<Inquiry>>(inquiryData);
                }
            }

            return inquiryList;
        }


        public async Task<List<OldInquiry>> GetOldInquiryList(string userID)
        {
            var base64Credentials = Convert.ToBase64String(Encoding.ASCII.GetBytes($"{Username}:{PasswordHash}"));
            var inquiryList = new List<OldInquiry>();
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", base64Credentials);
                ServicePointManager.ServerCertificateValidationCallback += (sender, certificate, chain, sslPolicyErrors) => true;
                var apiUrl = $"{BaseUrl}Demo/Get_AllEnquiryList?Party_code={userID}";
                var response = await client.GetAsync(apiUrl);
                if (response.IsSuccessStatusCode)
                {
                    var inquiryData = await response.Content.ReadAsStringAsync();
                    inquiryList = JsonConvert.DeserializeObject<List<OldInquiry>>(inquiryData);
                }
            }

            return inquiryList;
        }



        public async Task<List<OldInquiry>> GetPendingInquiryList(string userID)
        {
            var base64Credentials = Convert.ToBase64String(Encoding.ASCII.GetBytes($"{Username}:{PasswordHash}"));
            var inquiryList = new List<OldInquiry>();
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", base64Credentials);
                ServicePointManager.ServerCertificateValidationCallback += (sender, certificate, chain, sslPolicyErrors) => true;
                var apiUrl = $"{BaseUrl}Demo/Get_PendingInquiry?PartyCode={userID}";
                var response = await client.GetAsync(apiUrl);
                if (response.IsSuccessStatusCode)
                {
                    var inquiryData = await response.Content.ReadAsStringAsync();
                    inquiryList = JsonConvert.DeserializeObject<List<OldInquiry>>(inquiryData);
                }
            }

            return inquiryList;
        }




        public async Task<List<OldInquiry>> GetApprovedInquiryList(string userID)
        {
            var base64Credentials = Convert.ToBase64String(Encoding.ASCII.GetBytes($"{Username}:{PasswordHash}"));
            var inquiryList = new List<OldInquiry>();
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", base64Credentials);
                ServicePointManager.ServerCertificateValidationCallback += (sender, certificate, chain, sslPolicyErrors) => true;
                var apiUrl = $"{BaseUrl}Demo/Get_ApprovedEnquiryList?Party_code={userID}";
                var response = await client.GetAsync(apiUrl);
                if (response.IsSuccessStatusCode)
                {
                    var inquiryData = await response.Content.ReadAsStringAsync();
                    inquiryList = JsonConvert.DeserializeObject<List<OldInquiry>>(inquiryData);
                }
            }

            return inquiryList;
        }



        public async Task<List<OldInquiry>> GetInqCodeWiseInquiryList(string inquiryCode)
        {
            var base64Credentials = Convert.ToBase64String(Encoding.ASCII.GetBytes($"{Username}:{PasswordHash}"));
            var inquiryList = new List<OldInquiry>();
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", base64Credentials);
                ServicePointManager.ServerCertificateValidationCallback += (sender, certificate, chain, sslPolicyErrors) => true;
                var apiUrl = $"{BaseUrl}Demo/GetEnqCodesByDateList?EnquiryCode={inquiryCode}";
                var response = await client.GetAsync(apiUrl);
                if (response.IsSuccessStatusCode)
                {
                    var inquiryData = await response.Content.ReadAsStringAsync();
                    inquiryList = JsonConvert.DeserializeObject<List<OldInquiry>>(inquiryData);
                }
            }

            return inquiryList;
        }


        public async Task<List<OldInquiry>> GetDoseWiseInquiryList(string dosageForm,string userID)
        {
            var base64Credentials = Convert.ToBase64String(Encoding.ASCII.GetBytes($"{Username}:{PasswordHash}"));
            var inquiryList = new List<OldInquiry>();
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", base64Credentials);
                ServicePointManager.ServerCertificateValidationCallback += (sender, certificate, chain, sslPolicyErrors) => true;
                var apiUrl = $"{BaseUrl}Demo/GetDSGBYPARTYLIST?DSGCODES={dosageForm}&PARTYCODES={userID}";
                var response = await client.GetAsync(apiUrl);
                if (response.IsSuccessStatusCode)
                {
                    var inquiryData = await response.Content.ReadAsStringAsync();
                    inquiryList = JsonConvert.DeserializeObject<List<OldInquiry>>(inquiryData);
                }
            }

            return inquiryList;
        }


        public async Task<List<OldInquiry>> GetDateWiseInquiryList(string startDate, string endDate, string userID)
        {
            var base64Credentials = Convert.ToBase64String(Encoding.ASCII.GetBytes($"{Username}:{PasswordHash}"));
            var inquiryList = new List<OldInquiry>();
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", base64Credentials);
                ServicePointManager.ServerCertificateValidationCallback += (sender, certificate, chain, sslPolicyErrors) => true;
                var apiUrl = $"{BaseUrl}Demo/GetEnqCodesByDate?PartyCodes={userID}&StartDate={startDate}&EndDate={endDate}";
                var response = await client.GetAsync(apiUrl);
                if (response.IsSuccessStatusCode)
                {
                    var inquiryData = await response.Content.ReadAsStringAsync();
                    inquiryList = JsonConvert.DeserializeObject<List<OldInquiry>>(inquiryData);
                }
            }

            return inquiryList;
        }


        public async Task<bool> GenerateInquiryFirst(GenInqFirst model)
        {
            var base64Credentials = Convert.ToBase64String(Encoding.ASCII.GetBytes($"{Username}:{PasswordHash}"));
            string json = JsonConvert.SerializeObject(model);

            using (var client = new HttpClient())
            {
                var apiUrl = $"{BaseUrl}Demo/Insertenquiry_quatation";

                var content = new StringContent(json, Encoding.UTF8, "application/json");
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", base64Credentials);
                ServicePointManager.ServerCertificateValidationCallback += (sender, certificate, chain, sslPolicyErrors) => true;

                var response = await client.PostAsync(apiUrl, content);

                return response.IsSuccessStatusCode;
            }
        }


        public async Task<int> GenerateInquirySecond(List<PlaceInquiry> model)
        {
            var base64Credentials = Convert.ToBase64String(Encoding.ASCII.GetBytes($"{Username}:{PasswordHash}"));
            string json = JsonConvert.SerializeObject(model);
            int result = 0;

            using (var client = new HttpClient())
            {
                var apiUrl = $"{BaseUrl}Demo/InsertMaster";

                var content = new StringContent(json, Encoding.UTF8, "application/json");
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", base64Credentials);
                ServicePointManager.ServerCertificateValidationCallback += (sender, certificate, chain, sslPolicyErrors) => true;

                var response = await client.PostAsync(apiUrl, content);

                if (response.IsSuccessStatusCode)
                {
                    var dosejson = await response.Content.ReadAsStringAsync();
                    result = JsonConvert.DeserializeObject<int>(dosejson);
                }

                return result;
            }
        }


    }
}
