using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Web.Routing;
using NUnit.Framework;
using MvcRouteTester;

namespace myWall.Tests.Controllers
{
    public class TestBaseRoutes
    {
        public RouteCollection Routes;

        [SetUp]
        public void Setup()
        {
            Routes = RouteTable.Routes;
            myWall.RouteConfig.RegisterRoutes(Routes);

            RouteAssert.UseAssertEngine(new NunitAssertEngine());
        }

        [TearDown]
        public void TearDown()
        {
            RouteTable.Routes.Clear();
        }
    }
}
