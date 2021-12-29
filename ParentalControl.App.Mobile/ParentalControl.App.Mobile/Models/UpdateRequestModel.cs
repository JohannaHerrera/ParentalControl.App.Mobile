using System;
using System.Collections.Generic;
using System.Text;

namespace ParentalControl.App.Mobile.Models
{
    public class UpdateRequestModel
    {
        public int RequestId { get; set; }
        public int RequestAction { get; set; } // 1: Aprobar, 2: Desaprobar
    }
}
