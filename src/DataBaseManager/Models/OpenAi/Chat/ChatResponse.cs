
namespace BineshSoloution.Models;

public class ChatResponse
{
    public string Id { get; set; } = string.Empty;
    public string Object { get; set; } = string.Empty;
    public long Created { get; set; }
    public string Model { get; set; } = string.Empty;
    public List<ChatChoice> Choices { get; set; } = new();
    public ChatUsage Usage { get; set; } = new();
}