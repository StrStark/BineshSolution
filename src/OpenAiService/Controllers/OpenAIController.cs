using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OpenAiService.Models;
using OpenAiService.Models.Embedding;
using OpenAiService.Models.Image_Generation;
using OpenAiService.Service;

namespace OpenAiService.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    [Authorize]
    public class OpenAIController : Controller
    {
        private readonly IOpenAIService _openAIService;
        public OpenAIController(IOpenAIService openAIService)
        {
            _openAIService = openAIService;
        }

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
