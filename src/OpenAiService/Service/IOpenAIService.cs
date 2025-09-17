using OpenAiService.Models.Embedding;
using OpenAiService.Models.Image_Generation;
using OpenAiService.Models;

namespace OpenAiService.Service;

public interface IOpenAIService
{
    Task<ChatResponse> GetChatCompletionAsync(ChatRequest request, CancellationToken cancellationToken = default);
    Task<EmbeddingResponse> GetEmbeddingsAsync(EmbeddingRequest request, CancellationToken cancellationToken = default);
    Task<ImageResponse> GenerateImageAsync(ImageRequest request, CancellationToken cancellationToken = default);
}