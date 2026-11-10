using AlertaAracaju.Aplicacao.Dtos.Requests.Usuario;
using AlertaAracaju.Aplicacao.Dtos.Responses.Usuario;
using AlertaAracaju.Dominio.Entidades;
using AutoMapper;

namespace AlertaAracaju.Aplicacao.Profiles;

public class UsuarioProfile : Profile
{
    public UsuarioProfile()
    {
        CreateMap<CriarUsuarioRequest, Usuario>();
        CreateMap<AtualizarUsuarioRequest, Usuario>();
        CreateMap<AtualizarUsuarioPatchRequest, Ocorrencia>()
            .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));

        CreateMap<Usuario, UsuarioResponse>();
    }
}
