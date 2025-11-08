
namespace BineshSoloution.Models.Image_Generation;

public class ImageRequest
{
    public string Prompt { get; set; } = string.Empty;
    public int N { get; set; } = 1;
    public string Size { get; set; } = "1024x1024"; // "256x256", "512x512"
    public string ResponseFormat { get; set; } = "b64_json"; // or "url"
}