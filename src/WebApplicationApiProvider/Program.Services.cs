
using DataBaseManager.Controllers;
using MassTransit;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Shared.DataBaseManagerControllerInterfaces.Sales;
using Shared.Mapper;
using System.Security.Cryptography.X509Certificates;
namespace WebApplicationApiProvider;

public static partial class Program
{
    private static void ConfigureServices(this WebApplicationBuilder builder)
    {       
        var services = builder.Services;
        var configuration = builder.Configuration;
        var env = builder.Environment;
        //var appSettings = configuration.GetSection(nameof(AppSettings)).Get<AppSettings>()!;
        //var identityOptions = appSettings.Identity;
        //var dataprotectionSettings = appSettings.DataProtection;


        //var certificate = new X509Certificate2(dataprotectionSettings.DataProtectionCertificatePath!, dataprotectionSettings.DataProtectionCertificatePassword!, OperatingSystem.IsWindows() ? X509KeyStorageFlags.EphemeralKeySet : X509KeyStorageFlags.DefaultKeySet);

        //if (certificate.Thumbprint is "55140A8C935AB520294922071E5781E6946CD60606" && env.IsDevelopment() is false)
        //{
        //    throw new InvalidOperationException(@"The default test certificate is still in use. Please replace it with a new one by running the 'dotnet dev-certs https --export-path DataProtectionCertificate.pfx --password P@ssw0rdP@ssw0rd' command (or your preferred method for generating PFX files) in the server project's folder.");
        //}
        //services.Configure<AppSettings>(configuration.GetSection(nameof(AppSettings)));


        services.AddControllers();
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();


        services.AddControllers();
        //services.AddAuthorization();

        builder.Services.AddMassTransit(x =>
        {
            // Register consumer
            x.AddConsumer<SaleCreatedConsumer>();

            // RabbitMQ configuration
            x.UsingRabbitMq((context, cfg) =>
            {
                cfg.Host("rabbitmq://localhost", h =>
                {
                    h.Username("guest");
                    h.Password("guest");
                });

                // Define queue for this consumer
                cfg.ReceiveEndpoint("sales_created_queue", e =>
                {
                    e.ConfigureConsumer<SaleCreatedConsumer>(context);
                });
            });
        });

        builder.Services.AddMassTransitHostedService();



    }
}
