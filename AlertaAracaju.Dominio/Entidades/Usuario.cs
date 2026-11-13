namespace AlertaAracaju.Dominio.Entidades;

public class Usuario
{
    protected Usuario()
    {
        Nome = null!;
        Email = null!;
        SenhaHash = null!;
    }
    public Usuario(int id, string nome, string email, string senhaHash)
    {
        Id = id;
        Nome = nome;
        Email = email;
        SenhaHash = senhaHash;
        DataEHora = DateTime.UtcNow;
    }

    public int Id { get; private set; }
    public string Nome { get; private set; }
    public string Email { get; private set; }
    public string SenhaHash { get; private set; }
    public DateTime DataEHora { get; private set; } = DateTime.UtcNow;
}