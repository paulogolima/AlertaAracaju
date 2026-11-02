using System.ComponentModel.DataAnnotations;

namespace AlertaAracaju.Aplicacao.Dtos.Requests;

public class AtualizarStatusRequest
{
    [Required(ErrorMessage = "O status é obrigatório")]
    public string? Status { get; set; }
}
