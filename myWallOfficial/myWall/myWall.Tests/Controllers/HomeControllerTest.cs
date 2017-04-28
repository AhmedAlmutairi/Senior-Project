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
        public void DefaultUrl_ShouldMapTo_Home_Index()
        {
            TestRouteMatch("~/", "Home", "Index");
        }

        [TestMethod]
        public void DefaultHomeUrl_ShouldMapTo_Home_Index()
        {
            TestRouteMatch("~/Home", "Home", "Index");
        }

        [TestMethod]
        public void DefaultUrlIndex_ShouldMapTo_Home_Index()
        {
            TestRouteMatch("~/Home/Index", "Home", "Index");
        }

        [TestMethod]
        public void IndexUrl_ShouldNotMapTo_Home_Index()
        {
            TestRouteFail("~/Index");
        }

        [TestMethod]
        public void BogusUrl_ShouldNotMap()
        {
            TestRouteFail("~/ThisUrlIsBogus");
        }

        [TestMethod]
        public void HomeAboutUrl_ShouldMapTo_Home_Index()
        {
            TestRouteMatch("~/Home/About", "Home", "About");
        }

        [TestMethod]
        public void AboutUrl_ShouldNotMap()
        {
            TestRouteFail("~/About");
        }

        [TestMethod]
        public void HomeContactUrl_ShouldMapTo_Home_Index()
        {
            TestRouteMatch("~/Home/Contact", "Home", "Contact");
        }

        [TestMethod]
        public void ContactUrl_ShouldNotMap()
        {
            TestRouteFail("~/About");
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


       

        [TestMethod]
        public void Wall()
        {
            // Arrange
            HomeController controller = new HomeController();

            // Act
            ViewResult result = controller.Wall(12) as ViewResult;

            // Assert
            Microsoft.VisualStudio.TestTools.UnitTesting.Assert.IsNotNull(result);
        }

        

        [TestMethod]
        public void Create()
        {
            // Arrange
            HomeController controller = new HomeController();

            // Act
            ViewResult result = controller.Create() as ViewResult;

            // Assert
            Microsoft.VisualStudio.TestTools.UnitTesting.Assert.IsNotNull(result);
        }

        [TestMethod]
        public void CreateWall()
        {
            // Arrange
            HomeController controller = new HomeController();

            // Act
            ViewResult result = controller.CreateWall() as ViewResult;

            // Assert
            Microsoft.VisualStudio.TestTools.UnitTesting.Assert.IsNotNull(result);
        }

        [TestMethod]
        public void Delete()
        { 

            // Arrange
            HomeController controller = new HomeController();

            // Act

            ViewResult result = controller.DeleteConfirmed(11) as ViewResult;

           
            // Assert
            Microsoft.VisualStudio.TestTools.UnitTesting.Assert.IsNotNull(result);
        }

        [TestMethod]
        public void DeletePost()
        {
            // Arrange
            HomeController controller = new HomeController();

            // Act

            ViewResult result = controller.DeletePost(11) as ViewResult;

            // Assert
            Microsoft.VisualStudio.TestTools.UnitTesting.Assert.IsNotNull(result);
        }

        
        
    }
    
}
