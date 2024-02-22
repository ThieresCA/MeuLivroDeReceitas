using FluentAssertions;
using MeuLivroDeReceitas.Application.Services.LogedInUser;
using MeuLivroDeReceitas.Application.UseCases.UpdatePassword;
using MeuLivroDeReceitas.Application.UseCases.User.SignUp;
using MeuLivroDeReceitas.Comunication.Request;
using MeuLivroDeReceitas.Domain.Entities;
using MeuLivroDeReceitas.Domain.Repository;
using MeuLivroDeReceitas.Exceptions;
using MeuLivroDeReceitas.Exceptions.ExceptionsBase;
using Microsoft.AspNetCore.Http;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace UseCase.Test.User.UpdatePassword
{

    public class UpdatePasswordUseCaseTest
    {
        private UpdatePasswordUseCase _useCase;
        private Mock<IUpdateOnlyRepository> _updateOnlyRepository;
        private Mock<ILoggedInUser> _loggedUser;

        public UpdatePasswordUseCaseTest()
        {
            _loggedUser = new Mock<ILoggedInUser>();
            _updateOnlyRepository = new Mock<IUpdateOnlyRepository>();
            _useCase = new UpdatePasswordUseCase(_updateOnlyRepository.Object, _loggedUser.Object);
        }

        [Fact]
        public async Task Validate_Update_Password_Sucess()
        {
            //criando um usuário para receber de volta
            var createUser = new MeuLivroDeReceitas.Domain.Entities.User
            {
                Name = "name",
                Password = "$2a$13$Ux9JAr2COFySd6u/0CnIMOm0EvZATBfaiWGX6kddMkwo8Lk4g9F5.",
                Email = "email@gmail.com",
                Id = 132165341,
                Phone = "27 9 98874-2755",
                CreateDate = DateTime.Now
            };

            Func<Task> requestUpdate = async () =>
            {
                await _useCase.Execute(new UpdatePasswordRequestJson
                {
                    NewPassword = "NovaSenha",
                    Password = createUser.Password
                });
            };


            //setando que ao chamar a função de FindById o retorno será o usuário que criamos
            var user = _updateOnlyRepository.Setup(c => c.FindById(createUser.Id)).ReturnsAsync(createUser);


            var recoveredUser = _loggedUser.Setup(c => c.RetrieveUser()).ReturnsAsync(createUser);


            await requestUpdate.Should().NotThrowAsync();
        }

        [Fact]
        public async Task Validate_Update_Password_Error_Empty()
        {
            //criando um usuário para receber de volta
            var createUser = new MeuLivroDeReceitas.Domain.Entities.User
            {
                Name = "name",
                Password = "$2a$13$Ux9JAr2COFySd6u/0CnIMOm0EvZATBfaiWGX6kddMkwo8Lk4g9F5.",
                Email = "email@gmail.com",
                Id = 132165341,
                Phone = "27 9 98874-2755",
                CreateDate = DateTime.Now
            };

            Func<Task> requestUpdate = async () =>
            {
                await _useCase.Execute(new UpdatePasswordRequestJson
                {
                    NewPassword = "",
                    Password = createUser.Password
                });
            };


            //setando que ao chamar a função de FindById o retorno será o usuário que criamos
            var user = _updateOnlyRepository.Setup(c => c.FindById(createUser.Id)).ReturnsAsync(createUser);


            var recoveredUser = _loggedUser.Setup(c => c.RetrieveUser()).ReturnsAsync(createUser);


            await requestUpdate.Should().ThrowAsync<ValidationErrorsExceptions>(ResourceErrorMessage.EMPTY_PASSWORD);
        }

        [Fact]
        public async Task Validate_Update_Password_Error_Same_Password()
        {
            //criando um usuário para receber de volta
            var createUser = new MeuLivroDeReceitas.Domain.Entities.User
            {
                Name = "name",
                Password = "$2a$13$W9XuurHeAwOqe.xv1TQ/deDP6ESUMZQOcE65s6huFyXpmG3wnmyVm",
                Email = "email@gmail.com",
                Id = 132165341,
                Phone = "27 9 98874-2755",
                CreateDate = DateTime.Now
            };

            Func<Task> requestUpdate = async () =>
            {
                await _useCase.Execute(new UpdatePasswordRequestJson
                {
                    NewPassword = "string1",
                    Password = createUser.Password
                });
            };


            //setando que ao chamar a função de FindById o retorno será o usuário que criamos
            var user = _updateOnlyRepository.Setup(c => c.FindById(createUser.Id)).ReturnsAsync(createUser);


            var recoveredUser = _loggedUser.Setup(c => c.RetrieveUser()).ReturnsAsync(createUser);


            await requestUpdate.Should().ThrowAsync<ValidationErrorsExceptions>(ResourceErrorMessage.SAME_PASSWORD);
        }
    }
}
