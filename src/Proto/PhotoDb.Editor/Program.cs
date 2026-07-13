// <copyright file="Program.cs" company="Dmitry Kolchev">
// Copyright (c) 2026 Dmitry Kolchev. All rights reserved.
// See LICENSE in the project root for license information
// </copyright>

using System.Net.WebSockets;
using System.Text.Json;
using Microsoft.AspNetCore.HttpOverrides;
using Serilog;

namespace PhotoDb.Editor;

public class Program
{
    public static void Main(string[] args)
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

        Log.Information("Starting PhotoDb.Editor Web Application...");

        builder.Services.AddHttpContextAccessor();
        // Add services to the container.
        builder.Services.AddControllersWithViews();
        //builder.Services.AddSignalR();
        builder.Services.AddCors();

        var app = builder.Build();

        app.UseForwardedHeaders(new ForwardedHeadersOptions
        {
            ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto,
            KnownIPNetworks = { },
            KnownProxies = { }
        });

        // Configure HTTP Request logging via Serilog
        app.UseSerilogRequestLogging();

        // Configure WebSockets Middleware
        var webSocketOptions = new WebSocketOptions
        {
            KeepAliveInterval = TimeSpan.FromSeconds(120),
        };
        app.UseWebSockets(webSocketOptions);

        app.UseCors(policy => policy.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());

        // Handle incoming WebSockets connections
        app.Map("/ws", async context =>
        {
            if (context.WebSockets.IsWebSocketRequest)
            {
                using var webSocket = await context.WebSockets.AcceptWebSocketAsync();
                await HandleWebSocketSession(webSocket, context.RequestServices.GetRequiredService<ILogger<Program>>());
            }
            else
            {
                context.Response.StatusCode = StatusCodes.Status400BadRequest;
            }
        });

        // Configure the HTTP request pipeline.
        if (!app.Environment.IsDevelopment())
        {
            app.UseExceptionHandler("/Home/Error");
            // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
            app.UseHsts();
        }
        else
        {
            app.UseDeveloperExceptionPage();
        }

        app.UseHttpsRedirection();
        app.UseRouting();

        app.UseAuthorization();

        app.MapStaticAssets();
        app.MapControllerRoute(
            name: "default",
            pattern: "{controller=Home}/{action=Index}/{id?}")
            .WithStaticAssets();
        //app.MapHub<CommunicationHub>("/hub");

