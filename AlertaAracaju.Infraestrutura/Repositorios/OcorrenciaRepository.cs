using AlertaAracaju.Dominio.Entidades;
using AlertaAracaju.Dominio.Interface;
using AlertaAracaju.Infraestrutura.Data;
using Microsoft.EntityFrameworkCore;

namespace AlertaAracaju.Infraestrutura.Repositorios;

public class OcorrenciaRepository(AppDbContext context) : IOcorrenciaRepository
{
    public async Task CriarOcorrenciaAsync(Ocorrencia ocorrencia)
    {
        context.Ocorrencias.Add(ocorrencia);
        await context.SaveChangesAsync();
    }

    public async Task<IReadOnlyList<Ocorrencia>> ListarTodosAsync(int skip, int take)
    {
        return await context.Ocorrencias.AsNoTracking().Skip(skip).Take(take).ToListAsync();
    }

    public async Task<Ocorrencia?> ListarPorIdAsync(int id)
    {
        return await context.Ocorrencias.AsNoTracking().FirstOrDefaultAsync(o => o.Id == id);
    }

    public async Task AtualizarOcorrenciaAsync(Ocorrencia ocorrencia)
    {
        context.Ocorrencias.Update(ocorrencia);
        await context.SaveChangesAsync();
    }

    public async Task AtualizarOcorrenciaParcialAsync(Ocorrencia ocorrencia)
    {
        context.Ocorrencias.Update(ocorrencia);
        await context.SaveChangesAsync();
    }

    public async Task DeletarOcorrenciaAsync(Ocorrencia ocorrencia)
    {
        context.Ocorrencias.Remove(ocorrencia);
        await context.SaveChangesAsync();
    }
}
