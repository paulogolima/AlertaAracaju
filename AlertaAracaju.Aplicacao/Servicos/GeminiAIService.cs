using System.Text;
using System.Text.Json;
using Microsoft.Extensions.Configuration;
using AlertaAracaju.Aplicacao.Dtos.Responses.Ocorrencia;

namespace AlertaAracaju.Aplicacao.Servicos;

public class GeminiAIService : IServicoIA
{
    private readonly HttpClient _httpClient;
    private readonly string _chaveAPI;

    public GeminiAIService(HttpClient httpClient, IConfiguration configuracao)
    {
        _httpClient = httpClient;
        _chaveAPI = configuracao["GeminiAI:ApiKey"] ?? throw new InvalidOperationException("GeminiAI:ApiKey não configurada");
    }

    public async Task<AnaliseOcorrenciaResposta> AnalisarOcorrenciaAsync(string titulo, string detalhes)
    {
        var prompt = $@"Você é um assistente encarregado de analisar ocorrências urbanas.
Reescreva o título e os detalhes em linguagem formal e objetiva. Também classifique a categoria e a urgência.

Entradas:
- Título original: {titulo}
- Detalhes originais: {detalhes}

Opções válidas de Categoria: ""AsfaltoEBuracos"", ""IluminacaoPublica"", ""LixoEEntulho"", ""RiscoEletrico"", ""VazamentoDeAgua"", ""Outros"".
Opções válidas de Urgência: ""Baixa"", ""Media"", ""Alta"".

Retorne APENAS um JSON neste formato:
{{
  ""titulo"": ""Título formal aqui"",
  ""detalhes"": ""Detalhes formais aqui"",
  ""categoria"": ""IluminacaoPublica"",
  ""urgencia"": ""Media""
}}";

        var corpoRequisicao = new
        {
            contents = new[] { new { parts = new[] { new { text = prompt } } } },
            generationConfig = new { response_mime_type = "application/json" }
        };

        try
        {
            var jsonEnvio = JsonSerializer.Serialize(corpoRequisicao);
            using var conteudo = new StringContent(jsonEnvio, Encoding.UTF8, "application/json");

            var url = $"https://generativelanguage.googleapis.com/v1beta/models/gemini-3.5-flash-lite:generateContent?key={_chaveAPI}";

            var resposta = await _httpClient.PostAsync(url, conteudo);
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

            using var docAnalise = JsonDocument.Parse(jsonIa);
            var raiz = docAnalise.RootElement;

            return new AnaliseOcorrenciaResposta
            {
                Titulo = raiz.GetProperty("titulo").GetString() ?? titulo,
                Detalhes = raiz.GetProperty("detalhes").GetString() ?? detalhes,
                Categoria = raiz.GetProperty("categoria").GetString() ?? "Outros",
                Urgencia = raiz.GetProperty("urgencia").GetString() ?? "Media",
                Status = "Pendente"
            };
        }
        catch (Exception ex)
        {
            Console.WriteLine($"[EXCEÇÃO C#]: {ex.Message}");
            return ObterPadrao(titulo, detalhes);
        }
    }

    private static AnaliseOcorrenciaResposta ObterPadrao(string titulo, string detalhes)
    {
        return new AnaliseOcorrenciaResposta
        {
            Titulo = titulo,
            Detalhes = detalhes,
            Categoria = "Outros",
            Urgencia = "Media",
            Status = "Pendente"
        };
    }
}