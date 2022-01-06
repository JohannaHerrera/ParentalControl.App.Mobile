using System;
using System.Collections.Generic;
using System.Text;

namespace ParentalControl.App.Mobile.Models
{
    public class UpdateAppRulesModel
    {
        public int AppId { get; set; }
        public int? DevicePhoneId { get; set; }
        public int InfantAccountId { get; set; }
        public int? ScheduleId { get; set; }
        public string AppName { get; set; }
        public bool? AppAccessPermission { get; set; }
    }
}
