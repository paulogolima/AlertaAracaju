using AlertaAracaju.Aplicacao.Dtos.Responses.Ocorrencia;

namespace AlertaAracaju.Aplicacao.Servicos;

public interface IServicoIA
{
    Task<AnaliseOcorrenciaResponse> AnalisarOcorrenciaAsync(string titulo, string detalhes);
}

