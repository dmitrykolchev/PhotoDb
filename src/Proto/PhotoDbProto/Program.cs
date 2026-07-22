// <copyright file="Program.cs" company="Dmitry Kolchev">
// Copyright (c) 2026 Dmitry Kolchev. All rights reserved.
// See LICENSE in the project root for license information
// </copyright>

using System.Diagnostics;
using System.Text;
using System.Text.Json;
using Managed.LibRaw;
using Managed.LLama.Cpp;
using Managed.LLama.Cpp.Gemma;
using PhotoDb.Imaging;
using Spectre.Console;

namespace PhotoDbProto;

internal class Program
{
    private static readonly JsonSerializerOptions _indentedSerializationOptions = new()
    {
        IndentCharacter = ' ',
        IndentSize = 4,
        WriteIndented = true,
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase
    };

    private static async Task Main(string[] args)
    {
        Console.CancelKeyPress += OnCancelKeyPress;
        Environment.SetEnvironmentVariable("PATH", $"{Environment.GetEnvironmentVariable("PATH")};C:\\Program Files\\NVIDIA GPU Computing Toolkit\\CUDA\\v13.1\\bin\\x64\\");
        TestGemma();
        //StartGemmaChat();
    }

    private static void TestGemma()
    {
        //var imagePath = @"D:\Users\dykolchev.DYKBITS\Pictures\canon\Lena\_MG_6762.JPG";
        var imagePath = @"D:\Users\dykolchev.DYKBITS\Pictures\_MG_1110.jpg";
        var metadata = ExifMetadataExtractor.ReadMetadataCore(imagePath);
        Console.WriteLine(JsonSerializer.Serialize(metadata, _indentedSerializationOptions));

        LlamaLogger.InitializeLogger();
        LlamaLogger.SetCustomLogger((level, text, data) =>
        {
            if (level == GgmlLogLevel.Error)
            {
                Console.Write($"\u001b[31m{text}\u001b[0m");
            }
            else if (level == GgmlLogLevel.Warn)
            {
                Console.Write($"\u001b[33m{text}\u001b[0m");
            }
        });

        GgmlLogger.InitializeLogger();
        GgmlLogger.SetCustomLogger((level, text, data) =>
        {
            if (level == GgmlLogLevel.Error)
            {
                Console.Write($"\u001b[31m{text}\u001b[0m");
            }
            else if (level == GgmlLogLevel.Warn)
            {
                Console.Write($"\u001b[33m{text}\u001b[0m");
            }
        });

        MtmdLogger.InitializeLogger();
        MtmdLogger.SetCustomLogger((level, text, data) =>
        {
            if (level == GgmlLogLevel.Error)
            {
                Console.Write($"\u001b[31m{text}\u001b[0m");
            }
            else if (level == GgmlLogLevel.Warn)
            {
                Console.Write($"\u001b[33m{text}\u001b[0m");
            }
        });

        //var modelPath = @"C:\Projects\2026\PhotoDb\models\gemma-4-E4B-it-GGUF\gemma-4-E4B-it-Q4_K_M.gguf";
        //var mtmdPath = @"C:\Projects\2026\PhotoDb\models\gemma-4-E4B-it-GGUF\mmproj-BF16.gguf";
        var modelPath = @"C:\Projects\2026\PhotoDb\models\gemma-4-12B-it-GGUF\gemma-4-12B-it-Q4_K_M.gguf";
        var mtmdPath = @"C:\Projects\2026\PhotoDb\models\gemma-4-12B-it-GGUF\mmproj-gemma-4-12B-it-BF16.gguf";

        using var gemmaPromptExecutor = new Gemma4PromptExecutor(modelPath, mtmdPath);
        gemmaPromptExecutor.Load(
            contextSize: 16 * 1024,
            attentionType: LLamaAttentionType.Causal,
            typeK: GgmlType.Q4_0,
            typeV: GgmlType.Q4_0);
        gemmaPromptExecutor.MaxOutputTokens = 2000;
        gemmaPromptExecutor.ThinkingMode = false;
        (var description, var json) = DescribeImage(gemmaPromptExecutor, imagePath);
        Console.WriteLine(description);
        Console.WriteLine(json);
    }

