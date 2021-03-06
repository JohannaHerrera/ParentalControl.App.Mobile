using System;
using System.Collections.Generic;
using System.Text;

namespace ParentalControl.App.Mobile.Models
{
    public class ListInfantAccountModel
    {
        public int InfantAccountId { get; set; }
        public string InfantName { get; set; }
        public string InfantGender { get; set; }
        public DateTime InfantCreationDate { get; set; }
        public int ParentId { get; set; }
    }
}
