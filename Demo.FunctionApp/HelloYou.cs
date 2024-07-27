using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace Demo.FunctionApp
{
    public class HelloYou
    {
        public const string ServerNameConfigurationKey = "server";
        private readonly IConfiguration _configuration;
        private ILogger<HelloYou> _logger;

        public HelloYou(IConfiguration configuration, ILogger<HelloYou> logger)
        {
            _configuration = configuration;
            _logger = logger;
        }



        [Function("HelloYou")]
        public IActionResult Run([HttpTrigger(AuthorizationLevel.Function, "get")] HttpRequest request)
        {
            var name = request.Query["name"];

            if (string.IsNullOrEmpty(name))
            {
                return new BadRequestObjectResult("Missing parameter 'name'");
            }

            var server = _configuration.GetValue<string>(ServerNameConfigurationKey);

            var response = $"Hello {name}! This is {server}!";

            return new OkObjectResult(response);
        }
    }
}