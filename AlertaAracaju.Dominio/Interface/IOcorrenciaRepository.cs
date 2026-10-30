using AlertaAracaju.Dominio.Entidades;

namespace AlertaAracaju.Dominio.Interface;

// Interface que informa quais os métodos existem
public interface IOcorrenciaRepository
{
    Task CriarOcorrenciaAsync(Ocorrencia ocorrencia);
    Task<IReadOnlyList<Ocorrencia>> ListarTodosAsync(int skip, int take);
    Task<Ocorrencia?> ListarPorIdAsync(int id);
}