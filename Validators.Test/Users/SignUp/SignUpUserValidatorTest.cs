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
        public void Validate_Sucess()
        {
            var validator = new SignUpUserValidator();

            var request = new RequestCreateUserJson
            {
                Name = "teste",
                Email = "teste@gmail.com",
                Password = "123456",
                Phone = "27 9 9123-4587"
            };

            var response = validator.Validate(request);

            response.IsValid.Should().BeTrue();
        }

        [Fact]
        public void Validate_Empty_Name()
        {
            var validator = new SignUpUserValidator();

            var request = new RequestCreateUserJson
            {
                Name = "",
                Email = "th@gmail.com",
                Password = "password",
                Phone = "27 9 9123-4587"
            };

            //aqui a gente armazena o resultado da validação do request
            var result = validator.Validate(request);

            //dizendo que o resultado tem q ser um request inválido(!isValid), utilizando o FluentAssertions
            result.IsValid.Should().BeFalse();
            //aqui digo que a lista deve conter apenas 1 erro que é o de nome vazio
            result.Errors.Should().ContainSingle().And.Contain(error => error.ErrorMessage.Equals(ResourceErrorMessage.EMPTY_NAME));
        }

        [Fact]
        public void Validate_Empty_Email()
        {
            var validator = new SignUpUserValidator();

            var request = new RequestCreateUserJson
            {
                Name = "teste",
                Email = "",
                Password = "password",
                Phone = "27 9 9123-4587"
            };

            var result = validator.Validate(request);

            result.IsValid.Should().BeFalse();

            result.Errors.Should().ContainSingle().And.Contain(error => error.ErrorMessage.Equals(ResourceErrorMessage.EMPTY_EMAIL));
        }

        [Fact]
        public void Validate_Empty_Password()
        {

            var validator = new SignUpUserValidator();

            var request = new RequestCreateUserJson
            {
                Name = "teste",
                Email = "teste@gmail.com",
                Password = "",
                Phone = "27 9 9123-4587"
            };

            var result = validator.Validate(request);

            result.IsValid.Should().BeFalse();

            result.Errors.Should().ContainSingle().And.Contain(error => error.ErrorMessage.Equals(ResourceErrorMessage.EMPTY_PASSWORD));
        }

        [Fact]
        public void Validate_Empty_Phone()
        {
            var validate = new SignUpUserValidator();

            var request = new RequestCreateUserJson
            {
                Name = "teste",
                Email = "teste@gmail.com",
                Password = "123456",
                Phone = ""
            };

            var result = validate.Validate(request);

            result.IsValid.Should().BeFalse();

            result.Errors.Should().ContainSingle().And.Contain(error => error.ErrorMessage.Equals(ResourceErrorMessage.EMPTY_PHONE));
        }

        [Fact]
        public void Validate_6Char_Password()
        {
            var validator = new SignUpUserValidator();

            var request = new RequestCreateUserJson
            {
                Name = "teste",
                Email = "teste@gmail.com",
                Password = "12345",
                Phone = "27 9 9123-4587"
            };

            var result = validator.Validate(request);

            result.IsValid.Should().BeFalse();

            result.Errors.Should().ContainSingle().And.Contain(errors => errors.ErrorMessage.Equals(ResourceErrorMessage.INVALID_PASSWORD_6_CHAR_MIN));
        }

        [Fact]
        public void Validate_Invalid_Email()
        {

            var validator = new SignUpUserValidator();

            var request = new RequestCreateUserJson
            {
                Name = "teste",
                Email = "testegmail.com",
                Password = "123456",
                Phone = "27 9 9123-4587"
            };

            var result = validator.Validate(request);

            result.IsValid.Should().BeFalse();

            result.Errors.Should().ContainSingle().And.Contain(errors => errors.ErrorMessage.Equals(ResourceErrorMessage.INVALID_EMAIL));
        }

        [Fact]
        public void Validate_Invalid_Phone()
        {

            var validator = new SignUpUserValidator();

            var request = new RequestCreateUserJson
            {
                Name = "teste",
                Email = "teste@gmail.com",
                Password = "123456",
                Phone = "27 9123-84587"
            };

            var result = validator.Validate(request);

            result.IsValid.Should().BeFalse();

            result.Errors.Should().ContainSingle().And.Contain(errors => errors.ErrorMessage.Equals(ResourceErrorMessage.INVALID_PHONE));
        }
    }
}
