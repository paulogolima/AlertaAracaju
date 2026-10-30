using AlertaAracaju.Dominio.Entidades;
using AlertaAracaju.Dominio.Interface;
using AlertaAracaju.Infraestrutura.Data;
using Microsoft.EntityFrameworkCore;

namespace AlertaAracaju.Infraestrutura.Repositorios;

public class OcorrenciaRepository : IOcorrenciaRepository
{
    private readonly OcorrenciaContext _context;

    public OcorrenciaRepository(OcorrenciaContext context)
    {
        _context = context;
    }

    // POST
    public async Task CriarOcorrenciaAsync(Ocorrencia ocorrencia)
    {
        _context.Ocorrencias.Add(ocorrencia);
        await _context.SaveChangesAsync();
    }

    // GET
    public async Task<IReadOnlyList<Ocorrencia>> ListarTodosAsync(int skip, int take)
    {
        return await _context.Ocorrencias.AsNoTracking().Skip(skip).Take(take).ToListAsync();
    }

    public async Task<Ocorrencia?> ListarPorIdAsync(int id)
    {
        return await _context.Ocorrencias.AsNoTracking().FirstOrDefaultAsync(o => o.Id == id);
    }

    // PUT
    public async Task AtualizarOcorrenciaAsync(Ocorrencia ocorrencia)
    {
        _context.Ocorrencias.Update(ocorrencia);
        await _context.SaveChangesAsync();
    }
}