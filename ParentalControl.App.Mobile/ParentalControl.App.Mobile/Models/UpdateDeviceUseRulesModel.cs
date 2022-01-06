using System;
using System.Collections.Generic;
using System.Text;

namespace ParentalControl.App.Mobile.Models
{
    public class UpdateDeviceUseRulesModel
    {
        public string DeviceUseDay { get; set; }
        public int InfantAccountId { get; set; }
        public Nullable<int> ScheduleId { get; set; }
    }
}
