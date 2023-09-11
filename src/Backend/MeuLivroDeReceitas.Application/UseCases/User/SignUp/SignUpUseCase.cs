using MeuLivroDeReceitas.Comunication.Request;
using MeuLivroDeReceitas.Exceptions.ExceptionsBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeuLivroDeReceitas.Application.UseCases.User.SignUp
{
    public class SignUpUseCase
    {
        public async Task Execute(RequestCreateUser request)
        {
            Validate(request);

        }

        public void Validate(RequestCreateUser request)
        {
            //criando uma instancia do validator
            var validator = new SignUpUserValidator();
            //fazendo a validação passando o request
            var result = validator.Validate(request);

            if (!result.IsValid)
            {
                var errorMessage = result.Errors.Select(error => error.ErrorMessage).ToList();
                throw new ValidationErrorsExceptions(errorMessage);
            }
        }
    }
}
