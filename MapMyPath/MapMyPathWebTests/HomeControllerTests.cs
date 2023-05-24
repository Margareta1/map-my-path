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
            var result = controller.Index();

            Assert.IsInstanceOf<ViewResult>(result);
        }

    }
}