        app.Run();
    }

    // private static async Task HandleWebSocketSession(WebSocket webSocket, ILogger<Program> logger)
    // logger.LogInformation($"MsgType: {result.MessageType} EndOfMsg:{result.EndOfMessage} RecvLen: {result.Count}");
    private static async Task HandleWebSocketSession(WebSocket webSocket, ILogger<Program> logger)
    {
        var processor = new ImageProcessor();
        var currentParams = new AdjustmentParams();

        int? pendingWidth = null;
        int? pendingHeight = null;

        var buffer = new byte[1024 * 1024 * 8]; // Large 8MB buffer to read complete message in fewer calls
        using var ms = new MemoryStream();

        try
        {
            while (webSocket.State == WebSocketState.Open)
            {
                ms.SetLength(0);
                WebSocketReceiveResult result;
                do
                {
                    result = await webSocket.ReceiveAsync(new ArraySegment<byte>(buffer), CancellationToken.None);

                    logger.LogInformation($"MsgType: {result.MessageType} EndOfMsg:{result.EndOfMessage} RecvLen: {result.Count}");

                    if (result.MessageType == WebSocketMessageType.Close)
                    {
                        break;
                    }
                    ms.Write(buffer, 0, result.Count);
                } while (!result.EndOfMessage);

                if (result.MessageType == WebSocketMessageType.Close)
                {
                    await webSocket.CloseAsync(WebSocketCloseStatus.NormalClosure, "Session ended", CancellationToken.None);
                    break;
                }

                var messageBytes = ms.ToArray();

                if (result.MessageType == WebSocketMessageType.Binary)
                {
                    // Handle raw RGBA bytes binary upload
                    if (pendingWidth.HasValue && pendingHeight.HasValue)
                    {
                        var w = pendingWidth.Value;
                        var h = pendingHeight.Value;
                        var expectedLength = w * h * 4;

                        if (messageBytes.Length < expectedLength)
                        {
                            Console.WriteLine($"Received binary length {messageBytes.Length} is less than expected {expectedLength}");
                            continue;
                        }

                        // Copy the precise bytes representing image pixels
                        var imgBytes = new byte[expectedLength];
                        Array.Copy(messageBytes, imgBytes, expectedLength);

                        // Initialize C# FP32 Planar arrays
                        processor.Initialize(imgBytes, w, h);

                        // Apply adjustments and stream binary quantized frame back
                        processor.ApplyAdjustments(currentParams);
                        var renderBuffer = processor.GetQuantizedPlanarRender();

                        await webSocket.SendAsync(
                            new ArraySegment<byte>(renderBuffer),
                            WebSocketMessageType.Binary,
                            true,
                            CancellationToken.None
                        );

                        pendingWidth = null;
                        pendingHeight = null;
                    }
                }
                else if (result.MessageType == WebSocketMessageType.Text)
                {
                    // Parse JSON adjustment requests
                    var jsonText = System.Text.Encoding.UTF8.GetString(messageBytes);
                    using var doc = JsonDocument.Parse(jsonText);
                    var root = doc.RootElement;

                    if (root.TryGetProperty("type", out var typeProp))
                    {
                        var type = typeProp.GetString();

                        if (type == "upload_init")
                        {
                            pendingWidth = root.GetProperty("width").GetInt32();
                            pendingHeight = root.GetProperty("height").GetInt32();

                            // Respond that server is ready for binary payload
                            var readyMsg = System.Text.Encoding.UTF8.GetBytes("{\"type\":\"upload_ready\"}");
                            await webSocket.SendAsync(
                                new ArraySegment<byte>(readyMsg),
                                WebSocketMessageType.Text,
                                true,
                                CancellationToken.None
                            );
                        }
                        else if (type == "adjust")
                        {
                            var paramsNode = root.GetProperty("params");

                            // Map JSON to AdjustmentParams
                            if (paramsNode.TryGetProperty("exposure", out var exp))
                            {
                                currentParams.Exposure = exp.GetSingle();
                            }

                            if (paramsNode.TryGetProperty("contrast", out var con))
                            {
                                currentParams.Contrast = con.GetSingle();
                            }

                            if (paramsNode.TryGetProperty("highlights", out var hi))
                            {
                                currentParams.Highlights = hi.GetSingle();
                            }

                            if (paramsNode.TryGetProperty("shadows", out var sh))
                            {
                                currentParams.Shadows = sh.GetSingle();
                            }

                            if (paramsNode.TryGetProperty("temperature", out var temp))
                            {
                                currentParams.Temperature = temp.GetSingle();
                            }

                            if (paramsNode.TryGetProperty("tint", out var tint))
                            {
                                currentParams.Tint = tint.GetSingle();
                            }

                            if (paramsNode.TryGetProperty("saturation", out var sat))
                            {
                                currentParams.Saturation = sat.GetSingle();
                            }

                            if (paramsNode.TryGetProperty("vignette", out var vig))
                            {
                                currentParams.Vignette = vig.GetSingle();
                            }

                            if (processor.Width > 0)
                            {
                                processor.ApplyAdjustments(currentParams);
                                var renderBuffer = processor.GetQuantizedPlanarRender();

                                await webSocket.SendAsync(
                                    new ArraySegment<byte>(renderBuffer),
                                    WebSocketMessageType.Binary,
                                    true,
                                    CancellationToken.None
                                );
                            }
                        }
                    }
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"WebSocket error or disconnect: {ex.Message}");
        }
    }
    private static async Task HandleWebSocketSessionOld(WebSocket webSocket, ILogger<Program> logger)
    {
        var processor = new ImageProcessor();
        var currentParams = new AdjustmentParams();

        int? pendingWidth = null;
        int? pendingHeight = null;

        var buffer = new byte[1024 * 1024 * 4]; // Pre-allocated 4MB buffer for reading messages

        try
        {
            while (webSocket.State == WebSocketState.Open)
            {
                // Read incoming message
                var result = await webSocket.ReceiveAsync(new ArraySegment<byte>(buffer), CancellationToken.None);

                logger.LogInformation($"WebSocket: {result.MessageType} EOM:{result.EndOfMessage} RecvLen: {result.Count}");

                if (result.MessageType == WebSocketMessageType.Close)
                {
                    await webSocket.CloseAsync(WebSocketCloseStatus.NormalClosure, "Session ended", CancellationToken.None);
                    break;
                }

                if (result.MessageType == WebSocketMessageType.Binary)
                {
                    // Handle raw RGBA bytes binary upload
                    if (pendingWidth.HasValue && pendingHeight.HasValue)
                    {
                        var w = pendingWidth.Value;
                        var h = pendingHeight.Value;
                        var expectedLength = w * h * 4;

                        // Copy the precise bytes representing image pixels
                        var imgBytes = new byte[expectedLength];
                        Array.Copy(buffer, imgBytes, expectedLength);

                        // Initialize C# FP32 Planar arrays
                        processor.Initialize(imgBytes, w, h);

                        // Apply adjustments and stream binary quantized frame back
                        processor.ApplyAdjustments(currentParams);
                        var renderBuffer = processor.GetQuantizedPlanarRender();

                        await webSocket.SendAsync(
                            new ArraySegment<byte>(renderBuffer),
                            WebSocketMessageType.Binary,
                            true,
                            CancellationToken.None
                        );

                        pendingWidth = null;
                        pendingHeight = null;
                    }
                }
                else if (result.MessageType == WebSocketMessageType.Text)
                {
                    // Parse JSON adjustment requests
                    var jsonText = System.Text.Encoding.UTF8.GetString(buffer, 0, result.Count);
                    using var doc = JsonDocument.Parse(jsonText);
                    var root = doc.RootElement;

                    if (root.TryGetProperty("type", out var typeProp))
                    {
                        var type = typeProp.GetString();

                        if (type == "upload_init")
                        {
                            pendingWidth = root.GetProperty("width").GetInt32();
                            pendingHeight = root.GetProperty("height").GetInt32();

                            // Respond that server is ready for binary payload
                            var readyMsg = System.Text.Encoding.UTF8.GetBytes("{\"type\":\"upload_ready\"}");
                            await webSocket.SendAsync(
                                new ArraySegment<byte>(readyMsg),
                                WebSocketMessageType.Text,
                                true,
                                CancellationToken.None
                            );
                        }
                        else if (type == "adjust")
                        {
                            var paramsNode = root.GetProperty("params");

                            // Map JSON to AdjustmentParams
                            if (paramsNode.TryGetProperty("exposure", out var exp))
                            {
                                currentParams.Exposure = exp.GetSingle();
                            }

                            if (paramsNode.TryGetProperty("contrast", out var con))
                            {
                                currentParams.Contrast = con.GetSingle();
                            }

                            if (paramsNode.TryGetProperty("highlights", out var hi))
                            {
                                currentParams.Highlights = hi.GetSingle();
                            }

                            if (paramsNode.TryGetProperty("shadows", out var sh))
                            {
                                currentParams.Shadows = sh.GetSingle();
                            }

                            if (paramsNode.TryGetProperty("temperature", out var temp))
                            {
                                currentParams.Temperature = temp.GetSingle();
                            }

                            if (paramsNode.TryGetProperty("tint", out var tint))
                            {
                                currentParams.Tint = tint.GetSingle();
                            }

                            if (paramsNode.TryGetProperty("saturation", out var sat))
                            {
                                currentParams.Saturation = sat.GetSingle();
                            }

                            if (paramsNode.TryGetProperty("vignette", out var vig))
                            {
                                currentParams.Vignette = vig.GetSingle();
                            }

                            if (processor.Width > 0)
                            {
                                processor.ApplyAdjustments(currentParams);
                                var renderBuffer = processor.GetQuantizedPlanarRender();

                                await webSocket.SendAsync(
                                    new ArraySegment<byte>(renderBuffer),
                                    WebSocketMessageType.Binary,
                                    true,
                                    CancellationToken.None
                                );
                            }
                        }
                    }
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"WebSocket error or disconnect: {ex.Message}");
        }
    }

}
