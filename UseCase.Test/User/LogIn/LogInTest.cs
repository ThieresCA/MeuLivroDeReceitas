using AutoMapper;
using FluentAssertions;
using MeuLivroDeReceitas.Application.Services.Token;
using MeuLivroDeReceitas.Application.UseCases.Login.LogIn;
using MeuLivroDeReceitas.Application.UseCases.User.SignUp;
using MeuLivroDeReceitas.Comunication.Request;
using MeuLivroDeReceitas.Domain.Entities;
using MeuLivroDeReceitas.Domain.Repository;
using MeuLivroDeReceitas.Exceptions.ExceptionsBase;
using MeuLivroDeReceitas.Exceptions;
using Moq;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace UseCase.Test.User.LogIn
{
    public class LogInTest
    {
        private readonly LoginUseCase _LogInUseCase;
        private readonly TokenController _tokenControlerRepository;
        private readonly Mock<IUserReadOnlyRepository> _ReadOnlyRepository;


        public LogInTest()
        {
            _ReadOnlyRepository = new Mock<IUserReadOnlyRepository>();
            _tokenControlerRepository = new TokenController(1000, "TmFwZjcsOEE2UU1GXiZldyF9XC59dVMnTz98QH00Rl1DPHI=");
            _LogInUseCase = new LoginUseCase(_ReadOnlyRepository.Object, _tokenControlerRepository);
        }

        [Fact]
        public async Task Validate_Login_Sucess()
        {

            var request = new LoginRequestJson { Email = "user@gmail.com", Password = "string" };

            //criando um mock de usuário para ser comparado
            var user = new MeuLivroDeReceitas.Domain.Entities.User
            {
                Name = "teste",
                Password = "$2a$13$z6QsxYJ/xyTViLAv5HMgcO7uxN6WU5iubjCOC9v7Mo6bI/9J8AE7S",
                Email = "user@gmail.com"
            };

            //dizendo que ao chamar a função de Login, preciso que seja retornado esse usuário criado a cima
            _ReadOnlyRepository.Setup(i => i.FindByEmail(request.Email)).ReturnsAsync(user);

            //ao executar a verificação de senha dos usuários, acima foi setado o usuário a ser comparado.
            var useCase = await _LogInUseCase.Execute(request);

            useCase.Should().NotBeNull();
            useCase.Name.Should().NotBeNull().And.Be(user.Name);
            useCase.Token.Should().NotBeNullOrWhiteSpace();
        }

        [Fact]
        public async Task Invalid_Email_Request_Error()
        {
            var request = new LoginRequestJson { Email = "gmail.com", Password = "string" };

            Func<Task> result = async () => { await _LogInUseCase.Execute(request); };

            await result.Should().ThrowAsync<ValidationErrorsExceptions>()
                .Where(erro => erro.ErrorsMessage.Count == 1 &&
                erro.ErrorsMessage.Contains(ResourceErrorMessage.INVALID_LOGIN));
        }

        [Fact]
        public async Task Invalid_Password_Error()
        {
            var request = new LoginRequestJson { Email = "thieres@gmail.com", Password = "test12" };

            var user = new MeuLivroDeReceitas.Domain.Entities.User
            {
                Name = "teste",
                Password = "$2a$13$z6QsxYJ/xyTViLAv5HMgcO7uxN6WU5iubjCOC9v7Mo6bI/9J8AE7S",
                Email = "user@gmail.com"
            };

            _ReadOnlyRepository.Setup(i => i.FindByEmail(request.Email)).ReturnsAsync(user);

            Func<Task> result = async () => { await _LogInUseCase.Execute(request); };

            await result.Should().ThrowAsync<ValidationErrorsExceptions>().Where(error => error.ErrorsMessage.Count == 1 &&
            error.ErrorsMessage.Contains(ResourceErrorMessage.INVALID_LOGIN));
        }

        [Fact]
        public async Task Invalid_Password_Request_Error()
        {
            var request = new LoginRequestJson { Email = "thieres@gmail.com", Password = "test" };

            Func<Task> result = async () => { await _LogInUseCase.Execute(request); };

            await result.Should().ThrowAsync<ValidationErrorsExceptions>().Where(error => error.ErrorsMessage.Count == 1 &&
            error.ErrorsMessage.Contains(ResourceErrorMessage.INVALID_PASSWORD_6_CHAR_MIN));
        }
    }
}
