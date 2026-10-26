using AlertaAracaju.Dominio.Entidades;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace AlertaAracaju.Aplicacao.Dtos.Requests;

public class CoordenadasRequest
{
    public double Latitude { get; set; }
    public double Longitude { get; set; }
}