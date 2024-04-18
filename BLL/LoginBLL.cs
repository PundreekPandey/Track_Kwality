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
   public class LoginBLL
    {
        private const string Username = "SampleUser";
        private const string PasswordHash = "SamplePasswordHash";
        private const string BaseUrl = "https://103.80.32.71/testapi/";
        public async Task<string> AuthenticateUser(LoginViewModel model)
        {
            var base64Credentials = Convert.ToBase64String(Encoding.ASCII.GetBytes($"{Username}:{PasswordHash}"));
            string loginResult = "";

            using (var client = new HttpClient())
            {
                var apiUrl = $"{BaseUrl}Demo/DashboardLogin?UserName={model.UserId}&password={model.Password}";
                var data = new { UserName = model.UserId, password = model.Password };
                var json = JsonConvert.SerializeObject(data);
                HttpContent content = new StringContent(json, Encoding.UTF8, "application/json");

                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", base64Credentials);
                ServicePointManager.ServerCertificateValidationCallback += (sender, certificate, chain, sslPolicyErrors) => true;

                var response = await client.PostAsync(apiUrl, content);

                if (response.IsSuccessStatusCode)
                {
                    var loginUser = await response.Content.ReadAsStringAsync();
                    var logindata = JsonConvert.DeserializeObject<CustomIdentity>(loginUser);
                    loginResult = logindata.LoginResult;
                }

                return loginResult;
            }
        }
    }
}
