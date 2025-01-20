using System.Text.Json.Serialization;

namespace AmazonBedrockExample.Models.Cohere
{
    public class Generation
    {
        [JsonPropertyName("text")]
        public string? Text { get; set; }
    }
}
