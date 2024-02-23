using FluentAssertions;
using MeuLivroDeReceitas.Application.Services.LogedInUser;
using MeuLivroDeReceitas.Application.UseCases.Login.LogIn;
using MeuLivroDeReceitas.Comunication.Request;
using MeuLivroDeReceitas.Domain.Entities;
using MeuLivroDeReceitas.Domain.Repository;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using WebApi.Test.V1.User.SignUp;
using Xunit;

namespace WebApi.Test.V1.User.UpdatePassword
{
    public class UpdatePasswordTest : ControllerBase
    {
        private const string METOD = "user/change-password";
        private MeuLivroDeReceitasWebApplicationFactory<Program> _factory;
        private SignUpUserTest _signUp;

        public UpdatePasswordTest(MeuLivroDeReceitasWebApplicationFactory<Program> factory) : base(factory)
        {
            _signUp = new SignUpUserTest(factory);

        }

        [Fact]
        public async Task Validate_Sucess()
        {

            //cadastrando o user para que o user buscado ao fazer o login seja o mesmo InMemory
            await _signUp.SignUpUser_Sucess();

            var createUser = new MeuLivroDeReceitas.Domain.Entities.User
            {
                Name = "teste",
                Password = "string3",
                Email = "teste@gmail.com",
                Id = 132165341,
                Phone = "27 9 9123-4587",
                CreateDate = DateTime.Now
            };

            var loginRequest = new MeuLivroDeReceitas.Comunication.Request.LoginRequestJson
            {
                Email = createUser.Email,
                Password = createUser.Password
            };

            //fazendo o login para obter o token
            var token = await Login(loginRequest);


            var request = new MeuLivroDeReceitas.Comunication.Request.UpdatePasswordRequestJson
            {
                NewPassword = "string6",
                Password = createUser.Password
            };

            var response = await PutRequest(METOD,request, token);


            response.StatusCode.Should().Be(HttpStatusCode.NoContent);
        }
    }
}
