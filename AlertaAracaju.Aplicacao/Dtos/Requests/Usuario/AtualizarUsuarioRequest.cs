using System.ComponentModel.DataAnnotations;

namespace AlertaAracaju.Aplicacao.Dtos.Requests.Usuario;

public class AtualizarUsuarioRequest
{
    [Required(ErrorMessage = "O Nome de usuário é obrigatório! (ex: Ana Paula)")]
    public string? Nome { get; set; }

    [Required(ErrorMessage = "O E-mail é obrigatório!")]
    public string? Email { get; set; }

    [Required(ErrorMessage = "A Senha é obrigatória!")]
    public string? SenhaHash { get; set; }

}