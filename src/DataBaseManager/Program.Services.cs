using AutoMapper;
using DataBaseManager.DbContexts;
using DataBaseManager.Models.AuthModels;
using DataBaseManager.Services;
using MassTransit;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.OData;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection.Extensions;
using DataBaseManager.Mapper;
using System.Security.Cryptography.X509Certificates;

namespace DataBaseManager;

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
        });

        services.AddDbContext<ApplicationDbContext>(op => op.UseNpgsql(ConnectionString));
        services.AddScoped<IUserService, UserService>();
        services.AddScoped<ITokenService, TokenService>();
        services.AddScoped<ApplicationDbContext>();

        services.AddSingleton(certificate);
        services.AddSingleton(env);
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
