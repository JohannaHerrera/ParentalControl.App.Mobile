using System;
using System.Collections.Generic;
using System.Text;

namespace ParentalControl.App.Mobile.Models
{
    public class GetDeviceInfoModel
    {
        public int ParentId { get; set; }
        public string DevicePhoneCode { get; set; }
        // Action = 1 (GetDeviceInfo)
        // Action = 2 (DeleteDevice)
        public int Action { get; set; }
    }
}
