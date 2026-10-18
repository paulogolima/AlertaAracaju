using AlertaAracaju.Dominio.Entidades;
using AlertaAracaju.Dominio.Interface;
namespace AlertaAracaju.Infraestrutura.Repositorios;
public class OcorrenciaRepository : IOcorrenciaRepository
{
    private readonly List<Ocorrencia> _ocorrencias = new List<Ocorrencia>();

    public void AdicionarOcorrencia(Ocorrencia ocorrencia)
    {
        _ocorrencias.Add(ocorrencia);
    }

    public IEnumerable<Ocorrencia> ListarTodos()
    {
        return _ocorrencias;
    }

    //public void EditarOcorrencia(Ocorrencia ocorrencia)
    //{
    //    ocorrencia.Titulo = tituloAtualizado;
    //}
}

