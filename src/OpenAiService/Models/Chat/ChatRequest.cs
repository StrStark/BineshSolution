using Newtonsoft.Json;

namespace OpenAiService.Models;

public class ChatRequest
{
    [JsonProperty("model")]
    public string Model { get; set; } = "gpt-4.1";

    [JsonProperty("messages")]
    public List<ChatMessage> Messages { get; set; } = new();
}