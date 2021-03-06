using System;
using System.Collections.Generic;
using System.Text;

namespace ParentalControl.App.Mobile.Models
{
    public class SendRequestModel
    {
        public int DevicePhoneId { get; set; }
        public int ParentId { get; set; }
        public int InfantAccountId { get; set; }
        public int RequestType { get; set; }
        public string Object { get; set; }
        public string Hours { get; set; }
        public string Minutes { get; set; }
    }
}
