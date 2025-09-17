
using KpiHndler.DbContext;
using MassTransit;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.IdentityModel.Tokens;
using Shared;
using System;
using System.Security.Cryptography.X509Certificates;

namespace KpiHandler;

public static partial class Program
{
    private static void ConfigureServices(this WebApplicationBuilder builder)
    {
        var services = builder.Services;
        var configuration = builder.Configuration;
        var env = builder.Environment;

        var ConnectionString = configuration.GetConnectionString("DefaultConnection");

        services.AddControllers();
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();

        services.AddDbContext<ApplicationDbContext>(op => op.UseNpgsql(ConnectionString));

        services.AddScoped<ApplicationDbContext>();

        services.AddSingleton(env);

        services.AddAuthorization();

        services.AddMassTransit(x =>
        {
            x.UsingRabbitMq((context, cfg) =>
            {
                cfg.Host("rabbitmq", "/", h =>
                {
                    h.Username("guest");
                    h.Password("guest");
                });
            });
        });


    }

}