    private static (string description, string json) DescribeImage(Gemma4PromptExecutor gemmaPromptExecutor, string path)
    {
        var image = new CanvasImage(path);
        image.MaxWidth(80);
        AnsiConsole.Write(
            new Panel(image)
                .Header("[yellow]Превью фото[/]")
                .Border(BoxBorder.Rounded)
                .BorderColor(Color.Blue)
            );
        gemmaPromptExecutor.Clear();
        gemmaPromptExecutor.LoadBitmap(path);
        var result1 = gemmaPromptExecutor.Describe(systemPrompt: Gemma4PromptExecutor.ImageToTextPrompt);
        var result2 = gemmaPromptExecutor.Describe(systemPrompt: Gemma4PromptExecutor.ImageToJsonPrompt);
        return (result1, result2);
    }

    private static bool _cancelGeneration;

    private static void OnCancelKeyPress(object? sender, ConsoleCancelEventArgs e)
    {
        if (!_cancelGeneration)
        {
            e.Cancel = true;
            _cancelGeneration = true;
        }
    }

    private static MtmdBitmap? _bitmap;
    private static LLamaModel _model = null!;
    private static LLamaContext _context = null!;
    private static MtmdContext _mtmd = null!;

    private static void StartGemmaChat()
    {
        LlamaLogger.InitializeLogger();
        LlamaLogger.SetCustomLogger((level, text, data) =>
        {
            if (level == GgmlLogLevel.Error)
            {
                Console.Write($"\u001b[31m{text}\u001b[0m");
            }
            else if (level == GgmlLogLevel.Warn)
            {
                Console.Write($"\u001b[33m{text}\u001b[0m");
            }
        });
        var count = Cuda.GetDeviceCount();
        Console.WriteLine($"CUDA Device Count: {count}");
        for (var index = 0; index < count; ++index)
        {
            var description = Cuda.GetDeviceDescription(index);
            Console.WriteLine($"{index}:{description}");
            var (free, total) = Cuda.GetDeviceMemory(index);
            Console.WriteLine($"\tFree: {free / 1024.0 / 1024.0 / 1024.0} GB, Total: {total / 1024.0 / 1024.0 / 1024.0} GB");
        }
        using var backend = Cuda.GetBackend(0);
        using (new GCShield(backend))
        {
            Console.WriteLine($"Backend is{(Cuda.IsCuda(backend) ? " " : " not ")}CUDA backend");
            using var device = backend.GetDevice();
            Console.WriteLine($"Device: {device.Name} ({device.Description})");
            Console.WriteLine($"Device Type: {device.DeviceType}");
            var (free, total) = device.Memory;
            Console.WriteLine($"Device Memory - Free: {free / 1024.0 / 1024.0 / 1024.0} GB, Total: {total / 1024.0 / 1024.0 / 1024.0} GB");
            var properties = device.GetProperties();
        }
        Ggml.LoadAllBackends();

        //using (var simpleEmbedder = LLamaEmbedder.Create("C:\\Projects\\2026\\PhotoDb\\models\\Qwen3-Embedding-GGUF\\Qwen3-Embedding-8B-Q4_K_M.gguf"))
        //{
        //    simpleEmbedder.TargetDim = 256;
        //    var embedding = simpleEmbedder.GetEmbedding("«Просим жителей Крыма на время ликвидации аварии в энергосистеме полуострова ограничить потребление электроэнергии. Выключить кондиционеры и избыточные электроприборы», — написал советник главы республики Олег Крючков в телеграм-канале.");
        //    Console.WriteLine($"Embedding sample: {string.Join(", ", embedding.Take(20).Select(v => v.ToString("F4", CultureInfo.InvariantCulture)))}");
        //}

        var modelParams = LLamaModelParams.Default();
        modelParams.GpuLayerCount = 99;

        //var modelPath = @"C:\Projects\2026\PhotoDb\models\gemma-4-E4B-it-GGUF\gemma-4-E4B-it-Q4_K_M.gguf";
        //var mtmdPath = @"C:\Projects\2026\PhotoDb\models\gemma-4-E4B-it-GGUF\mmproj-BF16.gguf";
        //var modelPath = @"C:\Projects\2026\PhotoDb\models\gemma-4-12B-it-GGUF\gemma-4-12B-it-Q4_K_M.gguf";
        var modelPath = @"E:\LMStudio\models\OBLITERATUS\Gemma-4-12B-OBLITERATED\Gemma-4-12B-OBLITERATED-Q4_K_M.gguf";
        var mtmdPath = @"C:\Projects\2026\PhotoDb\models\gemma-4-12B-it-GGUF\mmproj-gemma-4-12B-it-BF16.gguf";

        //var modelPath = @"E:\LMStudio\models\lmstudio-community\gemma-4-12B-it-QAT-GGUF\gemma-4-12B-it-QAT-Q4_0.gguf";
        //var mtmdPath = @"E:\LMStudio\models\lmstudio-community\gemma-4-12B-it-QAT-GGUF\mmproj-gemma-4-12B-it-QAT-BF16.gguf";

        //var modelPath = @"E:\LMStudio\models\lmstudio-community\gemma-4-26B-A4B-it-QAT-GGUF\gemma-4-26B-A4B-it-QAT-Q4_0.gguf";
        //var mtmdPath = @"E:\LMStudio\models\lmstudio-community\gemma-4-26B-A4B-it-QAT-GGUF\mmproj-gemma-4-26B-A4B-it-QAT-BF16.gguf";

        //var modelPath = @"E:\LMStudio\models\AtomicChat\Laguna-XS-2.1-GGUF\Laguna-XS-2.1-Q4_K_M.gguf";
        //string? mtmdPath = null;

        _model = LLamaModel.LoadFromFile(
            modelPath,
            modelParams);

        var contextParams = LLamaContextParams.Default();
        contextParams.ContextSize = 128 * 1024;
        contextParams.BatchSize = 512;
        contextParams.ContextType = LLamaContextType.Default;
        contextParams.SwaFull = false;
        contextParams.TypeK = GgmlType.Q4_0;
        contextParams.TypeV = GgmlType.Q4_0;
        _context = LLamaContext.InitFromModel(_model, contextParams);

        var template = _model.GetChatTemplate();

        if (mtmdPath != null)
        {
            var mtmdParameters = MtmdContextParams.Default();
            _mtmd = MtmdContext.InitFromFile(mtmdPath, _model, mtmdParameters);
        }

        var samplerChainParams = LLamaSamplerChainParams.Default();
        using var sampler = LLamaSampler.ChainInit(samplerChainParams);
        sampler
            .AddTopK(k: 64)
            .AddTopP(p: 0.95f, 1)
            .AddTemp(t: 1f)
            //.AddMinP(p: 0.05f, minKeep: 1)
            //.AddPenalties(penaltyLastN: 0, penaltyRepeat: 1.0f, penaltyFreq: 0.0f, penaltyPresent: 0)
            //.AddMirostatV2(uint.MaxValue, 5.0f, 0.1f)
            .AddLogitBias(_model.Vocabulary.Count, 106, -50.0f)
            .AddSeed();

        var tokenCount = 0;
        var stateMachine = new StateMachine(_model);
        var generate = (string prompt) =>
        {
            stateMachine.Reset();
            var response = new StringBuilder();
            var isFirst = (_context.Memory.GetMaxPosition(0) + 1) == 0;
            LLamaBatch batch = default;
            MtmdInputChunks? chunks = default;

            int batchLength;

            if (_bitmap != null)
            {
                chunks = _mtmd.Tokenize(prompt, isFirst, true, [_bitmap], out batchLength);
                _bitmap.Dispose();
                _bitmap = null;
            }
            else
            {
                var tokens = _context.Tokenize(_model.Vocabulary, prompt, isFirst, true);
                //foreach (var t in tokens)
                //{
                //    Console.WriteLine($"Token ID: {t} -> '{_model.Vocabulary.TokenToPiece(t, 0, true)}'");
                //}
                batch = new LLamaBatch(tokens);
                batchLength = batch.Length;
            }

            tokenCount = 0;

            _cancelGeneration = false;
            var contextLength = _context.ContextLength;
            var startOfThinkingPosition = -1;
            var endOfThinkingPosition = -1;
            while (true)
            {
                if (_cancelGeneration)
                {
                    _cancelGeneration = false;
                    break;
                }
                var currentPos = _context.Memory.GetMaxPosition(0) + 1;
                if (currentPos + batchLength > contextLength)
                {
                    Console.WriteLine("\u001B[0m");
                    Console.WriteLine("context size exceeded");
                    throw new InvalidOperationException("context size exceeded");
                }

                if (chunks != null)
                {
                    currentPos = _mtmd.EvalChunks(_context, chunks, currentPos, 0, 512, true);
                    chunks.Dispose();
                    chunks = null;
                }
                else
                {
                    currentPos = _context.Decode(batch, currentPos, 0);
                }

                var newToken = sampler.Sample(_context, -1);

                stateMachine.Process(newToken);

                var piece = _model.Vocabulary.TokenToPiece(newToken, 0, true);

                if (stateMachine.CurrentState is StateMachine.State.StartOfThinking)
                {
                    startOfThinkingPosition = currentPos;
                    Console.WriteLine("\n[Start Of Thinking]");
                }
                else if (stateMachine.CurrentState is StateMachine.State.EndOfThinking)
                {
                    endOfThinkingPosition = currentPos;
                    Console.WriteLine("\n[End Of Thinking]");
                }
                else if (stateMachine.CurrentState is StateMachine.State.ThinkingInprogess)
                {
                    Console.Write("\u001B[37m");
                    Console.Write(piece);
                    Console.Write("\u001B[0m");
                }
                else if (stateMachine.CurrentState is StateMachine.State.ResponseInprogress)
                {
                    Console.Write("\u001B[32m");
                    Console.Write(piece);
                    Console.Write("\u001B[0m");
                    response.Append(piece);
                }
                else if (stateMachine.CurrentState is StateMachine.State.EndOfGeneration)
                {
                    if (startOfThinkingPosition >= 0 && endOfThinkingPosition >= 0)
                    {
                        _context.Memory.Remove(0, startOfThinkingPosition, endOfThinkingPosition);
                        Console.WriteLine($"\n[Removing context from {startOfThinkingPosition} to {endOfThinkingPosition}]");
                    }
                    break;
                }
                batch = new LLamaBatch(newToken);
                tokenCount++;
            }
            return response.ToString();
        };

        var first = true;
        while (true)
        {
            Console.Write("\x1B[32m> \x1B[0m");
            var user = ReadPrompt();
            if (string.IsNullOrEmpty(user))
            {
                break;
            }
            if (_bitmap != null)
            {
                user = $"<|turn>user\n{_mtmd.Marker}\n{user}<turn|>\n";
            }
            else
            {
                user = $"<|turn>user\n{user}<turn|>\n";
            }
            string promptText;
            if (first)
            {
                var systemPrompt = $"{SystemPrompt.Default}\nСейчас: {DateTime.Now} {TimeZoneInfo.Local}";
                first = false;
                promptText =
                    "<|turn>system\n<|think|>\n" +
                    $"{systemPrompt}<turn|>\n" +
                    user +
                    "<|turn>model\n";
            }
            else
            {
                var systemPrompt = $"{DateTime.Now} {TimeZoneInfo.Local}";
                promptText =
                    user +
                    "<|turn>model\n";
            }
            Console.Write("\u001B[31m");
            Console.Write(promptText);
            Console.Write("\n\u001B[0m");

            Console.Write("\u001B[33m");
            var startingTimestamp = Stopwatch.GetTimestamp();
            var response = generate(promptText);
            var seconds = Stopwatch.GetElapsedTime(startingTimestamp).TotalSeconds;
            Console.Write("\n\u001B[0m");
            Console.WriteLine($"\n\u001b[31mContext:{_context.Memory.GetMaxPosition(0)} Tokens: {tokenCount} ({tokenCount / seconds:0.00}tok/sec)");
        }
    }

