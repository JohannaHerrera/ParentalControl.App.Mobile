using System;
using System.Collections.Generic;
using System.Text;

namespace ParentalControl.App.Mobile.Models
{
    public class UpdateWebConfigurationRulesModel
    {
        public int WebConfigurationId { get; set; }
        public bool? WebConfigurationAccess { get; set; }
        public int CategoryId { get; set; }
        public int InfantAccountId { get; set; }
    }
}
