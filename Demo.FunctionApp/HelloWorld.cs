using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;

namespace Demo.FunctionApp
{
    public class HelloWorld
    {
        [Function("HelloWorld")]
        public IActionResult Run([HttpTrigger(AuthorizationLevel.Function, "get")] HttpRequest request)
        {
            var response = "Hello world!";

            return new OkObjectResult(response);
        }
    }
}