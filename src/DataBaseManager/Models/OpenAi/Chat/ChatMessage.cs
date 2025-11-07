using Newtonsoft.Json;

namespace DataBaseManager.Models;

public class ChatMessage
{
    [JsonProperty("role")]
    public string Role { get; set; } = "user";

    [JsonProperty("content")]
    public string Content { get; set; } = string.Empty;
}