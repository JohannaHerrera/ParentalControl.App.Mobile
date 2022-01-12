using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace ParentalControl.App.Mobile.Models
{
    public class DeviceUseByDayResponseModel
    {
        [JsonProperty("Hours")]
        public List<string> Hours { get; set; }
        [JsonProperty("Minutes")]
        public List<string> Minutes { get; set; }
        [JsonProperty("MessageError")]
        public string MessageError { get; set; }
    }
}
