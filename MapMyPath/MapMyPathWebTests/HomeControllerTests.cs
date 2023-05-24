using MapMyPathWeb.Controllers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Build.Framework;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MapMyPathWebTests
{
    [TestFixture]
    internal class HomeControllerTests
    {
        private HomeController controller;
        [SetUp]
        public void Setup()
        {
            controller = new HomeController(new TestLogger<HomeController>());

        }

        [Test]
        public void Index_ReturnsViewResult()
        {
            var result = controller.Index();

            Assert.IsInstanceOf<ViewResult>(result);
            Assert.IsNotNull(result);
        }       
        [Test]
        public void Privacy_ReturnsViewResult()
        {
            var result = controller.Privacy();

            Assert.IsInstanceOf<ViewResult>(result);
            Assert.IsNotNull(result);

        }
        [Test]
        public void About_ReturnsViewResult()
        {
            var result = controller.About();

            Assert.IsInstanceOf<ViewResult>(result);
            Assert.IsNotNull(result);

        }
        [Test]
        public void LiveFeed_ReturnsViewResult()
        {
            var result = controller.LiveFeed();

            Assert.IsInstanceOf<ViewResult>(result); 
            Assert.IsNotNull(result);

        }

    }
}