    private static string ReadPrompt()
    {
        StringBuilder builder = new();
        for (; ; )
        {
            var line = Console.ReadLine()?.Trim();
            if (line is null)
            {
                continue;
            }
            else if (line == ".")
            {
                return builder.ToString();
            }
            else if (line.StartsWith('@'))
            {
                var path = line[1..];
                if (path.StartsWith('\"') && path.EndsWith('\"'))
                {
                    path = path[1..^1];
                }
                if (File.Exists(path))
                {
                    try
                    {
                        _bitmap = _mtmd.LoadBitmap(path, false);
                        Console.WriteLine($"Image loaded");
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Image loading failed. Error: {ex.Message}");
                    }
                }
                else
                {
                    Console.WriteLine($"File: {path} cannot be found");
                }
            }
            else
            {
                if (builder.Length > 0)
                {
                    builder.AppendLine();
                }
                builder.Append(line);
            }
        }
    }

    private static void TestLibRaw()
    {
        Console.WriteLine($"LibRaw version: {Factory.Version}");
        Console.WriteLine($"LibRaw version number: {Factory.VersionNumber}");
        Console.WriteLine("Supported Canon cameras");
        Console.WriteLine("=======================");
        var index = 0;
        foreach (var camera in Factory.SupportedCameras.Where(t => t.Contains("canon eos", StringComparison.OrdinalIgnoreCase)))
        {
            Console.WriteLine($"{++index,4}. {camera}");
        }
    }

