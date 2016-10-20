using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WidgetsDemoCodeTest.Models
{
    public class NoticeBoard : IWidget
    {
        public int SortOrder { get; set; }
        public string WidgetName { get; set; }
        public string ClassName { get; set; }
        public int BasePrice { get; set; }   
        public string HeaderText { get; set; }
        public bool ItemSelected { get; set; }
        public int Quantity { get; set; }        
        public bool DiscountIndicator { get; set; }
        public int Discount { get; set; } 
        public ISubWidget SubWidget { get; set; }

    }
}