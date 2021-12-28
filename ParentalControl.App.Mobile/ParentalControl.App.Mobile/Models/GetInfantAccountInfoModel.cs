using System;
using System.Collections.Generic;
using System.Text;

namespace ParentalControl.App.Mobile.Models
{
    public class GetInfantAccountInfoModel
    {
        public int ParentId { get; set; }
        public int InfantAccountId { get; set; }
        // Action = 1 (GetInfantAcountInfo)
        // Action = 2 (DeleteInfantAccount)
        public int Action { get; set; }
    }
}
