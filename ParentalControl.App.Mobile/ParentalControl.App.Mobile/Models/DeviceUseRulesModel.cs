using System;
using System.Collections.Generic;
using System.Text;

namespace ParentalControl.App.Mobile.Models
{
    public class DeviceUseRulesModel
    {
        public int DeviceUseId { get; set; }
        public string DeviceUseDay { get; set; }
        public System.DateTime DeviceUseCreationDate { get; set; }
        public int InfantAccountId { get; set; }
        public Nullable<int> ScheduleId { get; set; }
    }
}
