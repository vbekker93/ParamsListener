using System;
using System.Collections.Generic;
using System.Web.Mvc;
using Infrastructure.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ParamsListenerWebPortal.Helpers;

namespace UnitTestsWebPortal
{
    [TestClass]
    public class TestPortal
    {
        [TestMethod]
        public void TestCalculateColor()
        {
            MvcHtmlString result = HtmlHelperExtensions.CalculateColor(null, 1);
            Assert.IsTrue(!result.Equals(string.Empty));
            Assert.AreEqual(result.ToString(), "background-color:rgba(0,0, 0, 1); color:rgb(255,255, 255);");

            result = HtmlHelperExtensions.CalculateColor(null, -1);
            Assert.IsTrue(!result.Equals(string.Empty));
            Assert.AreEqual(result.ToString(), "background-color:rgba(255,140, 0, 1); color:rgb(255,255, 255);");

            result = HtmlHelperExtensions.CalculateColor(null, 0.5);
            Assert.IsTrue(!result.Equals(string.Empty));
            Assert.AreEqual(result.ToString(), "background-color:rgba(0,0, 0, 0.5); color:rgb(0,0, 0);");

            result = HtmlHelperExtensions.CalculateColor(null, -0.5);
            Assert.IsTrue(!result.Equals(string.Empty));
            Assert.AreEqual(result.ToString(), "background-color:rgba(255,140, 0, 0.5); color:rgb(0,0, 0);");
        }

        private readonly string UrlBase = "https://localhost:44351";

        [TestMethod]
        public void TestGetTableEntities()
        {
            System.Threading.Tasks.Task<List<ParamsEntity>> data = WebApiHelper.ExecuteWebApiRequest<List<ParamsEntity>>(UrlBase, "api/ParamsEntities", WebApiHelper.HttpMethod.GET);
            data.Wait();
            Assert.IsNotNull(data.Result);
            Assert.AreEqual(data.IsFaulted, false);
        }
    }
}
