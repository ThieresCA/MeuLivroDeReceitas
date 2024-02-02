using AutoMapper;
using MeuLivroDeReceitas.Comunication.Request;

namespace MeuLivroDeReceitas.Application.Services.AutoMapper
{
    public class AutoMapperConfiguration : Profile
    {
        public AutoMapperConfiguration()
        {
            //receberemos a requisição e mapearemos como uma entidade, no caso o User
            CreateMap<CreateUserRequestJson, Domain.Entities.User>()
                //essa parte é feita pois a senha da entidade será criptografada ao invés
                //de salvar a senha diretamente como viria pelo request
                .ForMember(destino => destino.Password, config => config.Ignore());
            
            // em caso de o nome da prop do request seja diferente do nome da prop da entity
            // exemplo a seguir
            // CreateMap<RequestCreateUser, Domain.Entities.User>()
            // .ForMember(destino => destino.Password, config => config.MapFrom(request => request.Senha));
        }
    }
}
