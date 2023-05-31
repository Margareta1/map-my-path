using MapMyPathCore.Interfaces;
using MapMyPathCore.Services;
using MapMyPathLib;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MapMyPathCoreTests
{
    internal class RouteServiceTests
    {
        private RouteService _routeService;
        private Mock<IAccountService> _mockAccountService;
        private Mock<IRouteService> _mockRouteService;

        [SetUp]
        public void Setup()
        {
            _routeService = new RouteService();
            _mockAccountService = new Mock<IAccountService>();
            _mockRouteService = new Mock<IRouteService>();
        }

        [Test]
        public void CreateRoute_ReturnsString()
        {
            var username = "jim.doe@gmail.com";
            var result = _routeService.CreateRoute(username);
            Assert.IsInstanceOf<String>(result);
        }

        [Test]
        public void AddStoppingPoint_ReturnsTrue()
        {
            var username = "jim.doe@gmail.com";
            var temp = _routeService.CreateRoute(username);
            var result = _routeService.AddStoppingPoint(temp, 2.222, 3.333, 1);
            Assert.IsTrue(result);
        }

        [Test]
        public void GetRoutesForUser_ReturnsRoute()
        {
            var username = "jim.doe@gmail.com";
            var result = _routeService.GetRoutesForUser(username);
            Assert.IsInstanceOf<List<Route>>(result);
        }

        [Test]
        public void GetCoordinatesForRoute_ReturnsRoute()
        {
            string testRouteId = "af46889f-f270-4c1f-ca22-08db61eec07f";

            List<Coordinate> testCoordinates = new List<Coordinate>
            {
                new Coordinate {IdCoordinate = Guid.NewGuid(), Latitude = 458146920126336, Longitude = 15955815183679008, StoppingOrder = 1 },
                new Coordinate {IdCoordinate = Guid.NewGuid(), Latitude = 45806316249622184, Longitude = 15953583585778616, StoppingOrder = 2 },
            };

            _mockRouteService.Setup(service => service.GetCoordinatesForRoute(testRouteId.ToString())).Returns(testCoordinates);

            var returnedCoordinates = _routeService.GetCoordinatesForRoute(testRouteId.ToString());

            Assert.AreEqual(testCoordinates.Count, returnedCoordinates.Count);
            for (int i = 0; i < testCoordinates.Count; i++)
            {
                Assert.AreEqual(testCoordinates[i].Latitude, returnedCoordinates[i].Latitude);
                Assert.AreEqual(testCoordinates[i].Longitude, returnedCoordinates[i].Longitude);
                Assert.AreEqual(testCoordinates[i].StoppingOrder, returnedCoordinates[i].StoppingOrder);
            }
        }
    }
}