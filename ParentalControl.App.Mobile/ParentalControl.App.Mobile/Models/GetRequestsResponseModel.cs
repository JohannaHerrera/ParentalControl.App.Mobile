using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace ParentalControl.App.Mobile.Models
{
    public class GetRequestsResponseModel
    {
        [JsonProperty("requestModelList")]
        public List<RequestModel> requestModelList { get; set; }
        [JsonProperty("MessageError")]
        public string MessageError { get; set; }
    }
}
