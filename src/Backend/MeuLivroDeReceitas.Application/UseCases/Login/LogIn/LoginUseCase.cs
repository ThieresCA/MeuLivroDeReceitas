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
            await Validate(request);

            var user = await _readOnlyRepository.Login(request.Email);

            var verify = BCrypt.Net.BCrypt.EnhancedVerify(request.Password, user.Password);

            if (!verify)
            {
                return null;
            }

            return new ResponseLoginJson
            {
                Name = user.Name,
                Token = _tokenController.TokenGenerator(user.Email)
            };
        }

        public async Task Validate(LoginRequestJson loginRequest)
        {
            var validate = new LoginValidator();

            var result = validate.Validate(loginRequest);

            if (!result.IsValid)
            {
                var errorMessage = result.Errors.Select(error => error.ErrorMessage).ToList();
                throw new ValidationErrorsExceptions(errorMessage);
            }
        }
    }
}
