using MeuLivroDeReceitas.Application.Services.Token;
using MeuLivroDeReceitas.Comunication.Request;
using MeuLivroDeReceitas.Comunication.Response;
using MeuLivroDeReceitas.Domain.Repository;
using MeuLivroDeReceitas.Exceptions.ExceptionsBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeuLivroDeReceitas.Application.UseCases.Login.LogIn
{
    public class LoginUseCase : ILogInUseCase
    {
        private readonly IUserReadOnlyRepository _readOnlyRepository;
        private readonly TokenController _tokenController;
        public LoginUseCase(IUserReadOnlyRepository repository, TokenController token)
        {
            _readOnlyRepository = repository;
            _tokenController = token;
        }
        public async Task<ResponseLoginJson> Execute(LoginRequestJson request)
        {
            var user = await _readOnlyRepository.Login(request.Email, request.Password);
            
            if (user == null)
            {
                throw new InvalidLogInException();
            }

            return new ResponseLoginJson
            {
                Name = user.Name,
                Token = _tokenController.TokenGenerator(user.Email)
            };
        }
    }
}
