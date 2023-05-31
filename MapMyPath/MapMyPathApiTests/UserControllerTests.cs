using MapMyPathApi.Controllers;
using MapMyPathCore.Services;
using MapMyPathLib;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moq;
using MapMyPathCore.Interfaces;

namespace MapMyPathApiTests
{
    [TestFixture]
    internal class UserControllerTests
    {
        private UserController _userController;
        private Mock<IAccountService> _mockAccountService;
        private Mock<IRouteService> _mockRouteService;

        [SetUp]
        public void Setup()
        {
            _mockAccountService = new Mock<IAccountService>();
            _mockRouteService = new Mock<IRouteService>();
            _userController = new UserController(_mockAccountService.Object, _mockRouteService.Object);
        }

        [Test]
        public void AddUser_ReturnsSuccess_WhenUserDoesntExist()
        {
            // Arrange
            var username = "User1";
            var password = "Password1";
            var firstName = "John";
            var lastName = "Doe";

            _mockAccountService.Setup(service => service.ExistsInDatabase(username))
                .Returns(false);
            _mockAccountService.Setup(service => service.AddUser(It.IsAny<AppUser>()))
                .Verifiable();

            var result = _userController.AddUser(username, password, firstName, lastName);

            Assert.That(result.Value, Is.EqualTo("Success"));
            _mockAccountService.Verify();
        }

        [Test]
        public void AddUser_ReturnsSuccess()
        {
            var appUser = new AppUser { Id = new Guid(), FirstName = "Jim", LastName = "Doe", UserName = "jimmy.do@gmail.com", Password = "john22", CreatedAt = DateTime.Now, IsDeleted = 0 };

            var result = _userController.AddUser(appUser.UserName, appUser.Password, appUser.FirstName, appUser.LastName);
            var resultSerialized = JsonConvert.SerializeObject(result.Value);
            var resultDeserialized = JsonConvert.DeserializeObject<string>(resultSerialized);

            Assert.That(resultDeserialized == "Success");
        }

        [Test]
        public void ValidateUser_ReturnsSuccess()
        {
            var username = "jimmy.do@gmail.com";
            var password = "john22";

            _mockAccountService.Setup(service => service.ValidateUser(username, password)).Returns(true);

            var result = _userController.ValidateUser(username, password);

            Assert.AreEqual("Success", result.Value);
        }
    }
}