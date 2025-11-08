namespace BineshSoloution.Models.Embedding;

public class EmbeddingResponse
{
    public string Object { get; set; } = string.Empty;
    public List<EmbeddingData> Data { get; set; } = new();
    public string Model { get; set; } = string.Empty;
    public EmbeddingUsage Usage { get; set; } = new();
}