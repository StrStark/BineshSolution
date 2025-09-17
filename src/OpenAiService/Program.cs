
namespace OpenAiService;

public static partial class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.ConfigureServices();

        var app = builder.Build();

        app.ConfiureMiddlewares();

        app.Run();

    }
}