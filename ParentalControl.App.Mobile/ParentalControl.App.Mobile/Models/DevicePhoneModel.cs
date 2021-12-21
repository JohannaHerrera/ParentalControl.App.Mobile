using System;
using System.Collections.Generic;
using System.Text;

namespace ParentalControl.App.Mobile.Models
{
    public class DevicePhoneModel
    {
        public string DevicePhoneCode { get; set; }
        public string DevicePhoneName { get; set; }
        public List<AppDeviceModel> AppsInstalled { get; set; }
    }
}
