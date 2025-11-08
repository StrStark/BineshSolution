namespace BineshSoloution.Models.Embedding;

public class EmbeddingData
{
    public string Object { get; set; } = string.Empty;
    public int Index { get; set; }
    public List<float> Embedding { get; set; } = new();
}