    private static MemoryStream GetMaxResolutionThumbnail(string filePath)
    {
        using var context = Factory.Open(filePath);

        var maxWidth = int.MinValue;
        var thumbnailIndex = -1;
        var index = 0;
        foreach (var thumbnail in context.Thumbnails)
        {
            if (thumbnail.Format == InternalThumnailFormat.Jpeg)
            {
                if (maxWidth < thumbnail.Width)
                {
                    maxWidth = thumbnail.Width;
                    thumbnailIndex = index;
                }
            }
            index++;
        }
        Console.WriteLine($"Thumbnail Max Width: {maxWidth}");
        using var image = context.Thumbnails.Export(thumbnailIndex);
        if (image.ImageType == ImageType.Jpeg)
        {
            var jpegData = image.AsSpan<byte>();
            MemoryStream output = new(jpegData.Length);
            output.Write(jpegData);
            output.Position = 0;
            return output;
        }
        else
        {
            throw new InvalidOperationException($"Unsupported image type: {image.ImageType}");
        }
    }

    private static MemoryStream GetImage(string filePath, int thumbnailIndex = 0)
    {
        using var context = Factory.Open(filePath);
        //context.UnpackThumbnail();
        if (thumbnailIndex < 0 || thumbnailIndex >= context.Thumbnails.Count)
        {
            throw new ArgumentOutOfRangeException(nameof(thumbnailIndex), $"Thumbnail index must be between 0 and {context.Thumbnails.Count}.");
        }
        using var image = context.Thumbnails.Export(thumbnailIndex);
        if (image.ImageType == ImageType.Jpeg)
        {
            var jpegData = image.AsSpan<byte>();
            MemoryStream output = new(jpegData.Length);
            output.Write(jpegData);
            output.Position = 0;
            return output;
        }
        else
        {
            throw new InvalidOperationException($"Unsupported image type: {image.ImageType}");
        }
    }

