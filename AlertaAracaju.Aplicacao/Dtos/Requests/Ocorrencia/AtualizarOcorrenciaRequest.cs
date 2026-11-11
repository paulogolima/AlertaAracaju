using System.ComponentModel.DataAnnotations;
using AlertaAracaju.Dominio.Enums;

namespace AlertaAracaju.Aplicacao.Dtos.Requests.Ocorrencia;

public class AtualizarOcorrenciaRequest
{
    [Required(ErrorMessage = "O título da ocorrência é obrigatório! (ex: Semáforo quebrado)")]
    public string? Titulo { get; set; }

    [Required(ErrorMessage = "Os detalhes da ocorrência são obrigatórios! (ex: Semáforo apagado no cruzamento principal da avenida)")]
    [StringLength(500, MinimumLength = 10, ErrorMessage = "Os detalhes da ocorrência não podem conter menos de 10 caracteres.")]
    public string? Detalhes { get; set; }

    [Required(ErrorMessage = "A localização é obrigatória!")]
    public CoordenadasRequest? Localizacao { get; set; }

    [Required(ErrorMessage = "A categoria é obrigatória!")]
    public Categoria? Categoria { get; set; }

    [Required(ErrorMessage = "A urgência é obrigatória!")]
    public Urgencia? Urgencia { get; set; }

    [Required(ErrorMessage = "O status é obrigatório!")]
    public StatusOcorrencia? Status { get; set; }
}