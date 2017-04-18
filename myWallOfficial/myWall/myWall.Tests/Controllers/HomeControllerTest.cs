using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using myWall;
using myWall.Controllers;
using NUnit.Framework;

namespace myWall.Tests.Controllers
{
    [TestClass]
    public class HomeControllerTest
    {
        [SetUp]
        public void Setup()
        {
            //Routes = RouteTable.Routes;
            //myWall.RouteConfig.RegisterRoutes(Routes);

            //RouteAssert.UseAssertEngine(new NunitAssertEngine());
        }


        [OneTimeSetUp]
        public void RunThisBeforeEveryTestMethod()
        {

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

        [OneTimeTearDown]
        public void RunThisAfterEveryTestMethod()
        {

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

        [TestMethod]
        public void UserProfile()
        {
            // Arrange
            HomeController controller = new HomeController();

            // Act
            ViewResult result = controller.UserProfile("95b0702f-7c4e-4c12-8674-e25224fc2209") as ViewResult;

            // Assert
            Microsoft.VisualStudio.TestTools.UnitTesting.Assert.IsNotNull(result);
        }
    }
}
