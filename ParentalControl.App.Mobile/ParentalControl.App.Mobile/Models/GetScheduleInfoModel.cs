using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace ParentalControl.App.Mobile.Models
{
    class GetScheduleInfoModel
    {
        [JsonProperty("ScheduleId")]
        public int ScheduleId { get; set; }
        [JsonProperty("ParentId")]
        public int ParentId { get; set; }
    }
}
