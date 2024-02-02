using MeuLivroDeReceitas.Comunication.Request;
using MeuLivroDeReceitas.Comunication.Response;

namespace MeuLivroDeReceitas.Application.UseCases.User.SignUp
{
    public interface ISignUpUseCase
    {
        Task<ResponseCreateUserJson> Execute(CreateUserRequestJson request);
    }
}
