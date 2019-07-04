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
    public class CredentialServiceTests
    {
        private CredentialService CredentialServiceObject;
		[SetUp]
		public void Setup()
        {
            CredentialServiceObject = new CredentialService();
        }

		[Test]
		public void Encrypt_Decrypt_ShouldReturnSame()
        {
            CredentialServiceObject.SaveCredentials("test1", "test2");
            string[] expected = new string[] { "test1", "test2" };
            string[] actual = CredentialServiceObject.GetSavedCredentials();
            Assert.AreEqual(expected, actual);
        }
    }
}
