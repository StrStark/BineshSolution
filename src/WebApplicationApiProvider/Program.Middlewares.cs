namespace WebApplicationApiProvider;

public static partial class Program
{
    private static void ConfiureMiddlewares(this WebApplication app)
    {
        var configuration = app.Configuration;
        var env = app.Environment;
        var services = app.Services;

        if (env.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();
        app.UseRouting();

        app.UseAuthorization();

        app.MapControllers();

    }
}
