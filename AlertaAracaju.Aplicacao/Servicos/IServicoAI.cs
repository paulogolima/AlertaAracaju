using AlertaAracaju.Aplicacao.Dtos.Responses;

namespace AlertaAracaju.Aplicacao.Servicos;

public interface IServicoIA
{
    Task<AnaliseOcorrenciaResposta> AnalisarOcorrenciaAsync(string titulo, string detalhes);
}

