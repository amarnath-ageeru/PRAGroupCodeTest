using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WidgetsDemoCodeTest.Models;

namespace WidgetsDemoCodeTest.Controllers
{
    public class WidgetsController : Controller
    {     
        /// <summary>
        /// Default Action method - Home
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// Binds data and displays UserWidgetDashboard.cshtml page with widgets
        /// </summary>
        /// <returns>UserWidgetDashboard view</returns>
        public ActionResult UserWidgetDashboard()
        {
            ViewBag.TotalBasePrice = "0.0";
            ViewBag.TotalDiscountedPrice = "0.0";
            ViewBag.TotalSalesTaxPrice = "0.0";
            ViewBag.TotalPriceToBePaid = "0.0";

            ViewBag.Widgets = GetWidgetData();
            ViewBag.States = LoadStates();
            return View();
        }
        
        /// <summary>
        /// Method to calculate Discount for each Widget, 
        /// for ex: base price of one surface computer is $1000 and discount of 10%, it returns $900 after discount
        /// </summary>
        /// <param name="basePrice"></param>
        /// <param name="discount"></param>
        /// <returns>Discounted price of each device</returns>
        public double CalculateDiscount(double basePrice, double discount)
        {
            double DiscountPrice = (discount / 100) * basePrice;
            return (basePrice - DiscountPrice);
        }

        /// <summary>
        /// Calculates and returns the Base price, Discounted price, Sales Tax and Total Price for each widget
        /// </summary>
        /// <param name="widget"></param>
        /// <param name="StateTax"></param>
        /// <returns>Collection variable with Base price, Discounted price, Sales Tax and Total Price for each widget</returns>
        public Dictionary<string, double> CalculateTotalWidgetPrice(string[] widget, double StateTax)
        {
            Dictionary<string, double> dictWidgetDetails = new Dictionary<string, double>();

            int WidgetQuanity = 0;
            double WidgetBasePrice = 0;
            double WidgetDiscount = 0;
            double WidgetDiscountPrice = 0;
            double WidgetTotalPrice = 0;

            foreach (string item in widget)
            {
                string WidgetInfo1 = item.Substring(item.IndexOf('=') + 1);
                string WidgetInfo2 = item.Substring(0, item.IndexOf('=')-1);

                switch (WidgetInfo2)
                {
                    case "txt_Widget":
                        WidgetQuanity = Convert.ToInt32(WidgetInfo1);
                        break;

                    case "dis_Widget":
                        WidgetDiscount = 10;
                        break;

                    case "basePrice_Widget":
                        WidgetBasePrice = Convert.ToDouble(WidgetInfo1);
                        break;
                }
            }

            WidgetDiscountPrice = CalculateDiscount(WidgetBasePrice, WidgetDiscount);

            WidgetTotalPrice = WidgetQuanity * WidgetDiscountPrice;

            dictWidgetDetails.Add("BasePrice", WidgetBasePrice * WidgetQuanity);
            dictWidgetDetails.Add("StateTax", ((StateTax / 100) * WidgetTotalPrice));
            dictWidgetDetails.Add("TotalPrice", ((StateTax / 100) * WidgetTotalPrice) + WidgetTotalPrice);
            dictWidgetDetails.Add("DiscountPrice", (WidgetBasePrice * WidgetQuanity) - (WidgetDiscountPrice * WidgetQuanity));

            return dictWidgetDetails;
        }

