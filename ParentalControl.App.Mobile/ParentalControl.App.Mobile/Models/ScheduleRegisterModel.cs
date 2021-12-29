using System;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Text;

namespace ParentalControl.App.Mobile.Models
{
    class ScheduleRegisterModel
    {
        //public int? ScheduleId { get; set; }
        [JsonProperty("ParentId")]
        public int ParentId { get; set; }
        [JsonProperty("ScheduleStartTime")]
        public DateTime ScheduleStartTime { get; set; }
        [JsonProperty("ScheduleEndTime")]
        public DateTime ScheduleEndTime { get; set; }
        [JsonProperty("Action")]
        public int Action { get; set; }
    }
}
