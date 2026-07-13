// <copyright file="Program.cs" company="Dmitry Kolchev">
// Copyright (c) 2026 Dmitry Kolchev. All rights reserved.
// See LICENSE in the project root for license information
// </copyright>

using System.Globalization;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.EntityFrameworkCore;
using PhotoDb.Data;
using PhotoDb.Data.Services;
using PhotoDb.Data.Sqlite;
using PhotoLab.Hubs;
using PhotoLab.Middleware;
using Serilog;
using Xobex.Cryptography;
using Xobex.Cryptography.Abstractions;
using Xobex.CryptoId.DependencyInjection;

namespace PhotoDb.App;

internal class Program
{

    private static void Main(string[] args)
    {
        try
        {
            var builder = WebApplication.CreateBuilder(args);

            // Setup Serilog early
            Log.Logger = new LoggerConfiguration()
                .ReadFrom.Configuration(builder.Configuration)
                .Enrich.FromLogContext()
                .Enrich.WithClientIp()
                .CreateLogger();

            // Use Serilog as the logging provider
            builder.Host.UseSerilog();

            Log.Information("Starting PhotoLab Web Application...");

            ConfigureServices(builder.Services, builder.Configuration);

            var app = builder.Build();

            Configure(app);

            app.Run();
        }
        catch (Exception ex)
        {
            Log.Fatal(ex, "Host terminated unexpectedly");
        }
        finally
        {
            Log.CloseAndFlush();
        }
    }

    private static void ConfigureServices(IServiceCollection services, IConfiguration configuration)
    {
        // 1. SERVICES CONFIGURATION (.NET 10 WebApplication)
        services.AddHttpContextAccessor();
        services.AddRazorPages();
        services.AddControllers();

        // Configure local SQLite database
        services.AddDbContext<PhotoDbContext>(options =>
        {
            options.UseSqlite(configuration.GetConnectionString("DefaultConnection") ?? "Data Source=app.db");
        });

        services.AddScoped<PhotoDbContextBase>(serviceProvider =>
        {
            return serviceProvider.GetRequiredService<PhotoDbContext>();
        });
        services.AddCryptoId(new CryptoIdOptions()
        {
            Secret = "{47999C1A-BDC5-4C16-B561-BC92FBA3EE70}"
        });
        services.AddScoped<AlbumDataService>();
        services.AddScoped<ImageDataService>();
        services.AddScoped<LibraryDataService>();
        services.AddScoped<TagDataService>();
        services.AddSignalR();
    }

    private static void Configure(WebApplication app)
    {
        app.UseForwardedHeaders(new ForwardedHeadersOptions
        {
            ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto,
            KnownIPNetworks = { },
            KnownProxies = { }
        });

        // Configure HTTP Request logging via Serilog
        app.UseSerilogRequestLogging();

        // 2. MIDDLEWARE PIPELINE
        if (app.Environment.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
        }
        else
        {
            app.UseExceptionHandler("/Error");
            app.UseHsts();
        }

        app.UseHttpsRedirection();
        app.UseStaticFiles();
        app.UseRouting();
        app.UseAuthorization();

        // Mount our customized Security Stream File Provider Middleware
        app.UseMiddleware<LocalFileProviderMiddleware>();

        // Configure page routes
        app.MapRazorPages();
        app.MapControllers();
        app.MapHub<CommunicationHub>("/hub");

        // Auto-migrate SQLite schemas on initialize
        using var scope = app.Services.CreateScope();
        var db = scope.ServiceProvider.GetRequiredService<PhotoDbContextBase>();
        db.Database.EnsureCreated();
    }
}
