
namespace OpenAiService.Models.Image_Generation;

public class ImageResponse
{
    public long Created { get; set; }
    public List<ImageData> Data { get; set; } = new();
}