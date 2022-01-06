using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace ParentalControl.App.Mobile.Models
{
    public class DeviceUseRulesResponseModel
    {
        [JsonProperty("deviceUseRulesModelList")]
        public List<DeviceUseRulesModel> deviceUseRulesModelList { get; set; }
        [JsonProperty("MessageError")]
        public string MessageError { get; set; }
        [JsonProperty("IsSuccess")]
        public bool IsSuccess { get; set; }
    }
}
