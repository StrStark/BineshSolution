using BineshSoloution.Models.Embedding;
using BineshSoloution.Models.Image_Generation;
using BineshSoloution.Models;

namespace BineshSoloution.Service;

public interface IOpenAIService
{
    Task<ChatResponse> GetChatCompletionAsync(ChatRequest request, CancellationToken cancellationToken = default);
    Task<EmbeddingResponse> GetEmbeddingsAsync(EmbeddingRequest request, CancellationToken cancellationToken = default);
    Task<ImageResponse> GenerateImageAsync(ImageRequest request, CancellationToken cancellationToken = default);
}