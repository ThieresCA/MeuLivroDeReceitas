using FluentAssertions;
using MeuLivroDeReceitas.Comunication.Request;
using MeuLivroDeReceitas.Exceptions;
using System.Net;
using System.Text.Json;
using Xunit;

namespace WebApi.Test.V1.User.SignUp
{
    public class SignUpUserTest : ControllerBase
    {
        private const string METODO = "user";

        public SignUpUserTest(MeuLivroDeReceitasWebApplicationFactory<Program> factory) : base(factory)
        {

        }

        [Fact]
        public async Task SignUpUser_Sucess()
        {
            var request = new RequestCreateUserJson
            {
                Name = "teste",
                Email = "teste@gmail.com",
                Password = "123456",
                Phone = "27 9 9123-4587"
            };

            var response = await PostRequest(METODO, request);

            response.StatusCode.Should().Be(HttpStatusCode.Created);

            //pegando o body da resposta
            await using var responseBody = await response.Content.ReadAsStreamAsync();

            //fazendo um parse para um json
            var responseData = await JsonDocument.ParseAsync(responseBody);

            //entrando no json, pegando o elemento Token e verificando se ele está nulo ou vazio
            responseData.RootElement.GetProperty("token").GetString().Should().NotBeNullOrWhiteSpace();
        }

        [Fact]
        public async Task SignUpUser_EmptyPhone()
        {
            var request = new RequestCreateUserJson
            {
                Name = "teste",
                Email = "teste7@gmail.com",
                Password = "123456",
                Phone = ""
            };

            var response = await PostRequest(METODO, request);

            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);

            //pegando o body da resposta
            await using var responseBody = await response.Content.ReadAsStreamAsync();

            //fazendo um parse para um json
            var responseData = await JsonDocument.ParseAsync(responseBody);

            //entrando no json, pegando o elemento messages e devolvendo como um array de jsonElements
            var errors = responseData.RootElement.GetProperty("errorsMessages").EnumerateArray();

            //verifica o erro garantindo q seja o Empty_Phone
            errors.Should().ContainSingle().And.Contain(error => error.GetString().Equals(ResourceErrorMessage.EMPTY_PHONE));
        }
    }
}
