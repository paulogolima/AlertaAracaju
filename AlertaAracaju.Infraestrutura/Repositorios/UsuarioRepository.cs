using AlertaAracaju.Dominio.Entidades;
using AlertaAracaju.Dominio.Interface;
using AlertaAracaju.Infraestrutura.Data;
using Microsoft.EntityFrameworkCore;

namespace AlertaAracaju.Infraestrutura.Repositorios;

public class UsuarioRepository(AppDbContext context) : IUsuarioRepository
{
    public async Task CriarUsuarioAsync(Usuario usuario)
    {
        context.Usuarios.Add(usuario);
        await context.SaveChangesAsync();
    }

    public async Task<IReadOnlyList<Usuario>> ListarTodosAsync(int skip, int take)
    {
        return await context.Usuarios
            .AsNoTracking()
            .OrderBy(u => u.Id)
            .Skip(skip)
            .Take(take)
            .ToListAsync();
    }

    public async Task<Usuario?> ListarPorIdAsync(int id)
    {
        return await context.Usuarios.AsNoTracking().FirstOrDefaultAsync(o => o.Id == id);
    }

    public async Task AtualizarUsuarioAsync(Usuario usuario)
    {
        context.Usuarios.Update(usuario);
        await context.SaveChangesAsync();
    }

    public async Task DeletarUsuarioAsync(Usuario usuario)
    {
        context.Usuarios.Remove(usuario);
        await context.SaveChangesAsync();
    }
}