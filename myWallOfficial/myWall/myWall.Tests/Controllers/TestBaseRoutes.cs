using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Web;
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
    public class TestBaseRoutes
    {
        protected HttpContextBase CreateHttpContext(string targetUrl = null, string httpMethod = "GET")
        {
            // create the mock request
            Mock<HttpRequestBase> mockRequest = new Mock<HttpRequestBase>();
            mockRequest.Setup(m => m.AppRelativeCurrentExecutionFilePath).Returns(targetUrl);
            mockRequest.Setup(m => m.HttpMethod).Returns(httpMethod);

            // create the mock response
            Mock<HttpResponseBase> mockResponse = new Mock<HttpResponseBase>();
            mockResponse.Setup(m => m.ApplyAppPathModifier(
                It.IsAny<string>())).Returns<string>(s => s);

            // create the mock context, using the request and response
            Mock<HttpContextBase> mockContext = new Mock<HttpContextBase>();
            mockContext.Setup(m => m.Request).Returns(mockRequest.Object);
            mockContext.Setup(m => m.Response).Returns(mockResponse.Object);

            // return the mocked context
            return mockContext.Object;
        }

        protected void TestRouteMatch(string url, string controller, string action,
                                    object routeProperties = null, string httpMethod = "GET")
        {
            // Arrange
            RouteCollection routes = new RouteCollection();
            RouteConfig.RegisterRoutes(routes);

            // Act - Process the route
            RouteData result = routes.GetRouteData(CreateHttpContext(url, httpMethod));

            // Assert
            Microsoft.VisualStudio.TestTools.UnitTesting.Assert.IsNotNull(result);
            Microsoft.VisualStudio.TestTools.UnitTesting.Assert.IsTrue(TestIncomingRouteResult(result, controller, action, routeProperties));
        }

        protected bool TestIncomingRouteResult(RouteData routeResult, string controller,
                                             string action, object propertySet = null)
        {
            Func<object, object, bool> valCompare = (v1, v2) =>
              {
                  return StringComparer.InvariantCultureIgnoreCase.Compare(v1, v2) == 0;
              };
            bool result = valCompare(routeResult.Values["controller"], controller)
                && valCompare(routeResult.Values["action"], action);

            if( propertySet != null)
            {
                PropertyInfo[] propInfo = propertySet.GetType().GetProperties();
                foreach (PropertyInfo pi in propInfo)
                {
                    if( !( routeResult.Values.ContainsKey(pi.Name)
                        && valCompare(routeResult.Values[pi.Name],
                        pi.GetValue(propertySet,null))))
                    {
                        result = false;
                        break;
                    }
                }
            }
            return result;
        }

        protected void TestRouteFail(string url)
        {
            // Arrange
            RouteCollection routes = new RouteCollection();
            RouteConfig.RegisterRoutes(routes);

            // Act - process the route
            RouteData result = routes.GetRouteData(CreateHttpContext(url));

            // Assert
            Microsoft.VisualStudio.TestTools.UnitTesting.Assert.IsTrue(result == null || result.Route == null);
        }

        [SetUp]
        public void Setup()
        {
            //Routes = RouteTable.Routes;
            //myWall.RouteConfig.RegisterRoutes(Routes);

            //RouteAssert.UseAssertEngine(new NunitAssertEngine());
        }

        [TearDown]
        public void TearDown()
        {
            RouteTable.Routes.Clear();
        }

        [OneTimeSetUp]
        public void RunThisBeforeEveryTestMethod()
        {

        }

        [OneTimeTearDown]
        public void RunThisAfterEveryTestMethod()
        {

        }
    }
}
