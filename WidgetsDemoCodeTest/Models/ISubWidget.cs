using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WidgetsDemoCodeTest.Models
{
    public interface ISubWidget
    {
        string Topic { get; set; }
        string Description { get; set; }
        string ImagePath { get; set; }
    }
}
