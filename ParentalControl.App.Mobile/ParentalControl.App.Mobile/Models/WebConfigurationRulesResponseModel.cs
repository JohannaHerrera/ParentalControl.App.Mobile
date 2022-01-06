using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace ParentalControl.App.Mobile.Models
{
    public class WebConfigurationRulesResponseModel
    {
        [JsonProperty("webConfigurationRulesModelList")]
        public List<WebConfigurationRulesModel> webConfigurationRulesModelList { get; set; }
        [JsonProperty("MessageError")]
        public string MessageError { get; set; }
        [JsonProperty("IsSuccess")]
        public bool IsSuccess { get; set; }
    }
}
