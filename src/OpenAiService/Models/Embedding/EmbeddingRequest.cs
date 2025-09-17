namespace OpenAiService.Models.Embedding;

public class EmbeddingRequest
{
    public string Model { get; set; } = "text-embedding-3-small";
    public List<string> Input { get; set; } = new();
}