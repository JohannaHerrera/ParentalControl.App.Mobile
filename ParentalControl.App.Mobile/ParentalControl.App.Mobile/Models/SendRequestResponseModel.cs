using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace ParentalControl.App.Mobile.Models
{
    public class SendRequestResponseModel
    {
        [JsonProperty("MessageError")]
        public string MessageError { get; set; }
        [JsonProperty("IsSend")]
        public bool IsSend { get; set; }
    }
}
