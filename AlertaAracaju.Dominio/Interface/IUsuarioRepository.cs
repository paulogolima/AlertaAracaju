using AlertaAracaju.Dominio.Entidades;

namespace AlertaAracaju.Dominio.Interface;

public interface IUsuarioRepository
{
    Task CriarUsuarioAsync(Usuario usuario);
    Task<IReadOnlyList<Usuario>> ListarTodosAsync(int skip, int take);
    Task<Usuario?> ListarPorIdAsync(int id);
    Task AtualizarUsuarioAsync(Usuario usuario);
    Task DeletarUsuarioAsync(Usuario usuario);
}