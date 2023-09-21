using FluentValidation;
using MeuLivroDeReceitas.Comunication.Request;
using MeuLivroDeReceitas.Exceptions;
using System.Text.RegularExpressions;

namespace MeuLivroDeReceitas.Application.UseCases.User.SignUp
{
    public class SignUpUserValidator : AbstractValidator<RequestCreateUserJson>
    {
        public SignUpUserValidator()
        {
            RuleFor(user => user.Name).NotEmpty().WithMessage(ResourceErrorMessage.EMPTY_NAME);
            RuleFor(user => user.Email).NotEmpty().WithMessage(ResourceErrorMessage.EMPTY_EMAIL);
            RuleFor(user => user.Password).NotEmpty().WithMessage(ResourceErrorMessage.INVALID_PASSWORD);
            RuleFor(user => user.Phone).NotEmpty().WithMessage(ResourceErrorMessage.EMPTY_PHONE);

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

            //validação caso o telefone seja preenchido, necessário estar no padrão XX X XXXX-XXXX
            When(user => !string.IsNullOrWhiteSpace(user.Phone), () =>
            {
                RuleFor(user => user.Phone).Custom((phone, context) =>
                {
                    //criando o padrão do regex [] <- indica o que receberemos [0-9], no caso seriam numeros de 0 a 9
                    //dentro dos {2} virá a quantidade de strings q receberemos ali, que no exemplo é 2
                    string pattern = "[0-9]{2} [1-9]{1} [0-9]{4}-[0-9]{4}";
                    var isMatch = Regex.IsMatch(phone, pattern);

                    if (!isMatch)
                    {
                        // adicionando a mensagem de erro a regra
                        context.AddFailure(new FluentValidation.Results.ValidationFailure(nameof(phone), ResourceErrorMessage.INVALID_PHONE));
                    }
                });
            });
        }
    }
}
