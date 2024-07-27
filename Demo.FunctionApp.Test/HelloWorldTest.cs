using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Moq;
using System.Net;

namespace Demo.FunctionApp.Test
{
    [TestClass]
    public class HelloWorldTest
    {
        [TestMethod]
        public void WhenInvokingHelloWorld_ThenItReturnsHelloWorld()
        {
            var subject = new HelloWorld();

            var result = subject.Run(new Mock<HttpRequest>().Object);

            Assert.IsTrue(result is OkObjectResult);
        }
    }
}