using MeuLivroDeReceitas.Application.Services.LogedInUser;
using MeuLivroDeReceitas.Application.UseCases.User.SignUp;
using MeuLivroDeReceitas.Comunication.Request;
using MeuLivroDeReceitas.Domain.Repository;
using MeuLivroDeReceitas.Exceptions;
using MeuLivroDeReceitas.Exceptions.ExceptionsBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeuLivroDeReceitas.Application.UseCases.UpdatePassword
{
    public class UpdatePasswordUseCase : IUpdatePasswordUseCase
    {
        private ILoggedInUser _loggedInUser;
        private IUpdateOnlyRepository _repository;
        private UpdatePasswordValidator _validator;
        public UpdatePasswordUseCase(IUpdateOnlyRepository repository, ILoggedInUser loggedInUser)
        {
            _loggedInUser = loggedInUser;
            _repository = repository;
        }

        public async Task Execute(UpdatePasswordRequestJson request)
        {
            var userRetrived = await _loggedInUser.RetrieveUser();

            var user = await _repository.FindById(userRetrived.Id);

            Validate(request, user);

            user.Password = BCrypt.Net.BCrypt.EnhancedHashPassword(request.NewPassword, 13);

            _repository.Update(user);
        }

        public void Validate(UpdatePasswordRequestJson request, Domain.Entities.User user)
        {
            var validate = new UpdatePasswordValidator();

            var verify = BCrypt.Net.BCrypt.EnhancedVerify(request.Password, user.Password);

            if (!verify)
            {
                throw new Exception(ResourceErrorMessage.SAME_PASSWORD);
            }

            var response = validate.Validate(request.NewPassword);

            if (!response.IsValid)
            {
                var errorMessage = response.Errors.Select(error => error.ErrorMessage).ToList();
                throw new ValidationErrorsExceptions(errorMessage);
            }
        }
    }
}
