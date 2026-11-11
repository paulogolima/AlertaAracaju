using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using AlertaAracaju.Aplicacao.Dtos.Responses.Ocorrencia;
using Microsoft.Extensions.Configuration;

namespace AlertaAracaju.Aplicacao.Servicos;

public class GeminiAIService(HttpClient httpClient, IConfiguration configuracao) : IServicoIA
{
    private const string Url = "https://generativelanguage.googleapis.com/v1beta/models/gemini-3.5-flash-lite:generateContent";

    private static readonly JsonSerializerOptions OpcoesJson = new()
    {
        PropertyNameCaseInsensitive = true,
        Converters = { new JsonStringEnumConverter() }
    };

    private readonly string _chaveAPI = configuracao["GeminiAI:ApiKey"]
        ?? throw new InvalidOperationException("GeminiAI:ApiKey não configurada");

    public async Task<AnaliseOcorrenciaResponse> AnalisarOcorrenciaAsync(string titulo, string detalhes)
    {
        var prompt = $$"""
            Você é um assistente encarregado de analisar ocorrências urbanas.
            Reescreva o título e os detalhes em linguagem formal e objetiva. Também classifique a categoria e a urgência.

            Entradas:
            - Título original: {{titulo}}
            - Detalhes originais: {{detalhes}}

            Opções válidas de Categoria: "AsfaltoEBuracos", "IluminacaoPublica", "LixoEEntulho", "RiscoEletrico", "VazamentoDeAgua", "Outros".
            Opções válidas de Urgência: "Baixa", "Media", "Alta".

            Retorne APENAS um JSON neste formato:
            {
              "titulo": "Título formal aqui",
              "detalhes": "Detalhes formais aqui",
              "categoria": "IluminacaoPublica",
              "urgencia": "Media"
            }
            """;

        var corpoRequisicao = new
        {
            contents = new[] { new { parts = new[] { new { text = prompt } } } },
            generationConfig = new { response_mime_type = "application/json" }
        };

        try
        {
            using var requisicao = new HttpRequestMessage(HttpMethod.Post, Url)
            {
                Content = new StringContent(JsonSerializer.Serialize(corpoRequisicao), Encoding.UTF8, "application/json")
            };
            requisicao.Headers.Add("x-goog-api-key", _chaveAPI);

            var resposta = await httpClient.SendAsync(requisicao);
            var textoResposta = await resposta.Content.ReadAsStringAsync();

            if (!resposta.IsSuccessStatusCode)
            {
                Console.WriteLine($"[ERRO GEMINI {resposta.StatusCode}]: {textoResposta}");
                return ObterPadrao(titulo, detalhes);
            }

            using var doc = JsonDocument.Parse(textoResposta);
            var jsonIa = doc.RootElement
                .GetProperty("candidates")[0]
                .GetProperty("content")
                .GetProperty("parts")[0]
                .GetProperty("text")
                .GetString()!;

            var analise = JsonSerializer.Deserialize<AnaliseOcorrenciaResponse>(jsonIa, OpcoesJson)
                ?? throw new JsonException("A IA devolveu um JSON vazio.");

            if (string.IsNullOrWhiteSpace(analise.Titulo)) analise.Titulo = titulo;
            if (string.IsNullOrWhiteSpace(analise.Detalhes)) analise.Detalhes = detalhes;

            return analise;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"[EXCEÇÃO C#]: {ex.Message}");
            return ObterPadrao(titulo, detalhes);
        }
    }

    private static AnaliseOcorrenciaResponse ObterPadrao(string titulo, string detalhes) => new()
    {
        Titulo = titulo,
        Detalhes = detalhes
    };
}