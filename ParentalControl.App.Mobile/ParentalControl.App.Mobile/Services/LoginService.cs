using Newtonsoft.Json;
using ParentalControl.App.Mobile.Interfaces;
using ParentalControl.App.Mobile.Models;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace ParentalControl.App.Mobile.Services
{
    public class LoginService : ILoginService
    {
        public async Task<LoginResponseModel> VerifyLogin(LoginModel loginModel)
        {  
            HttpClient client = new HttpClient();
            Constants constants = new Constants();

            client.BaseAddress = new Uri(constants.Uri);
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
            var body = JsonConvert.SerializeObject(loginModel);
            var content = new StringContent(body, Encoding.UTF8, "application/json");
            
            HttpResponseMessage response = await client.PostAsync("api/Login", content);

            if (response.StatusCode == HttpStatusCode.OK)
            {
                var deserialize = await response.Content.ReadAsStringAsync();
                var responseContent = JsonConvert.DeserializeObject<LoginResponseModel>(deserialize);
                return responseContent;
            }
            else
            {
                return null;
            }            
        }

        public async Task<RegisterResponseModel> Register(RegisterModel registerModel)
        {
            HttpClient client = new HttpClient();
            Constants constants = new Constants();

            client.BaseAddress = new Uri(constants.Uri);
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
            var body = JsonConvert.SerializeObject(registerModel);
            var content = new StringContent(body, Encoding.UTF8, "application/json");

            HttpResponseMessage response = await client.PostAsync("api/Register", content);

            if (response.StatusCode == HttpStatusCode.OK)
            {
                var deserialize = await response.Content.ReadAsStringAsync();
                var responseContent = JsonConvert.DeserializeObject<RegisterResponseModel>(deserialize);
                return responseContent;
            }
            else
            {
                return null;
            }
        }
    }
}
