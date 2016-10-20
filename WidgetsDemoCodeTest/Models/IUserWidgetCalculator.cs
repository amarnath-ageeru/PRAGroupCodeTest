using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WidgetsDemoCodeTest.Models
{
    public interface IUserWidgetCalculator
    {
        double TotalBasePrice { get; set; }
        double TotalPrice { get; set; }
        double TotalDiscountPrice { get; set; }
        double TotalSalesTax { get; set; }
    }
}