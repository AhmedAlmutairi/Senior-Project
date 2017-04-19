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
using NUnit.Framework;

namespace myWall.Tests.Controllers
{
    [TestClass]
    public class HomeControllerTest:TestBaseRoutes
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
<<<<<<< HEAD
        }

        [OneTimeTearDown]
        public void RunThisAfterEveryTestMethod()
        {

=======
>>>>>>> 483a5d2bbd039ce121ce9115e4a6b3e757b6df5e
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
<<<<<<< HEAD
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
=======

        }
            [TestMethod]
        public void DefaultUrl_ShouldMapTo_Home_Whiteboard()
        {
            TestRouteMatch("~/", "Home", "Whiteboard");
        }

        [TestMethod]
        public void DefaultHomeUrl_ShouldMapTo_Home_Whiteboard()
        {
            TestRouteMatch("~/Home", "Home", "Whiteboard");
        }

        [TestMethod]
        public void DefaultUrlIndex_ShouldMapTo_Home_Whiteboard()
        {
            TestRouteMatch("~/Home/Whiteboard", "Home", "Whiteboard");
        }

        [TestMethod]
        public void WhiteboardUrl_ShouldNotMapTo_Home_Whiteboard()
        {
            TestRouteFail("~/Index");
        }

        [TestMethod]
        public void BogusUrl_ShouldNotMapWall()
        {
            TestRouteFail("~/ThisUrlIsBogus");
        }

        [TestMethod]
        public void ChessUrl_ShouldMapTo_Home_Chess()
        {
            TestRouteMatch("~/Home/Chess", "Home", "Chess");
        }

        [TestMethod]
        public void ChessUrl_ShouldNotMapToChess()
        {
            TestRouteFail("~/Chess");
        }

        [TestMethod]
        public void HomeContactUrl_ShouldMapTo_Home_Wall()
        {
            TestRouteMatch("~/Home/Wall", "Home", "Wall");
        }

        [TestMethod]
        public void WallUrl_ShouldNotMapTOWALL()
        {
            TestRouteFail("~/Wall");
        }

        [TestMethod]
        public void Wall()
>>>>>>> 483a5d2bbd039ce121ce9115e4a6b3e757b6df5e
        {
            // Arrange
            HomeController controller = new HomeController();

            // Act
<<<<<<< HEAD
            ViewResult result = controller.DeleteConfirmed(11) as ViewResult;
=======
            ViewResult result = controller.AWall() as ViewResult;
>>>>>>> 483a5d2bbd039ce121ce9115e4a6b3e757b6df5e

            // Assert
            Microsoft.VisualStudio.TestTools.UnitTesting.Assert.IsNotNull(result);
        }

        [TestMethod]
<<<<<<< HEAD
        public void DeletePost()
=======
        public void Chess()
>>>>>>> 483a5d2bbd039ce121ce9115e4a6b3e757b6df5e
        {
            // Arrange
            HomeController controller = new HomeController();

            // Act
<<<<<<< HEAD
            ViewResult result = controller.DeletePost(11) as ViewResult;

            // Assert
            Microsoft.VisualStudio.TestTools.UnitTesting.Assert.IsNotNull(result);
        }

        [TestMethod]
        public void UserProfile()
=======
            ViewResult result = controller.Chess() as ViewResult;

            // Assert
            Microsoft.VisualStudio.TestTools.UnitTesting.Assert.AreEqual("Your application description page.", result.ViewBag.Message);
        }

        [TestMethod]
        public void Whiteboard()
>>>>>>> 483a5d2bbd039ce121ce9115e4a6b3e757b6df5e
        {
            // Arrange
            HomeController controller = new HomeController();

            // Act
<<<<<<< HEAD
            ViewResult result = controller.UserProfile("95b0702f-7c4e-4c12-8674-e25224fc2209") as ViewResult;
=======
            ViewResult result = controller.Whiteboard() as ViewResult;
>>>>>>> 483a5d2bbd039ce121ce9115e4a6b3e757b6df5e

            // Assert
            Microsoft.VisualStudio.TestTools.UnitTesting.Assert.IsNotNull(result);
        }
    }


    
}
