using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace ParentalControl.App.Mobile.Models
{
    public class RegisterResponseModel
    {
        [JsonProperty("MessageError")]
        public string MessageError { get; set; }

        [JsonProperty("Registered")]
        public bool Registered { get; set; }
    }
}
