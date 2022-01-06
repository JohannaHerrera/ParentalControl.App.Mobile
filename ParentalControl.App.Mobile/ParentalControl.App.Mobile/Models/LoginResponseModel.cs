using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace ParentalControl.App.Mobile.Models
{
    public class LoginResponseModel
    {
        [JsonProperty("ParentId")]
        public int ParentId { get; set; }

        [JsonProperty("ParentUsername")]
        public string ParentUserName { get; set; }

        [JsonProperty("ParentEmail")]
        public string ParentEmail { get; set; }

        [JsonProperty("MessageError")]
        public string MessageError { get; set; }
        [JsonProperty("IsFirstTime")]
        public bool IsFirstTime { get; set; }
    }
}
