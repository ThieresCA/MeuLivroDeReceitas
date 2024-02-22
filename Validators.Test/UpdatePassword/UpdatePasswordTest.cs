using FluentAssertions;
using MeuLivroDeReceitas.Application.UseCases.User.SignUp;
using MeuLivroDeReceitas.Comunication.Request;
using MeuLivroDeReceitas.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Validators.Test.UpdatePassword
{
    public class UpdatePasswordTest
    {
        [Fact]
        public void Validate_Sucess()
        {
            var validator = new UpdatePasswordValidator();

            var request = new UpdatePasswordRequestJson
            {
                Password = "string2",
                NewPassword = "string3"
            };

            var response = validator.Validate(request.NewPassword);

            response.IsValid.Should().BeTrue();
        }

        [Fact]
        public void Validate_Error_Min_Lenght()
        {
            var validator = new UpdatePasswordValidator();

            var request = new UpdatePasswordRequestJson
            {
                Password = "string2",
                NewPassword = "strin"
            };

            var response = validator.Validate(request.NewPassword);

            response.Errors.Should().ContainSingle().And.Contain(error => error.ErrorMessage.Equals(ResourceErrorMessage.INVALID_PASSWORD_6_CHAR_MIN));
        }

        [Fact]
        public void Validate_Error_Empty_Password()
        {
            var validator = new UpdatePasswordValidator();

            var request = new UpdatePasswordRequestJson
            {
                Password = "string2",
                NewPassword = ""
            };

            var response = validator.Validate(request.NewPassword);

            response.Errors.Should().ContainSingle().And.Contain(error => error.ErrorMessage.Equals(ResourceErrorMessage.EMPTY_PASSWORD));
        }
    }
}
