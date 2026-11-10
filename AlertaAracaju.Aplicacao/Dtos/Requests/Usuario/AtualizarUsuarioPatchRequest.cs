namespace AlertaAracaju.Aplicacao.Dtos.Requests.Usuario;

public class AtualizarUsuarioPatchRequest
{
    public string? Nome { get; set; }
    public string? Email { get; set; }
    public string? SenhaHash { get; set; }
}