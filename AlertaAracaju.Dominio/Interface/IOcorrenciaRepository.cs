using AlertaAracaju.Dominio.Entidades;

namespace AlertaAracaju.Dominio.Interface;

public interface IOcorrenciaRepository
{
    Task CriarOcorrenciaAsync(Ocorrencia ocorrencia);
    Task<IReadOnlyList<Ocorrencia>> ListarTodosAsync(int skip, int take);
    Task<Ocorrencia?> ListarPorIdAsync(int id);
    Task AtualizarOcorrenciaAsync(Ocorrencia ocorrencia);
    Task DeletarOcorrenciaAsync(Ocorrencia ocorrencia);
}