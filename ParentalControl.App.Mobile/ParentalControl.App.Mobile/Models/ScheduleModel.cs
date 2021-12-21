using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace ParentalControl.App.Mobile.Models
{
    public class ScheduleModel
    {
        [JsonProperty("ScheduleId")]
        public int ScheduleId { get; set; }
        [JsonProperty("ScheduleTime")]
        public string ScheduleTime { get; set; }
    }
}
