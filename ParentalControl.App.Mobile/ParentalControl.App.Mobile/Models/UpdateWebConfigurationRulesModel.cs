using System;
using System.Collections.Generic;
using System.Text;

namespace ParentalControl.App.Mobile.Models
{
    public class UpdateWebConfigurationRulesModel
    {
        public int WebConfigurationId { get; set; }
        public bool? WebConfigurationAccess { get; set; }
        public string CategoryName { get; set; }
        public int InfantAccountId { get; set; }
    }
}
