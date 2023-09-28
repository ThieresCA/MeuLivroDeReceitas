using FluentAssertions;
using MeuLivroDeReceitas.Application.UseCases.User.SignUp;
using MeuLivroDeReceitas.Comunication.Request;
using MeuLivroDeReceitas.Exceptions;
using Xunit;

namespace Validators.Test.Users.SignUp
{
    public class SignUpUserValidatorTest
    {
        [Fact]
        public void Validate_Error_Empty_Name()
        {
            var validator = new SignUpUserValidator();

            var request = new RequestCreateUserJson
            {
                Name = "",
                Email = "th@gmail.com",
                Password = "password",
                Phone = "27 9 9123-4587"
            };

            var result = validator.Validate(request);

            //dizendo que o resultado tem q ser um request inválido(!isValid), utilizando o FluentAssertions
            result.IsValid.Should().BeFalse();
            //aqui digo que a lista deve conter apenas 1 erro que é o de nome vazio
            result.Errors.Should().ContainSingle().And.Contain(error => error.ErrorMessage.Equals(ResourceErrorMessage.EMPTY_NAME)); 
        }
    }
}
