using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WidgetsDemoCodeTest.Models
{
    public class SubWidget : ISubWidget
    {
        public string Topic { get; set; }        
        public string Description { get; set; }
        public string ImagePath { get; set; }
    }
}