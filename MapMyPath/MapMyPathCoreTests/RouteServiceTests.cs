using MapMyPathCore.Services;
using MapMyPathLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MapMyPathCoreTests
{
    internal class RouteServiceTests
    {

        private RouteService service;

        [SetUp]
        public void Setup()
        {
            service = new RouteService();
        }

        [Test]
        public void CreateRoute_ReturnsString()
        {
            var username = "jim.doe@gmail.com";
            var result = service.CreateRoute(username);
            Assert.IsInstanceOf<String>(result);
        }

        [Test]
        public void AddStoppingPoint_ReturnsTrue()
        {
            var username = "jim.doe@gmail.com";
            var temp = service.CreateRoute(username);
            var result = service.AddStoppingPoint(temp, 2.222, 3.333, 1); 
            Assert.IsTrue(result);
        }

        [Test]
        public void GetRoutesForUser_ReturnsRoute()
        {
            var username = "jim.doe@gmail.com";
            var result = service.GetRoutesForUser(username);
            Assert.IsInstanceOf<List<Route>>(result);
        }
    }
}
