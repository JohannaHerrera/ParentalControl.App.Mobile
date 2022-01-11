using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace ParentalControl.App.Mobile.Models
{
    public class InfantRequestModel
    {
        [JsonProperty("RequestDescription")]
        public string RequestDescription { get; set; }
        [JsonProperty("StateRequest")]
        public string StateRequest { get; set; }
    }
}
