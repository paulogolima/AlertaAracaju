using AlertaAracaju.Dominio.Entidades;
using AlertaAracaju.Dominio.Interface;

namespace AlertaAracaju.Infraestrutura.Repositorios;

public class OcorrenciaRepository : IOcorrenciaRepository 
{
    private readonly List<Ocorrencia> _ocorrencias = new List<Ocorrencia>();

    // POST
    public void AdicionarOcorrencia(Ocorrencia ocorrencia)
    {
        _ocorrencias.Add(ocorrencia);
    }

    // GET
    public IEnumerable<Ocorrencia> ListarTodos(int skip, int take)
    {
        return _ocorrencias.Skip(skip).Take(take);
    }

    public Ocorrencia? ListarPorId(int id)
    {
        return _ocorrencias.FirstOrDefault(o => o.Id == id);
    }
}

