using System;
using System.Collections.Generic;
using System.Text;

namespace ParentalControl.App.Mobile.Models
{
    public class CreateInfantAccountModel
    {
        public string InfantName { get; set; }
        public string InfantGender { get; set; }
        public int ParentId { get; set; }
    }
}
