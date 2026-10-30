using System.ComponentModel.DataAnnotations;

namespace AlertaAracaju.Aplicacao.Dtos.Requests;

public class CoordenadasRequest
{
    [Range(-90.0, 90.0, ErrorMessage = "A latitude deve estar entre -90 e 90 graus.")]
    public double Latitude { get; set; }

    [Range(-180.0, 180.0, ErrorMessage = "A longitude deve estar entre -180 e 180 graus.")]
    public double Longitude { get; set; }
}