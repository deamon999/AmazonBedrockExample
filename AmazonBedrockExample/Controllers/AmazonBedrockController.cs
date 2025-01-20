using System.Text.Json;
using System.Text;

using AmazonBedrockExample.DTOs;
using AmazonBedrockExample.Models.Cohere;

using Microsoft.AspNetCore.Mvc;
using Amazon.BedrockRuntime;

namespace AmazonBedrockExample.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AmazonBedrockController : ControllerBase
    {

        private readonly ILogger<AmazonBedrockController> _logger;
        private AmazonBedrockRuntimeClient _client;

        public AmazonBedrockController(ILogger<AmazonBedrockController> logger, AmazonBedrockRuntimeClient client)
        {
            _logger = logger;
            _client = client;
        }

        [HttpPost("/prompts/text", Name = "GetAmazonBedrock")]
        public async Task<TextPromptReponse> PostAsync([FromBody] TextPromptRequest request)
        {
            var coherePrompt = new CoherePrompt(request.Prompt);
            var bytes = Encoding.UTF8.GetBytes(JsonSerializer.Serialize(coherePrompt));
            var stream = new MemoryStream(bytes);
            var requestModel = new Amazon.BedrockRuntime.Model.InvokeModelRequest()
            {
                ModelId = "cohere.command-text-v14",
                ContentType = "application/json",
                Accept = "*/*",
                Body = stream
            };
            var response = await _client.InvokeModelAsync(requestModel);
            var data = JsonSerializer.Deserialize<CohereResponse>(response.Body);
            return new TextPromptReponse(data!.Generations![0].Text!.Trim());
        }
    }
}
