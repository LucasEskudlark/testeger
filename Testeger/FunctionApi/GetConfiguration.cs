using System;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Extensions.Logging;

namespace FunctionApi
{
    public class GetConfiguration
    {
        private readonly ILogger _logger;

        public GetConfiguration(ILoggerFactory loggerFactory)
        {
            _logger = loggerFactory.CreateLogger<GetConfiguration>();
        }

        [Function("GetConfiguration")]
        public async Task<HttpResponseData> Run([HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "configuration")] HttpRequestData req)
        {
            _logger.LogInformation("Processing request for configuration.");

            string apiUrl = Environment.GetEnvironmentVariable("ApiBaseAddress");

            if (string.IsNullOrEmpty(apiUrl))
            {
                _logger.LogError("ApiUrl environment variable is not set.");
                var errorResponse = req.CreateResponse(HttpStatusCode.InternalServerError);
                await errorResponse.WriteStringAsync("Configuration error.");
                return errorResponse;
            }

            var config = new { ApiBaseAddress = apiUrl };

            var response = req.CreateResponse(HttpStatusCode.OK);
            response.Headers.Add("Content-Type", "application/json");

            await response.WriteStringAsync(JsonSerializer.Serialize(config));

            return response;
        }
    }
}
