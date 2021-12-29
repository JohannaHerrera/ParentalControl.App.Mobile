using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace ParentalControl.App.Mobile.Models
{
    public class RequestModel
    {
        [JsonProperty("RequestId")]
        public int RequestId { get; set; }
        [JsonProperty("RequestTypeId")]
        public int RequestTypeId { get; set; }
        [JsonProperty("InfantAccountId")]
        public int InfantAccountId { get; set; }
        [JsonProperty("RequestObject")]
        public string requestModelList { get; set; }
        [JsonProperty("RequestTime")]
        public decimal? RequestTime { get; set; }
        [JsonProperty("RequestState")]
        public int RequestState { get; set; }
        [JsonProperty("InfantName")]
        public string InfantName { get; set; }
        [JsonProperty("InfantGender")]
        public string InfantGender { get; set; }
        [JsonProperty("RequestDescription")]
        public string RequestDescription { get; set; }
    }
}
