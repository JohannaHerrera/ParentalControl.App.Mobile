using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace ParentalControl.App.Mobile.Models
{
    public class DevicePhoneResponseModel
    {
        [JsonProperty("DevicePhoneId")]
        public int DevicePhoneId { get; set; }
        [JsonProperty("DevicePhoneName")]
        public string DevicePhoneName { get; set; }
        [JsonProperty("DevicePhoneCode")]
        public string DevicePhoneCode { get; set; }
        [JsonProperty("InfantAccountId")]
        public int? InfantAccountId { get; set; }
        [JsonProperty("devicePhoneUseList")]
        public List<DevicePhoneUseModel> devicePhoneUseList { get; set; }
        [JsonProperty("infantAccountList")]
        public List<InfantAccountModel> infantAccountList { get; set; }
        [JsonProperty("scheduleList")]
        public List<ScheduleModel> scheduleList { get; set; }
        [JsonProperty("MessageError")]
        public string MessageError { get; set; }
    }
}