        /// <summary>
        /// Method which will be executed upon user clicking on Calculate button on view
        /// </summary>
        /// <param name="nvc"></param>
        /// <returns></returns>
        public ActionResult CalculateTotalPrice(FormCollection nvc)
        {
            Dictionary<string, double> dictWidget1Details = new Dictionary<string, double>();
            Dictionary<string, double> dictWidget2Details = new Dictionary<string, double>();
            Dictionary<string, double> dictWidget3Details = new Dictionary<string, double>();
            Dictionary<string, double> dictWidget4Details = new Dictionary<string, double>();

            double BasePrice = 0.0;
            double DiscountPrice = 0.0;
            double TotalPriceCost = 0.0;
            double SalesTax = 0.0;

            string[] widget1 = (from key in nvc.AllKeys
                                from value in nvc.GetValues(key)
                                where key.Contains("Widget1")
                                select string.Format("{0}={1}", HttpUtility.UrlEncode(key), HttpUtility.UrlEncode(value))).ToArray();

            string[] widget2 = (from key in nvc.AllKeys
                                from value in nvc.GetValues(key)
                                where key.Contains("Widget2")
                                select string.Format("{0}={1}", HttpUtility.UrlEncode(key), HttpUtility.UrlEncode(value))).ToArray();

            string[] widget3 = (from key in nvc.AllKeys
                                from value in nvc.GetValues(key)
                                where key.Contains("Widget3")
                                select string.Format("{0}={1}", HttpUtility.UrlEncode(key), HttpUtility.UrlEncode(value))).ToArray();

            string[] widget4 = (from key in nvc.AllKeys
                                from value in nvc.GetValues(key)
                                where key.Contains("Widget4")
                                select string.Format("{0}={1}", HttpUtility.UrlEncode(key), HttpUtility.UrlEncode(value))).ToArray();

            string[] StateTax = (from key in nvc.AllKeys
                                 from value in nvc.GetValues(key)
                                 where key.Contains("ddlState")
                                 select string.Format("{0}={1}", HttpUtility.UrlEncode(key), HttpUtility.UrlEncode(value))).ToArray();


            double dblStateTax = Convert.ToDouble(StateTax[0].Substring(StateTax[0].IndexOf('=') + 1));

            if (widget1.Contains("chk_Widget1=true"))
            {
                dictWidget1Details = CalculateTotalWidgetPrice(widget1, dblStateTax);
                BasePrice = BasePrice + dictWidget1Details["BasePrice"];
                SalesTax = SalesTax + dictWidget1Details["StateTax"];
                TotalPriceCost = TotalPriceCost + dictWidget1Details["TotalPrice"];
                DiscountPrice = DiscountPrice + dictWidget1Details["DiscountPrice"];
            }
            if (widget2.Contains("chk_Widget2=true"))
            {
                dictWidget2Details = CalculateTotalWidgetPrice(widget2, dblStateTax);
                BasePrice = BasePrice + dictWidget2Details["BasePrice"];
                SalesTax = SalesTax + dictWidget2Details["StateTax"];
                TotalPriceCost = TotalPriceCost + dictWidget2Details["TotalPrice"];
                DiscountPrice = DiscountPrice + dictWidget2Details["DiscountPrice"];
            }
            if (widget3.Contains("chk_Widget3=true"))
            {
                dictWidget3Details = CalculateTotalWidgetPrice(widget3, dblStateTax);
                BasePrice = BasePrice + dictWidget3Details["BasePrice"];
                SalesTax = SalesTax + dictWidget3Details["StateTax"];
                TotalPriceCost = TotalPriceCost + dictWidget3Details["TotalPrice"];
                DiscountPrice = DiscountPrice + dictWidget3Details["DiscountPrice"];
            }
            if (widget4.Contains("chk_Widget4=true"))
            {
                dictWidget4Details = CalculateTotalWidgetPrice(widget4, dblStateTax);
                BasePrice = BasePrice + dictWidget4Details["BasePrice"];
                SalesTax = SalesTax + dictWidget4Details["StateTax"];
                TotalPriceCost = TotalPriceCost + dictWidget4Details["TotalPrice"];
                DiscountPrice = DiscountPrice + dictWidget4Details["DiscountPrice"];
            }

            UserWidgetCalculator objCalcular = new UserWidgetCalculator();

            objCalcular.TotalBasePrice = BasePrice;
            objCalcular.TotalDiscountPrice = DiscountPrice;
            objCalcular.TotalSalesTax = SalesTax;
            objCalcular.TotalPrice = TotalPriceCost;
            
            ViewBag.TotalBasePrice = Convert.ToString(BasePrice);
            ViewBag.TotalDiscountedPrice = Convert.ToString(DiscountPrice);
            ViewBag.TotalSalesTaxPrice = Convert.ToString(SalesTax);
            ViewBag.TotalPriceToBePaid = Convert.ToString(TotalPriceCost);

            ViewBag.Widgets = GetWidgetData();
            ViewBag.States = LoadStates();

            //RedirectToAction("UserWidgetDashboard", "Widgets");
            return View("UserWidgetDashboard");
        }

