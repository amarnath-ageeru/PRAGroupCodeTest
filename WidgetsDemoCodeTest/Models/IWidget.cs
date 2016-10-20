using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WidgetsDemoCodeTest.Models
{
  public  interface IWidget
    {
        int SortOrder { get; set; }
        string ClassName { get; set; }
        int BasePrice { get; set; }
        string HeaderText { get; set; }
        bool ItemSelected { get; set; }
        int Quantity { get; set; }
        string WidgetName { get; set; }
        bool DiscountIndicator { get; set; }
        int Discount { get; set; } 
        ISubWidget SubWidget { get; set; }
    }
}
