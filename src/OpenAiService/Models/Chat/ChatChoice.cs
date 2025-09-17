namespace OpenAiService.Models;
public class ChatChoice
{
    public int Index { get; set; }
    public ChatMessage Message { get; set; } = new();
    public string FinishReason { get; set; } = string.Empty;
}
