using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace ParentalControl.App.Mobile.Models
{
    public class AppRulesResponseModel
    {
        [JsonProperty("appRulesModelList")]
        public List<AppRulesModel> appRulesModelList { get; set; }
        [JsonProperty("MessageError")]
        public string MessageError { get; set; }
        [JsonProperty("IsSuccess")]
        public bool IsSuccess { get; set; }
    }
}
