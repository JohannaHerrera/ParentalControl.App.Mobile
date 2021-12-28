using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace ParentalControl.App.Mobile.Models
{
    class ScheduleEditFindModel
    {
        [JsonProperty("ScheduleId")]
        public int? ScheduleId { get; set; }
    }
}
