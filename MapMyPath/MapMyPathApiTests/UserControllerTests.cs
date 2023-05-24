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

namespace MapMyPathApiTests
{
    [TestFixture]
    internal class UserControllerTests
    {
        private UserController userController;


        [SetUp]
        public void Setup()
        {
            // Initialize the mock account service and the user controller
            userController = new UserController();
        }

        [Test]
        public void GetAllUsers_ReturnsListOfUsers()
        {

            // Act
            var users = userController.GetAllUsers();
            var serializedData = JsonConvert.SerializeObject(users.Value);
            var deserializedList = JsonConvert.DeserializeObject<List<AppUser>>(serializedData);

            // Assert
            Assert.IsInstanceOf<JsonResult>(users);
            Assert.IsInstanceOf(typeof(List<AppUser>), deserializedList);
        }

        [Test]
        public void AddUser_ReturnsSuccess()
        {
            //Arrange
            var appUser = new AppUser { Id = new Guid(), FirstName = "Jim", LastName = "Doe", UserName = "jimmy.doe@gmail.com", Password = "john22", CreatedAt = DateTime.Now, IsDeleted = 0 };

            //Act

            var result = userController.AddUser(appUser.UserName, appUser.Password, appUser.FirstName, appUser.LastName);
            var resultSerialized = JsonConvert.SerializeObject(result.Value);
            var resultDeserialized = JsonConvert.DeserializeObject<string>(resultSerialized);

            //Assert
            Assert.That(resultDeserialized == "Success");

        }


    }
}
