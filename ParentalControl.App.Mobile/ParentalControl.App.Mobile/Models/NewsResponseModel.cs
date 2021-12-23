using System;
using System.Collections.Generic;
using System.Text;

namespace ParentalControl.App.Mobile.Models
{
    public class NewsResponseModel
    {
        public List<NewsModel> NewsModelList { get; set; }
        public string MessageError { get; set; }
    }
}
