using System;
using System.Collections.Generic;
using System.Text;
using ParentalControl.App.Mobile.Interfaces;
using Newtonsoft.Json;
using ParentalControl.App.Mobile.Models;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace ParentalControl.App.Mobile.Services
{
    class WebConfigurationService : IWebConfiguration
    {
        public async Task<WebConfigurationResponseModel> getWebConfiguration(string information)
        {
            HttpClient client = new HttpClient();
            Constants constants = new Constants();
            client.BaseAddress = new Uri(constants.Uri);
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
            var body = JsonConvert.SerializeObject(information);
            var content = new StringContent(body, Encoding.UTF8, "application/json");

            HttpResponseMessage response = await client.PostAsync("api/WebConfiguration", content);

            if (response.StatusCode == HttpStatusCode.OK)
            {
                var deserialize = await response.Content.ReadAsStringAsync();
                var responseContent = JsonConvert.DeserializeObject<WebConfigurationResponseModel>(deserialize);
                return responseContent;
            }
            else
            {
                return null;
            }
        }
    }
}
