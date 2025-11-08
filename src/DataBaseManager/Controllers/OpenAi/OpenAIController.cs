using BineshSoloution.Models;
using BineshSoloution.Models.Embedding;
using BineshSoloution.Models.Image_Generation;
using BineshSoloution.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BineshSoloution.Controllers.OpenAi
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    [Authorize]
    public partial class OpenAIController : AppControllerBase
    {
        [AutoInject] private readonly IOpenAIService _openAIService;

        [HttpPost]
        public async Task<IActionResult> Chat([FromBody] ChatRequest request, CancellationToken cancellationToken)
        {
            var response = await _openAIService.GetChatCompletionAsync(request, cancellationToken);
            return Ok(response);
        }
        [HttpPost("embeddings")]
        public async Task<IActionResult> Embeddings([FromBody] EmbeddingRequest request, CancellationToken cancellationToken)
        {
            var response = await _openAIService.GetEmbeddingsAsync(request, cancellationToken);
            return Ok(response);
        }
        [HttpPost("images")]
        public async Task<IActionResult> Images([FromBody] ImageRequest request, CancellationToken cancellationToken)
        {
            var response = await _openAIService.GenerateImageAsync(request, cancellationToken);
            return Ok(response);
        }
    }
}
