// <copyright file="Gemma4PromptExecutor.cs" company="Dmitry Kolchev">
// Copyright (c) 2026 Dmitry Kolchev. All rights reserved.
// See LICENSE in the project root for license information
// </copyright>

using System.Diagnostics.CodeAnalysis;
using System.Text;

namespace Managed.LLama.Cpp.Gemma;

public class Gemma4PromptExecutor : IDisposable
{
    public const string ImageToTextPrompt = "Подробно опишите вложенное изображение с точки зрения профессионального фотографа" +
        ", сконцентрируйся на видимых деталях снимка.";

    public const string ImageToJsonPrompt = @"Вы — специализированный ИИ-модуль для глубокого анализа и каталогизации изображений. Ваша задача — извлечь из картинки метаданные и вернуть их строго в формате JSON.

ПРАВИЛА ОТВЕТА:
1. Выдайте ТОЛЬКО валидный JSON-код. Не пишите никаких вступлений, пояснений или тегов вроде ```json ... ```.
2. Текстовые поля заполняйте на русском языке.
3. Если информацию о каком-то поле невозможно определить, запишите значение null.

СТРУКТУРА JSON, КОТОРУЮ НЕОБХОДИМО ВЕРНУТЬ:
{
  ""imageType"": ""Тип (варианты: photo, screenshot, digital_art, scan, vector, meme)"",
  ""composition"": {
    ""viewAngle"": ""Ракурс (варианты: eye-level, top-down, low-angle, macro, wide-angle)"",
    ""lighting"": ""Освещение (варианты: daylight, studio, night, low-light, golden_hour, neon)"",
    ""dominantColors"": [""Три главных цвета на изображении через запятую""]
  },
  ""context"": {
    ""setting"": ""Где происходит действие (варианты: indoor, outdoor, urban, nature, studio, unknown)"",
    ""approximateLocation"": ""Предположительное место, страна, город или тип окружения (например: 'Офис', 'Пляж в Таиланде', 'Улица европейского города')"",
    ""timeOfDay"": ""Время суток (варианты: morning, afternoon, evening, night, unknown)"",
    ""weather"": ""Погода (варианты: sunny, cloudy, rainy, snowy, foggy, indoor)""
  },
  ""content_analysis"": {
    ""mainObjects"": [""Список 3-5 ключевых физических объектов на переднем плане""],
    ""peopleDetected"": true/false,
    ""peopleCount"": 0, // если людей нет или их точное число сложно посчитать, ставьте 0
    ""emotionalTone"": ""Общая атмосфера кадра (например: 'уютная', 'динамическая', 'тревожная', 'праздничная')"",
    ""brandsAndLogos"": [""Найденные коммерческие бренды, логотипы или марки машин. Если нет — пустой массив""]
  },
  ""ocr"": {
    ""hasText"": true/false,
    ""extractedText"": ""Запишите весь видимый текст на картинке, включая вывески, номера машин или документы. Соблюдайте регистр. Если текста нет — null""
  },
  ""description"": ""Подробное описание изображения с точки зрения профессионального фотографа, с подробным описанием видимых деталей на снимке."",
  ""tags"": [""5-10 точных тегов-существительных для быстрого поиска в БД""]
}";

    private readonly string _modelPath;
    private readonly string _mtmdPath;
    private LLamaModel? _model;
    private LLamaContext? _context;
    private MtmdContext? _mtmd;
    private MtmdBitmap? _bitmap;
    private LLamaSampler? _sampler;
    private StateMachine? _stateMachine;

    public Gemma4PromptExecutor(string modelPath, string mtmdPath)
    {
        _modelPath = modelPath;
        _mtmdPath = mtmdPath;
    }

    public string? SystemPrompt { get; set; }

    public string? UserPrompt { get; set; }

    public bool ThinkingMode { get; set; }

    public string? MediaMarker { get; set; }

    public int MaxOutputTokens { get; set; } = 500;

    protected virtual string GeneratePrompt(string? systemPrompt = default, string? userPrompt = default)
    {
        systemPrompt ??= SystemPrompt;
        userPrompt ??= UserPrompt;
        var think = ThinkingMode ? "<|think|>\n" : "";
        return $"<|turn>system\n{think}" +
                    $"{systemPrompt}<turn|>\n" +
                    $"<|turn>user\n{MediaMarker}\n{userPrompt}<turn|>\n" +
                    "<|turn>model\n";
    }

    public void Dispose()
    {
        _sampler?.Dispose();
        _context?.Dispose();
        _mtmd?.Dispose();
        _model?.Dispose();
    }

