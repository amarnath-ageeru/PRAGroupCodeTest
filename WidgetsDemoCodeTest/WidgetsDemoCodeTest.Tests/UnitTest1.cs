using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WidgetsDemoCodeTest.Models;
using WidgetsDemoCodeTest.Controllers;
using System.Collections.Generic;
using System.Web.Mvc;

namespace WidgetsDemoCodeTest.Tests
{
    [TestClass]
    public class UnitTest1
    {
        /// <summary>
        /// Test Method to perform unit test on GetWidgetData method - which loads Widget data on screen
        /// </summary>
        [TestMethod]
        public void TestMethod1()
        {
            List<IWidget> widget = new List<IWidget>();
            WidgetsController widgetController = new WidgetsController();
            widget = widgetController.GetWidgetData();
            Assert.AreEqual(widget.Count, 4);
        }

        /// <summary>
        /// Test Method to perform unit test on LoadStates method - which populates the states in State drop down
        /// </summary>
        [TestMethod]
        public void TestMethod2()
        {
            List<SelectListItem> lstStates = new List<SelectListItem>();
            WidgetsController widgetController = new WidgetsController();
            lstStates = widgetController.LoadStates();
            Assert.AreEqual(lstStates.Count, 24);
        }

        /// <summary>
        /// Test Method to perform unit test on CalculateDiscount method - which Calculates price after discount applied
        /// </summary>
        [TestMethod]
        public void TestMethod3()
        {
            double Discount;
            WidgetsController widgetController = new WidgetsController();
            Discount = widgetController.CalculateDiscount(500.0, 10.0);
            double value = 500.0 - Discount;
            Assert.AreEqual(value, 50.0);
        }

        /// <summary>
        /// Test method to perform unit test on CalculateTotalWidgetPrice - which gives the Widget price details
        /// </summary>
        [TestMethod]
        public void TestMethod4()
        {
            Dictionary<string, double> dictWidgetDetails = new Dictionary<string, double>();
            string[] widget = new string[4];
            widget[0] = "txt_Widget1=2";
            widget[1] = "chk_Widget1=true";
            widget[2] = "basePrice_Widget1=500.0";
            widget[3] = "dis_Widget1=10.0";

            WidgetsController widgetController = new WidgetsController();
            dictWidgetDetails = widgetController.CalculateTotalWidgetPrice(widget, 5.0);
            Assert.AreEqual(dictWidgetDetails["BasePrice"], 1000);
            Assert.AreEqual(dictWidgetDetails["StateTax"], 45);
            Assert.AreEqual(dictWidgetDetails["TotalPrice"], 945);
            Assert.AreEqual(dictWidgetDetails["DiscountPrice"], 100);
        }
    }
}
