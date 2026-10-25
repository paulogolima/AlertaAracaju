using AlertaAracaju.Dominio.Entidades;
using AlertaAracaju.Dominio.Interface;
using AlertaAracaju.Infraestrutura.Data;

namespace AlertaAracaju.Infraestrutura.Repositorios;

public class OcorrenciaRepository : IOcorrenciaRepository 
{
    private OcorrenciaContext _context;

    public OcorrenciaRepository(OcorrenciaContext context)
    {
        _context = context;
    }

    // POST
    public void AdicionarOcorrencia(Ocorrencia ocorrencia)
    {
        _context.Ocorrencias.Add(ocorrencia);
        _context.SaveChanges();
    }

    // GET
    public IEnumerable<Ocorrencia> ListarTodos(int skip, int take)
    {
        return _context.Ocorrencias.Skip(skip).Take(take);
    }

    public Ocorrencia? ListarPorId(int id)
    {
        return _context.Ocorrencias.FirstOrDefault(o => o.Id == id);
    }
}