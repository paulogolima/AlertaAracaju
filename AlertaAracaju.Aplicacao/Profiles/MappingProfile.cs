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
        // Ocorrencia
        CreateMap<CoordenadasRequest, Coordenadas>();
        CreateMap<AtualizarOcorrenciaRequest, Ocorrencia>();
        CreateMap<Ocorrencia, OcorrenciaResponse>();
        CreateMap<Coordenadas, CoordenadasResponse>();

        // Usuario
        CreateMap<AtualizarUsuarioRequest, Usuario>();
        CreateMap<Usuario, UsuarioResponse>();
    }
}