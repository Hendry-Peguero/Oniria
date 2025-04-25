using MediatR;
using Microsoft.Extensions.Options;
using Oniria.Core.Application.Features.Base;
using Oniria.Core.Application.Features.DeepSeek.Commands;
using Oniria.Core.Application.Features.EmotionalStates.Queries;
using Oniria.Core.Dtos.DreamAnalsys.Response;
using Oniria.Infrastructure.Shared.Entities;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Text.RegularExpressions;

namespace Oniria.Infrastructure.Shared.Features.DeepSeek.Commands
{
    public class AnalyzeDreamByPromptAsyncCommandHandler : IRequestHandler<AnalyzeDreamByPromptAsyncCommand, OperationResult<DreamAnalysisDeepSeekResponse>>
    {
        private readonly HttpClient httpClient;
        private readonly DeepSeekSettings deepSeekSettings;
        private readonly IMediator mediator;

        public AnalyzeDreamByPromptAsyncCommandHandler(
            HttpClient httpClient,
            IOptions<DeepSeekSettings> deepSeekSettings,
            IMediator mediator
        )
        {
            this.httpClient = httpClient;
            this.deepSeekSettings = deepSeekSettings.Value;
            this.mediator = mediator;

            // Config base url
            this.httpClient.BaseAddress = new Uri(this.deepSeekSettings.BaseUrl);
            this.httpClient.Timeout = TimeSpan.FromSeconds(300);
        }

        public async Task<OperationResult<DreamAnalysisDeepSeekResponse>> Handle(AnalyzeDreamByPromptAsyncCommand request, CancellationToken cancellationToken)
        {
            var result = OperationResult<DreamAnalysisDeepSeekResponse>.Create();
            var emotionsResult = await mediator.Send(new GetAllEmotionalStatesAsyncQuery());

            if (!emotionsResult.IsSuccess)
            {
                result.AddError("An error occurred obtaining emotional states");
                return result;
            }

            try
            {
                var requestBody = new
                {
                    model = "deepseek-chat",
                    messages = new[] {
                        new {
                            role = "user",
                            content = $@"
                                Analiza este sueño en español y devuelve SOLO un objeto JSON con las siguientes propiedades:
                                1. 'DreamTitle': Título literal del sueño, puede ser una frase destacada o algo que represente lo que la persona soñó.
                                2. 'AnalysisTitle': Título creativo que resuma la esencia o interpretación del sueño.
                                3. 'EmotionalState': Emoción principal identificada (DEBE ser una de: {string.Join(',', emotionsResult.Data!.Select(e => $"'{e.Description}'"))})
                                4. 'Recommendation': Recomendación práctica para mejorar el estado emocional
                                5. 'PatternBehaviour': Interpretación del sueño junto con patrones de comportamiento identificados

                                Formato requerido (en español):
                                {{
                                    ""DreamTitle"": ""..."",
                                    ""AnalysisTitle"": ""..."",
                                    ""EmotionalState"": ""..."",
                                    ""Recommendation"": ""..."",
                                    ""PatternBehaviour"": ""...""
                                }}

                                Sueño a analizar:
                                {request.DreamPrompt}
                            "
                        }
                    },
                    temperature = 0.5,
                    max_tokens = 650
                };

                var requestMessage = new HttpRequestMessage(HttpMethod.Post, "v1/chat/completions")
                {
                    Content = new StringContent(
                        JsonSerializer.Serialize(requestBody),
                        Encoding.UTF8,
                        "application/json")
                };

                requestMessage.Headers.Authorization = new AuthenticationHeaderValue(
                    "Bearer",
                    deepSeekSettings.ApiKey
                );

                var response = await httpClient.SendAsync(requestMessage);
                var responseContent = await response.Content.ReadAsStringAsync();

                if (!response.IsSuccessStatusCode)
                {
                    result.AddError("Artificial intelligence was not able to analyze the dream");
                    return result;
                }

                // Procesar la respuesta
                using var jsonDoc = JsonDocument.Parse(responseContent);
                var responseText = jsonDoc.RootElement
                    .GetProperty("choices")[0]
                    .GetProperty("message")
                    .GetProperty("content")
                    .GetString();

                var jsonMatch = Regex.Match(responseText, @"\{.*\}", RegexOptions.Singleline);

                if (!jsonMatch.Success)
                {
                    result.AddError("The json object generated by the AI is not valid.");
                    return result;
                }

                // Deserializar el contenido
                var resultResponse = JsonSerializer.Deserialize<DreamAnalysisDeepSeekResponse>(
                    jsonMatch.Value,
                    new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true,
                        AllowTrailingCommas = true
                    }
                );

                if (
                    resultResponse == null ||
                    string.IsNullOrWhiteSpace(resultResponse.DreamTitle) ||
                    string.IsNullOrWhiteSpace(resultResponse.AnalysisTitle) ||
                    string.IsNullOrWhiteSpace(resultResponse.EmotionalState) ||
                    string.IsNullOrWhiteSpace(resultResponse.Recommendation) ||
                    string.IsNullOrWhiteSpace(resultResponse.PatternBehaviour)
                )
                {
                    result.AddError("Key fields were not included in the artificial intelligence response.");
                    return result;
                }

                result.Data = resultResponse;
            }
            catch (Exception ex)
            {
                result.AddError("An error occurred in analyzing the dream in general.");
            }

            return result;
        }
    }
}
