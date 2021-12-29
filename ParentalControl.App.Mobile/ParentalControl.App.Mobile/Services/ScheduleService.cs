using ParentalControl.App.Mobile.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
using ParentalControl.App.Mobile.Models;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace ParentalControl.App.Mobile.Services
{
    class ScheduleService : IScheduleService
    {
        public async Task<List<ScheduleResponseModel>> ScheduleIndex(GetScheduleInfoModel getScheduleInfoModel)
        {
            HttpClient client = new HttpClient();
            Constants constants = new Constants();
            client.BaseAddress = new Uri(constants.Uri);
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
            var body = JsonConvert.SerializeObject(getScheduleInfoModel);
            var content = new StringContent(body, Encoding.UTF8, "application/json");

            HttpResponseMessage response = await client.PostAsync("api/Schedule", content);


            if (response.StatusCode == HttpStatusCode.OK)
            {
                var deserialize = await response.Content.ReadAsStringAsync();
                var responseContent = JsonConvert.DeserializeObject<List<ScheduleResponseModel>>(deserialize);
                return responseContent;
            }
            else
            {
                return null;
            }
        }
        public async Task<ScheduleResponseModel> DeleteSchedule(GetScheduleInfoModel scheduleInfoModel)
        {
            HttpClient client = new HttpClient();
            Constants constants = new Constants();

            client.BaseAddress = new Uri(constants.Uri);
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
            var body = JsonConvert.SerializeObject(scheduleInfoModel);
            var content = new StringContent(body, Encoding.UTF8, "application/json");

            HttpResponseMessage response = await client.PostAsync("api/ScheduleDelete", content);

            if (response.StatusCode == HttpStatusCode.OK)
            {
                var deserialize = await response.Content.ReadAsStringAsync();
                var responseContent = JsonConvert.DeserializeObject<ScheduleResponseModel>(deserialize);
                return responseContent;
            }
            else
            {
                return null;
            }
        }
        public async Task<ScheduleResponseModel> ScheduleCreate(ScheduleRegisterModel createModel)
        {
            HttpClient client = new HttpClient();
            Constants constants = new Constants();

            client.BaseAddress = new Uri(constants.Uri);
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
            var body = JsonConvert.SerializeObject(createModel);
            var content = new StringContent(body, Encoding.UTF8, "application/json");

            HttpResponseMessage response = await client.PostAsync("api/ScheduleManage", content);

            if (response.StatusCode == HttpStatusCode.OK)
            {
                var deserialize = await response.Content.ReadAsStringAsync();
                var responseContent = JsonConvert.DeserializeObject<ScheduleResponseModel>(deserialize);
                return responseContent;
            }
            else
            {
                return null;
            }
        }
        public async Task<bool> ScheduleUpdate(ScheduleUpdateModel updateModel)
        {
            HttpClient client = new HttpClient();
            Constants constants = new Constants();

            client.BaseAddress = new Uri(constants.Uri);
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
            var body = JsonConvert.SerializeObject(updateModel);
            var content = new StringContent(body, Encoding.UTF8, "application/json");

            HttpResponseMessage response = await client.PutAsync("api/ScheduleManage", content);

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
        public async Task<ScheduleResponseModel> ScheduleFindEdit(ScheduleEditFindModel getScheduleInfoModel)
        {
            HttpClient client = new HttpClient();
            Constants constants = new Constants();
            client.BaseAddress = new Uri(constants.Uri);
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
            var body = JsonConvert.SerializeObject(getScheduleInfoModel);
            var content = new StringContent(body, Encoding.UTF8, "application/json");

            HttpResponseMessage response = await client.PostAsync("api/ScheduleEditFind", content);


            if (response.StatusCode == HttpStatusCode.OK)
            {
                var deserialize = await response.Content.ReadAsStringAsync();
                var responseContent = JsonConvert.DeserializeObject<ScheduleResponseModel>(deserialize);
                return responseContent;
            }
            else
            {
                return null;
            }
        }

    }

}
