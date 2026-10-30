using System.ComponentModel.DataAnnotations;

namespace AlertaAracaju.Aplicacao.Dtos.Requests;

public class AtualizarOcorrenciaRequest
{
    [Required(ErrorMessage = "O título da ocorrência é obrigatório! (ex: Semáforo quebrado)")]
    public string? Titulo { get; set; }

    [Required(ErrorMessage = "Os detalhes da ocorrência são obrigatórios! (ex: Semáforo apagado no cruzamento principal da avenida)")]
    [StringLength(500, MinimumLength = 10, ErrorMessage = "Os detalhes da ocorrência não podem conter menos de 10 caracteres.")]
    public string? Detalhes { get; set; }

    [Required(ErrorMessage = "A localização é obrigatória!")]
    public CoordenadasRequest? Localizacao { get; set; }
}