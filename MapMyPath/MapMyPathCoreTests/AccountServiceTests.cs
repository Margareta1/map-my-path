using MapMyPathCore.Services;
using MapMyPathLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace MapMyPathCoreTests
{
    [TestFixture]
    internal class AccountServiceTests
    {

        private AccountService service;


        [SetUp]
        public void Setup()
        {
            // Initialize the mock account service and the user controller
            service = new AccountService();
        }
        

        [Test]
        public void GetUsers_ReturnsList()
        {
            var users = service.GetUsers();
            Assert.IsInstanceOf(typeof(List<AppUser>), users);

        }

        
        [Test]
        public void GetUserByUsername_ReturnsAppUser()
        {
            var username = "john.doe@gmail.com";
            var user = service.GetUserByUsername(username);
            Assert.IsInstanceOf(typeof(AppUser), user);

        }        
        [Test]
        public void DeleteUser_ReturnsTrue()
        {
            var username = "john.doe@gmail.com";
            var result = service.DeleteUser(username);
            Assert.IsTrue(result);

        }

    }
}
