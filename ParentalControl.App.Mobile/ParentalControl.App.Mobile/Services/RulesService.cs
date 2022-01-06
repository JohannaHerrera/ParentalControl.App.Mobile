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
    public class RulesService : IRulesService
    {
        public async Task<ActivityRulesResponseModel> GetActivityRules(string infantAccountId)
        {
            HttpClient client = new HttpClient();
            Constants constants = new Constants();

            client.BaseAddress = new Uri(constants.Uri);
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
            string uri = $"{client.BaseAddress}api/ActivityRules?infantAccountId={infantAccountId}";
            HttpResponseMessage response = await client.GetAsync(uri);

            if (response.StatusCode == HttpStatusCode.OK)
            {
                var deserialize = await response.Content.ReadAsStringAsync();
                var responseContent = JsonConvert.DeserializeObject<ActivityRulesResponseModel>(deserialize);
                return responseContent;
            }
            else
            {
                return null;
            }
        }

        public async Task<AppRulesResponseModel> GetAppRules(string infantAccountId)
        {
            HttpClient client = new HttpClient();
            Constants constants = new Constants();

            client.BaseAddress = new Uri(constants.Uri);
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
            string uri = $"{client.BaseAddress}api/AppRules?infantAccountId={infantAccountId}";
            HttpResponseMessage response = await client.GetAsync(uri);

            if (response.StatusCode == HttpStatusCode.OK)
            {
                var deserialize = await response.Content.ReadAsStringAsync();
                var responseContent = JsonConvert.DeserializeObject<AppRulesResponseModel>(deserialize);
                return responseContent;
            }
            else
            {
                return null;
            }
        }

        public async Task<DeviceUseRulesResponseModel> GetDeviceUseRules(string infantAccountId)
        {
            HttpClient client = new HttpClient();
            Constants constants = new Constants();

            client.BaseAddress = new Uri(constants.Uri);
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
            string uri = $"{client.BaseAddress}api/DeviceUseRules?infantAccountId={infantAccountId}";
            HttpResponseMessage response = await client.GetAsync(uri);

            if (response.StatusCode == HttpStatusCode.OK)
            {
                var deserialize = await response.Content.ReadAsStringAsync();
                var responseContent = JsonConvert.DeserializeObject<DeviceUseRulesResponseModel>(deserialize);
                return responseContent;
            }
            else
            {
                return null;
            }
        }

        public async Task<WebConfigurationRulesResponseModel> GetWebConfigurationRules(string infantAccountId)
        {
            HttpClient client = new HttpClient();
            Constants constants = new Constants();

            client.BaseAddress = new Uri(constants.Uri);
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
            string uri = $"{client.BaseAddress}api/WebConfigurationRules?infantAccountId={infantAccountId}";
            HttpResponseMessage response = await client.GetAsync(uri);

            if (response.StatusCode == HttpStatusCode.OK)
            {
                var deserialize = await response.Content.ReadAsStringAsync();
                var responseContent = JsonConvert.DeserializeObject<WebConfigurationRulesResponseModel>(deserialize);
                return responseContent;
            }
            else
            {
                return null;
            }
        }

        public async Task<bool> UpdateAppRules(List<UpdateAppRulesModel> updateAppRulesModelList)
        {
            HttpClient client = new HttpClient();
            Constants constants = new Constants();

            client.BaseAddress = new Uri(constants.Uri);
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
            var body = JsonConvert.SerializeObject(updateAppRulesModelList);
            var content = new StringContent(body, Encoding.UTF8, "application/json");

            HttpResponseMessage response = await client.PutAsync("api/AppRules", content);

            if (response.StatusCode == HttpStatusCode.OK)
            {
                var deserialize = await response.Content.ReadAsStringAsync();
                var responseContent = JsonConvert.DeserializeObject<bool>(deserialize);
                return responseContent;
            }
            else
            {
                return false;
            }
        }

        public async Task<bool> UpdateDeviceUseRules(List<UpdateDeviceUseRulesModel> updateDeviceUseRulesModelList)
        {
            HttpClient client = new HttpClient();
            Constants constants = new Constants();

            client.BaseAddress = new Uri(constants.Uri);
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
            var body = JsonConvert.SerializeObject(updateDeviceUseRulesModelList);
            var content = new StringContent(body, Encoding.UTF8, "application/json");

            HttpResponseMessage response = await client.PutAsync("api/DeviceUseRules", content);

            if (response.StatusCode == HttpStatusCode.OK)
            {
                var deserialize = await response.Content.ReadAsStringAsync();
                var responseContent = JsonConvert.DeserializeObject<bool>(deserialize);
                return responseContent;
            }
            else
            {
                return false;
            }
        }

        public async Task<bool> UpdateWebConfigurationRules(List<UpdateWebConfigurationRulesModel> updateWebConfigurationRulesModelList)
        {
            HttpClient client = new HttpClient();
            Constants constants = new Constants();

            client.BaseAddress = new Uri(constants.Uri);
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
            var body = JsonConvert.SerializeObject(updateWebConfigurationRulesModelList);
            var content = new StringContent(body, Encoding.UTF8, "application/json");

            HttpResponseMessage response = await client.PutAsync("api/WebConfigurationRules", content);

            if (response.StatusCode == HttpStatusCode.OK)
            {
                var deserialize = await response.Content.ReadAsStringAsync();
                var responseContent = JsonConvert.DeserializeObject<bool>(deserialize);
                return responseContent;
            }
            else
            {
                return false;
            }
        }
    }
}
