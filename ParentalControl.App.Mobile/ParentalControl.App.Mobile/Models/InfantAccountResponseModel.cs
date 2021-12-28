using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace ParentalControl.App.Mobile.Models
{
    public class InfantAccountResponseModel
    {
        [JsonProperty("InfantAccountModelList")]
        public List<ListInfantAccountModel> InfantAccountModelList { get; set; }
        [JsonProperty("MessageError")]
        public string MessageError { get; set; }
        [JsonProperty("IsSuccess")]
        public bool IsSuccess { get; set; }
    }
}
