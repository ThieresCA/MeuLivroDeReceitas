using MeuLivroDeReceitas.Comunication.Request;

namespace MeuLivroDeReceitas.Application.UseCases.User.SignUp
{
    public interface ISignUpUseCase
    {
        Task Execute(RequestCreateUserJson request);
    }
}
