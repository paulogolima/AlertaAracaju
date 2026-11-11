using System.ComponentModel.DataAnnotations;
using AlertaAracaju.Dominio.Enums;

namespace AlertaAracaju.Aplicacao.Dtos.Requests.Ocorrencia;

public class AtualizarStatusRequest
{
    [Required(ErrorMessage = "O status é obrigatório")]
    public StatusOcorrencia? Status { get; set; }
}