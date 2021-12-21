using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace ParentalControl.App.Mobile.Models
{
    public class InfantAccountModel
    {
        [JsonProperty("InfantAccountId")]
        public int InfantAccountId { get; set; }
        [JsonProperty("InfantName")]
        public string InfantName { get; set; }
    }
}
