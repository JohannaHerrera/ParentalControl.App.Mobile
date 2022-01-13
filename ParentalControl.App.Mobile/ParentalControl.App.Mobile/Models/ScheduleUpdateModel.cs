using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace ParentalControl.App.Mobile.Models
{
    public class ScheduleUpdateModel
    {
        [JsonProperty("ScheduleId")]
        public int? ScheduleId { get; set; }
        [JsonProperty("ParentId")]
        public int ParentId { get; set; }
        [JsonProperty("ScheduleStartTime")]
        public DateTime ScheduleStartTime { get; set; }
        [JsonProperty("ScheduleEndTime")]
        public DateTime ScheduleEndTime { get; set; }
    }
}
