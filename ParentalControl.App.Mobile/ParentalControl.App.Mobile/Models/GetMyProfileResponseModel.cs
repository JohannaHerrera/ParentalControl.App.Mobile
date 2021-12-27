using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace ParentalControl.App.Mobile.Models
{
    public class GetMyProfileResponseModel
    {
        [JsonProperty("ParentUsername")]
        public string ParentUsername { get; set; }
        [JsonProperty("ParentEmail")]
        public string ParentEmail { get; set; }
        [JsonProperty("ParentPassword")]
        public string ParentPassword { get; set; }
        [JsonProperty("MessageError")]
        public string MessageError { get; set; }
    }
}
