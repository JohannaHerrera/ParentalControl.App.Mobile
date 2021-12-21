using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace ParentalControl.App.Mobile.Models
{
    public class DevicePhoneUseModel
    {
        [JsonProperty("DevicePhoneUseId")]
        public int DevicePhoneUseId { get; set; }
        [JsonProperty("DevicePhoneUseDay")]
        public string DevicePhoneUseDay { get; set; }
        [JsonProperty("ScheduleId")]
        public int? ScheduleId { get; set; }
    }
}
