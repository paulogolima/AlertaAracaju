using AlertaAracaju.Aplicacao.Dtos.Requests.Ocorrencia;
using AlertaAracaju.Aplicacao.Dtos.Requests.Usuario;
using AlertaAracaju.Aplicacao.Dtos.Responses.Ocorrencia;
using AlertaAracaju.Aplicacao.Dtos.Responses.Usuario;
using AlertaAracaju.Dominio.Entidades;
using AutoMapper;

namespace AlertaAracaju.Aplicacao.Profiles;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        // Mapeamento de Ocorrencias
        CreateMap<CriarOcorrenciaRequest, Ocorrencia>();
        CreateMap<CoordenadasRequest, Coordenadas>();
        CreateMap<AtualizarOcorrenciaRequest, Ocorrencia>();
        CreateMap<AtualizarOcorrenciaPatchRequest, Ocorrencia>()
            .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));

        CreateMap<Ocorrencia, OcorrenciaResponse>();
        CreateMap<Coordenadas, CoordenadasResponse>();

        // Mapeamentos de Usuario
        CreateMap<CriarUsuarioRequest, Usuario>();
        CreateMap<AtualizarUsuarioRequest, Usuario>();
        CreateMap<AtualizarUsuarioPatchRequest, Usuario>()
            .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
        CreateMap<Usuario, UsuarioResponse>();
    }
}