    [MemberNotNull(nameof(_model), nameof(_context), nameof(_mtmd), nameof(_model), nameof(_stateMachine), nameof(_sampler))]
    public void Load(
        bool useGpu = true,
        int mainGpu = 0,
        bool useMemoryMap = true,
        int gpuLayerCount = 99,
        int contextSize = 4096,
        int batchSize = 512,
        bool swaFull = false,
        GgmlType typeK = GgmlType.BF16,
        GgmlType typeV = GgmlType.BF16,
        LLamaAttentionType attentionType = LLamaAttentionType.Unspecified,
        LLamaFlashAttentionType flashAttentionType = LLamaFlashAttentionType.Enabled,
        int topK = 64,
        float topP = 0.95f,
        float temp = 1f,
        int seed = int.MinValue,
        bool thinkingMode = false)
    {
        var modelParams = LLamaModelParams.Default();
        modelParams.GpuLayerCount = 99;
        modelParams.MainGpu = mainGpu;
        modelParams.UseMemoryMap = useMemoryMap;
        _model = LLamaModel.LoadFromFile(
            _modelPath,
            modelParams);

        var contextParams = LLamaContextParams.Default();
        contextParams.ContextSize = (uint)contextSize;
        contextParams.BatchSize = (uint)batchSize;
        contextParams.ContextType = LLamaContextType.Default;
        contextParams.SwaFull = swaFull;
        contextParams.TypeK = typeK;
        contextParams.TypeV = typeV;
        contextParams.AttentionType = attentionType;
        contextParams.FlashAttentionType = flashAttentionType;

        _context = LLamaContext.InitFromModel(_model, contextParams);

        var mtmdParameters = MtmdContextParams.Default();
        mtmdParameters.UseGpu = useGpu;
        mtmdParameters.FlashAttentionType = flashAttentionType;
        _mtmd = MtmdContext.InitFromFile(_mtmdPath, _model, mtmdParameters);

        var samplerChainParams = LLamaSamplerChainParams.Default();
        _sampler = LLamaSampler.ChainInit(samplerChainParams);
        _sampler
            .AddTopK(k: topK)
            .AddTopP(p: topP, minKeep: 1)
            .AddTemp(t: temp)
            .AddSeed(unchecked((uint)seed));

        _stateMachine = new StateMachine(_model);
        ThinkingMode = thinkingMode;
        MediaMarker = _mtmd.Marker;
    }

    public string Describe(int sequenceId = 0, string? systemPrompt = default, string? userPrompt = default)
    {
        EnsureLoaded();
        var prompt = GeneratePrompt(systemPrompt: systemPrompt, userPrompt: userPrompt);

        var isFirst = (_context.Memory.GetMaxPosition(sequenceId) + 1) == 0;

        int batchLength;
        LLamaBatch batch = default;
        MtmdInputChunks? chunks = default;

        if (_bitmap != null)
        {
            chunks = _mtmd.Tokenize(prompt, isFirst, true, [_bitmap], out batchLength);
        }
        else
        {
            var tokens = _context.Tokenize(_model.Vocabulary, prompt, isFirst, true);
            batch = new LLamaBatch(tokens);
            batchLength = batch.Length;
        }
        var tokenCount = 0;
        var contextLength = _context.ContextLength;
        var startOfThinkingPosition = -1;
        var endOfThinkingPosition = -1;
        var response = new StringBuilder();
        _stateMachine.Reset();
        for (; tokenCount < MaxOutputTokens;)
        {
            var currentPos = _context.Memory.GetMaxPosition(sequenceId) + 1;
            if (currentPos + batchLength > contextLength)
            {
                Console.WriteLine("\u001B[0m");
                Console.WriteLine("context size exceeded");
                throw new InvalidOperationException("context size exceeded");
            }

            if (chunks != null)
            {
                currentPos = _mtmd.EvalChunks(_context, chunks, currentPos, sequenceId, 512, true);
                chunks.Dispose();
                chunks = null;
            }
            else
            {
                currentPos = _context.Decode(batch, currentPos, sequenceId);
            }

            var newToken = _sampler.Sample(_context, -1);

            _stateMachine.Process(newToken);
            var piece = _model.Vocabulary.TokenToPiece(newToken, 0, true);
            //Console.Write(piece);

            if (_stateMachine.CurrentState is StateMachine.State.StartOfThinking)
            {
                startOfThinkingPosition = currentPos;
                Console.WriteLine("\n[Start Of Thinking]");
            }
            else if (_stateMachine.CurrentState is StateMachine.State.ThinkingInprogess)
            {
                Console.Write("\u001B[37m");
                Console.Write(piece);
                Console.Write("\u001B[0m");
            }
            else if (_stateMachine.CurrentState is StateMachine.State.EndOfThinking)
            {
                endOfThinkingPosition = currentPos;
                Console.WriteLine("\n[End Of Thinking]");
            }
            else if (_stateMachine.CurrentState is StateMachine.State.ResponseInprogress)
            {
                Console.Write("\u001B[32m");
                Console.Write(piece);
                Console.Write("\u001B[0m");
                response.Append(piece);
            }
            else if (_stateMachine.CurrentState is StateMachine.State.EndOfGeneration)
            {
                if (startOfThinkingPosition >= 0 && endOfThinkingPosition >= 0)
                {
                    _context.Memory.Remove(0, startOfThinkingPosition, endOfThinkingPosition);
                    Console.WriteLine($"\n[Removing context from {startOfThinkingPosition} to {endOfThinkingPosition}]");
                }
                break;
            }

            tokenCount++;
            batch = new LLamaBatch(newToken);
        }
        return response.ToString();
    }

    public void LoadBitmap(string path)
    {
        EnsureLoaded();
        _bitmap?.Dispose();
        _bitmap = _mtmd.LoadBitmap(path);
    }

    public void CleadContext()
    {
        EnsureLoaded();
        _context.Memory.Remove(0);
    }

    public void Clear()
    {
        EnsureLoaded();
        _bitmap?.Dispose();
        _context.Memory.Remove(0);
    }

    [MemberNotNull(nameof(_model), nameof(_context), nameof(_mtmd), nameof(_stateMachine), nameof(_sampler))]
    private void EnsureLoaded()
    {
        if (_model is null || _context is null || _mtmd is null || _stateMachine is null || _sampler is null)
        {
            throw new InvalidOperationException("model not loaded");
        }
    }
}
