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
        private IMAPService IMAPServiceObject;

        [SetUp]
        public void Setup()
        {
            IMAPServiceObject = new IMAPService();
        }

        [Test]
        public void ConnectToIMAPService_TestUser_ShouldConnect()
        {
            IMAPServiceObject.ConnectToIMAPService("SaintSender.NET@Gmail.com", "SaintSender123");
            Assert.IsTrue(IMAPServiceObject.IsConnected());
        }

        [Test]
        public void ConnectToIMAPService_InvalidUser_ShouldThrowArgumentException()
        {
            TestDelegate testDelegate = () => IMAPServiceObject.ConnectToIMAPService("", "");
            Assert.Throws<ArgumentException>(testDelegate);
        }
    }
}
