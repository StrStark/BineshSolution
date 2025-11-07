using DataBaseManager.Models.Embedding;
using DataBaseManager.Models.Image_Generation;
using DataBaseManager.Models;

namespace DataBaseManager.Service;

public interface IOpenAIService
{
    Task<ChatResponse> GetChatCompletionAsync(ChatRequest request, CancellationToken cancellationToken = default);
    Task<EmbeddingResponse> GetEmbeddingsAsync(EmbeddingRequest request, CancellationToken cancellationToken = default);
    Task<ImageResponse> GenerateImageAsync(ImageRequest request, CancellationToken cancellationToken = default);
}