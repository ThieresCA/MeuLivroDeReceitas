using FluentValidation;
using MeuLivroDeReceitas.Comunication.Request;
using MeuLivroDeReceitas.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeuLivroDeReceitas.Application.UseCases.Login.LogIn
{
    public class LoginValidator : AbstractValidator<LoginRequestJson>
    {
        public LoginValidator()
        {
            RuleFor(user => user.Email).NotEmpty().WithMessage(ResourceErrorMessage.EMPTY_EMAIL);

            //validação caso o email seja preenchido, deve ser um email valido
            When(user => !string.IsNullOrWhiteSpace(user.Email), () =>
            {
                RuleFor(user => user.Email).EmailAddress().WithMessage(ResourceErrorMessage.INVALID_EMAIL);
            });

            //validação caso a string seja preenchida, necessário minimo de 6 caracteres
            When(user => !string.IsNullOrWhiteSpace(user.Password), () =>
            {
                RuleFor(user => user.Password).MinimumLength(6).WithMessage(ResourceErrorMessage.INVALID_PASSWORD_6_CHAR_MIN);
            });
        }
    }
}
