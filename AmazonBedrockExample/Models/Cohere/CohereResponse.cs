using System.Text.Json.Serialization;

namespace AmazonBedrockExample.Models.Cohere
{
    public class CohereResponse
    {
        [JsonPropertyName("generations")]
        public Generation[]? Generations { get; set; }
    }
}
