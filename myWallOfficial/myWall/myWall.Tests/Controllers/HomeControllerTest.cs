using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Web;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Web.Routing;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using MvcRouteTester;
using NUnit.Framework;
using myWall;
using myWall.Controllers;

namespace myWall.Tests.Controllers
{
    [TestClass]
    public class HomeControllerTest:TestBaseRoutes
    {
        [TestMethod]
        public void DefaultURL_ShouldMapTo_Home_Index()
        {
            TestRouteMatch("~/", "Home", "Index");
        }

        [TestMethod]
        public void Index()
        {
            // Arrange
            HomeController controller = new HomeController();

            // Act
            ViewResult result = controller.Index() as ViewResult;

            // Assert
            Microsoft.VisualStudio.TestTools.UnitTesting.Assert.IsNotNull(result);
        }

        [TestMethod]
        public void About()
        {
            // Arrange
            HomeController controller = new HomeController();

            // Act
            ViewResult result = controller.About() as ViewResult;

            // Assert
            Microsoft.VisualStudio.TestTools.UnitTesting.Assert.AreEqual("Your application description page.", result.ViewBag.Message);
        }

        [TestMethod]
        public void Contact()
        {
            // Arrange
            HomeController controller = new HomeController();

            // Act
            ViewResult result = controller.Contact() as ViewResult;

            // Assert
            Microsoft.VisualStudio.TestTools.UnitTesting.Assert.IsNotNull(result);
        }
    }
}
