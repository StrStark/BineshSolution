using DataBaseManager.Models;
using DataBaseManager.Models.Embedding;
using DataBaseManager.Models.Image_Generation;
using DataBaseManager.Service;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Text;
namespace DataBaseManager.Services.OpenAi;

public partial class OpenAIService : IOpenAIService
{
    [AutoInject] private readonly HttpClient _httpClient = default!;

    public async Task<ChatResponse> GetChatCompletionAsync(ChatRequest request, CancellationToken cancellationToken = default)
    {
        var json = JsonConvert.SerializeObject(request);
        var content = new StringContent(json, Encoding.UTF8, "application/json");

        var response = await _httpClient.PostAsync("chat/completions", content, cancellationToken);
        var body = await response.Content.ReadAsStringAsync();

        if (!response.IsSuccessStatusCode)
            throw new Exception($"OpenAI API Error: {response.StatusCode} - {body}");

        return JsonConvert.DeserializeObject<ChatResponse>(body)!;
    }


    public async Task<EmbeddingResponse> GetEmbeddingsAsync(EmbeddingRequest request, CancellationToken cancellationToken = default)
    {
        var json = JsonConvert.SerializeObject(request);
        var content = new StringContent(json, Encoding.UTF8, "application/json");

        var response = await _httpClient.PostAsync("embeddings", content, cancellationToken);
        response.EnsureSuccessStatusCode();

        var responseJson = await response.Content.ReadAsStringAsync(cancellationToken);
        return JsonConvert.DeserializeObject<EmbeddingResponse>(responseJson)!;
    }

    public async Task<ImageResponse> GenerateImageAsync(ImageRequest request, CancellationToken cancellationToken = default)
    {
        var json = JsonConvert.SerializeObject(request);
        var content = new StringContent(json, Encoding.UTF8, "application/json");

        var response = await _httpClient.PostAsync("images/generations", content, cancellationToken);
        response.EnsureSuccessStatusCode();

        var responseJson = await response.Content.ReadAsStringAsync(cancellationToken);
        return JsonConvert.DeserializeObject<ImageResponse>(responseJson)!;
    }

}
