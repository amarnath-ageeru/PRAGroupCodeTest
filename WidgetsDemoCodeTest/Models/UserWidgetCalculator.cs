using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WidgetsDemoCodeTest.Models
{
    public class UserWidgetCalculator : IUserWidgetCalculator
    {
        public double TotalBasePrice { get; set; } 
        public double TotalPrice { get; set; }
        public double TotalDiscountPrice { get; set; }
        public double TotalSalesTax { get; set; }
    }
}