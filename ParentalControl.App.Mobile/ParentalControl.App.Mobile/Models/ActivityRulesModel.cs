using System;
using System.Collections.Generic;
using System.Text;

namespace ParentalControl.App.Mobile.Models
{
    public class ActivityRulesModel
    {
        public int ActivityId { get; set; }
        public int InfantAccountId { get; set; }
        public string ActivityObject { get; set; }
        public string ActivityDescription { get; set; }
        public System.DateTime ActivityCreationDate { get; set; }
        public int ActivityTimesAccess { get; set; }
    }
}