    private static void GetImage()
    {
        //string filePath = @"D:\Media\2012\2012-05-12\_MG_2112.CR2";
        var filePath = @"D:\Users\dykolchev.DYKBITS\Pictures\canon\2026\2026_03_14\3M6A1554.CR3";
        using var context = Factory.Open(filePath);
        //context.UnpackThumbnail();
        for (var index = 0; index < context.Thumbnails.Count; index++)
        {
            var thumbnail = context.Thumbnails[index];
            using var image = context.Thumbnails.Export(index);
            if (image.ImageType == ImageType.Jpeg)
            {
                var jpegData = image.AsSpan<byte>();
                using var output = File.Create($"test_{index}.jpg");
                output.Write(jpegData);
            }
            else
            {
                throw new InvalidOperationException($"Unsupported image type: {image.ImageType}");
            }
        }
        //context.Unpack();
        //context.DcrawProcess();
        //context.WriteDcrawPpmTiff("test.tif", true, 16);
        //context.DcrawProcess();
        //context.WriteDcrawPpmTiff("русское название.tif", true, 16);
    }
}

//public class ConsoleLogger : ILLamaLogger
//{
//    public void Log(string source, string message, ILLamaLogger.LogLevel level)
//    {
//        if (level >= ILLamaLogger.LogLevel.Info)
//            Console.WriteLine($"[{level}] {message}");
//    }
//}
