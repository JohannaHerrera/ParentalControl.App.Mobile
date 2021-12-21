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
    public class DeviceService : IDeviceService
    {
        public async Task<DeviceResponseModel> GetDeviceInfo(string parentId)
        {
            HttpClient client = new HttpClient();
            Constants constants = new Constants();

            client.BaseAddress = new Uri(constants.Uri);
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
            var body = JsonConvert.SerializeObject(parentId);
            var content = new StringContent(body, Encoding.UTF8, "application/json");

            HttpResponseMessage response = await client.PostAsync("api/Device", content);

            if (response.StatusCode == HttpStatusCode.OK)
            {
                var deserialize = await response.Content.ReadAsStringAsync();
                var responseContent = JsonConvert.DeserializeObject<DeviceResponseModel>(deserialize);
                return responseContent;
            }
            else
            {
                return null;
            }
        }
    }
}
