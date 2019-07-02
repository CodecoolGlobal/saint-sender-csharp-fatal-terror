using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using SaintSender.Core.Services;

namespace SaintSender.Core.Tests
{
    [TestFixture]
    public class AuthenticationServiceTests
    {
        private AuthenticationService authenticationService;

        [SetUp]
        public void Setup()
        {
            authenticationService = new AuthenticationService();
        }

        [Test]
        public void ConnectToIMAPService_TestUser_ShouldConnect()
        {
            authenticationService.ConnectToIMAPService("SaintSender.NET@Gmail.com", "SaintSender123");
            Assert.IsTrue(authenticationService.IsConnected());
        }

        [Test]
        public void ConnectToIMAPService_InvalidUser_ShouldThrowArgumentException()
        {
            TestDelegate testDelegate = () => authenticationService.ConnectToIMAPService("", "");
            Assert.Throws<ArgumentException>(testDelegate);
        }
    }
}
