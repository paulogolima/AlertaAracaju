using AlertaAracaju.Aplicacao.Dtos.Requests;
using AlertaAracaju.Dominio.Entidades;
using AutoMapper;

namespace AlertaAracaju.Aplicacao.Profiles;

public class OcorrenciaProfile : Profile
{
    public OcorrenciaProfile()
    {
        CreateMap<CriarOcorrenciaRequest, Ocorrencia>();
        CreateMap<CoordenadasRequest, Coordenadas>();
    }
}
