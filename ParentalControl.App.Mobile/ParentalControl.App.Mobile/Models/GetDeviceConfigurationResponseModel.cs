using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace ParentalControl.App.Mobile.Models
{
    public class GetDeviceConfigurationResponseModel
    {
        [JsonProperty("HaveRules")]
        public bool HaveRules { get; set; }
        [JsonProperty("DevicePhoneId")]
        public int DevicePhoneId { get; set; }
        [JsonProperty("InfantAccountId")]
        public int? InfantAccountId { get; set; }
        [JsonProperty("ParentId")]
        public int ParentId { get; set; }
        [JsonProperty("scheduleModelList")]
        public List<ScheduleModel> scheduleModelList { get; set; }
        [JsonProperty("WebsLocked")]
        public List<WebConfigurationRulesModel> WebsLocked { get; set; }
        [JsonProperty("AppsLocked")]
        public List<AppRulesModel> AppsLocked { get; set; }
        [JsonProperty("DeviceConfig")]
        public List<DevicePhoneUseModel> DeviceConfig { get; set; }
        [JsonProperty("MessageError")]
        public string MessageError { get; set; }
    }
}
