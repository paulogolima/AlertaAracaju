using System.ComponentModel.DataAnnotations;

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
    [Range(0, int.MaxValue, ErrorMessage = "A categoria deve ser válida.")]
    public int? Categoria { get; set; }

    [Required(ErrorMessage = "A urgência é obrigatória!")]
    [Range(0, int.MaxValue, ErrorMessage = "A urgência deve ser válida.")]
    public int? Urgencia { get; set; }

    [Required(ErrorMessage = "O status é obrigatório!")]
    [Range(0, int.MaxValue, ErrorMessage = "O status deve ser válido.")]
    public int? Status { get; set; }
}