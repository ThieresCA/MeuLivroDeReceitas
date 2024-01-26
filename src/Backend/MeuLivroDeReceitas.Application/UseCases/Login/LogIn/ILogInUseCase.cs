using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MeuLivroDeReceitas.Comunication.Request;
using MeuLivroDeReceitas.Comunication.Response;

namespace MeuLivroDeReceitas.Application.UseCases.Login.LogIn
{
    public interface ILogInUseCase
    {
        Task<ResponseLoginJson> Execute(LoginRequestJson input);
    }
}
