using FluentValidation;
using MeuLivroDeReceitas.Comunication.Request;
using MeuLivroDeReceitas.Exceptions;

namespace MeuLivroDeReceitas.Application.UseCases.User.SignUp
{
    public class UpdatePasswordValidator : AbstractValidator<string>
    {
        public UpdatePasswordValidator()
        {
            RuleFor(user => user).NotEmpty().WithMessage(ResourceErrorMessage.EMPTY_PASSWORD);

            //validação caso a string seja preenchida, necessário minimo de 6 caracteres
            When(user => !string.IsNullOrWhiteSpace(user), () =>
            {
                RuleFor(user => user).MinimumLength(6).WithMessage(ResourceErrorMessage.INVALID_PASSWORD_6_CHAR_MIN);
            });
        }
    }
}
