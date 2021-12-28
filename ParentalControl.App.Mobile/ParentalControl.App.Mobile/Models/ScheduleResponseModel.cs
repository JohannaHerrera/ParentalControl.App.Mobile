using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
namespace ParentalControl.App.Mobile.Models
{
    class ScheduleResponseModel
    {
        [JsonProperty("ScheduleId")]
        public int ScheduleId { get; set; }
        [JsonProperty("ScheduleStartTime")]
        public string ScheduleStartTime { get; set; }
        [JsonProperty("ScheduleEndTime")]
        public string ScheduleEndTime { get; set; }
        //public DateTime ScheduleCreationDate { get; set; }
        [JsonProperty("ParentId")]
        public int? ParentId { get; set; }
        [JsonProperty("MessageError")]
        public string MessageError { get; set; }
        [JsonProperty("IsSuccess")]
        public bool IsSuccess { get; set; }
        [JsonProperty("Registered")]
        public bool Registered { get; set; }
    }
}
