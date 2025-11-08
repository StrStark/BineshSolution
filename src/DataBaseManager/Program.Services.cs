

using AutoMapper;
using MassTransit;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.OData;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection.Extensions;
using BineshSoloution.Controllers;
using BineshSoloution.DbContexts;
using BineshSoloution.Interfaces.Sales;
using BineshSoloution.Mapper;
using BineshSoloution.Models.AuthModels;
using BineshSoloution.Services;
using BineshSoloution.Services.Sales;
using System.Security.Cryptography.X509Certificates;
using OpenAiService.Mapper;
using BineshSoloution.Service;
using BineshSoloution.Services.OpenAi;
using BineshSoloution.Interfaces.Account;
using BineshSoloution.Services.Account;
using BineshSoloution.Interfaces.Products;
using BineshSoloution.Services.Products;

namespace BineshSoloution;

public static partial class Program
{
    private static void ConfigureServices(this WebApplicationBuilder builder)
    {
        var services = builder.Services;
        var configuration = builder.Configuration;
        var env = builder.Environment;
        var appSettings = configuration.GetSection(nameof(AppSettings)).Get<AppSettings>()!;
        var identityOptions = appSettings.Identity;
        var dataprotectionSettings = appSettings.DataProtection;

        var ConnectionString = configuration.GetConnectionString("DefaultConnection");

        var certificate = new X509Certificate2(dataprotectionSettings.DataProtectionCertificatePath!, dataprotectionSettings.DataProtectionCertificatePassword!, OperatingSystem.IsWindows() ? X509KeyStorageFlags.EphemeralKeySet : X509KeyStorageFlags.DefaultKeySet);
        if (certificate.Thumbprint is "55140A8C935AB520294922071E5781E6946CD60606" && env.IsDevelopment() is false)
        {
            throw new InvalidOperationException(@"The default test certificate is still in use. Please replace it with a new one by running the 'dotnet dev-certs https --export-path DataProtectionCertificate.pfx --password P@ssw0rdP@ssw0rd' command (or your preferred method for generating PFX files) in the server project's folder.");
        }
        services.Configure<AppSettings>(configuration.GetSection(nameof(AppSettings)));


        services.AddControllers().AddOData(options => options.EnableQueryFeatures());
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();
        services.AddAutoMapper(cfg =>
        {
            cfg.AddProfile<InventoryMappingProfile>();
            cfg.AddProfile<SalesMappingProfile>();
            cfg.AddProfile<UserMappingProfile>();
            cfg.AddProfile<AccountMappingProfile>();
        });

        services.AddDbContext<ApplicationDbContext>(op => op.UseNpgsql(ConnectionString));
        services.AddDbContext<ApplicationIdentityDbContext>(op => op.UseNpgsql(ConnectionString));

        services.TryAddScoped<IUserService, UserService>();
        services.TryAddScoped<ITokenService, TokenService>();
        services.TryAddScoped<ISalesService, SalesService>();
        services.TryAddScoped<IAccountService, AccountService>();
        services.TryAddScoped<IProductService, ProductService>();

        services.AddSingleton<IOpenAIService>(sp =>
        {
            var configuration = sp.GetRequiredService<IConfiguration>();
            var httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Authorization =
                new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", appSettings.OpenAiApiKey);

            return new OpenAIService(httpClient);
        });

        services.TryAddSingleton(certificate);
        services.TryAddSingleton(env);
        services.TryAddTransient<SmsService>();

        services
            .AddIdentity<User, Role>()
            .AddEntityFrameworkStores<ApplicationDbContext>()
            .AddDefaultTokenProviders();
        services.AddAuthentication(options =>
        {
        });

        services.AddAuthorization();

        services.AddMassTransit(x =>
        {
            x.UsingRabbitMq((context, cfg) =>
            {
                cfg.Host("rabbitmq://localhost", h =>
                {
                    h.Username("guest");
                    h.Password("guest");
                });
            });
        });



    }
}
