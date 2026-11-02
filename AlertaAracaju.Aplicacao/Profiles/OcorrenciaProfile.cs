using AlertaAracaju.Aplicacao.Dtos.Requests;
using AlertaAracaju.Aplicacao.Dtos.Responses;
using AlertaAracaju.Dominio.Entidades;
using AutoMapper;

namespace AlertaAracaju.Aplicacao.Profiles;

public class OcorrenciaProfile : Profile
{
    public OcorrenciaProfile()
    {
        CreateMap<CriarOcorrenciaRequest, Ocorrencia>();
        CreateMap<CoordenadasRequest, Coordenadas>();
        CreateMap<AtualizarOcorrenciaRequest, Ocorrencia>();
        CreateMap<AtualizarOcorrenciaPatchRequest, Ocorrencia>()
            .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));

        CreateMap<Ocorrencia, OcorrenciaResponse>();
        CreateMap<Coordenadas, CoordenadasResponse>();
    }
}