        /// <summary>
        /// Populates States in the Drop down
        /// </summary>
        /// <returns>Collection of states</returns>
        public List<SelectListItem> LoadStates()
        {
            //Creating generic list
            List<SelectListItem> lstStates = new List<SelectListItem>()
            {
                new SelectListItem { Text = "Florida", Value = "0" },
                new SelectListItem { Text = "Texas", Value = "0" },
                new SelectListItem { Text = "Arizona",Value = "5" },
                new SelectListItem { Text = "Arkansas", Value = "5" },
                new SelectListItem { Text = "California", Value = "5" },
                new SelectListItem { Text = "Colorado", Value = "5" },
                new SelectListItem { Text = "Connecticut", Value = "5" },
                new SelectListItem { Text = "Delaware", Value = "5" },
                new SelectListItem { Text = "Georgia", Value = "5" },
                new SelectListItem { Text = "Hawaii", Value = "5" },
                new SelectListItem { Text = "Idaho", Value = "5" },
                new SelectListItem { Text = "Illinois", Value = "5" },
                new SelectListItem { Text = "Indiana", Value = "5" },
                new SelectListItem { Text = "Iowa", Value = "5" },
                new SelectListItem { Text = "Kansas", Value = "5" },
                new SelectListItem { Text = "Kentucky", Value = "5" },
                new SelectListItem { Text = "Louisiana", Value = "5" },
                new SelectListItem { Text = "Maine", Value = "5" },
                new SelectListItem { Text = "Maryland", Value = "5" },
                new SelectListItem { Text = "Massachusetts", Value = "5" },
                new SelectListItem { Text = "Michigan", Value = "5" },
                new SelectListItem { Text = "Minnesota", Value = "5" },
                new SelectListItem { Text = "Mississippi", Value = "5" },
                new SelectListItem { Text = "Missouri", Value = "5" },
            };

            return lstStates;
        }

        /// <summary>
        /// Method to load the Widgets on screen with data
        /// </summary>
        /// <returns></returns>
        public List<IWidget> GetWidgetData()
        {
            var WidgetBoard = new List<IWidget>
            {
                new WidgetBoard()
                {
                 ClassName = "high", HeaderText = "Microsoft Surface Book", BasePrice = 1349 ,
                SubWidget = new SubWidget { Topic = "Microsoft Surface Book - 128GB", Description = "", ImagePath = @"\Images\Surface.png"}, DiscountIndicator= true, WidgetName="Widget1", Discount = 10,               
                },
                 new WidgetBoard()
                {
                 ClassName = "medium", HeaderText = "Apple iPad Pro", BasePrice = 599 ,
                SubWidget = new SubWidget { Topic = "Apple iPad Pro 9.7-inch Display", Description = "" , ImagePath = @"\Images\iPadPro.PNG"},DiscountIndicator= false, WidgetName="Widget2", Discount = 0,
                },
                new WidgetBoard()
                {
                 ClassName = "low", HeaderText = "iPhone 7", BasePrice = 749 ,
                SubWidget = new SubWidget { Topic = "iPhone 7 128 GB Jet Black Display", Description = "" , ImagePath = @"\Images\iphone7.PNG"},DiscountIndicator= true, WidgetName="Widget3", Discount = 10, 
                },              
                new WidgetBoard()
                {
                  ClassName = "high", HeaderText = "Samsung Galaxy S7", BasePrice = 599 ,
                 SubWidget = new SubWidget { Topic = "Samsung Galaxy S7 ‑ 32 GB", Description = "" , ImagePath = @"\Images\SamsungS7.PNG"},DiscountIndicator= false, WidgetName="Widget4", Discount = 0,
                },                
            };
            return WidgetBoard.OrderBy(p => p.SortOrder).ToList();
        }
    }
}
