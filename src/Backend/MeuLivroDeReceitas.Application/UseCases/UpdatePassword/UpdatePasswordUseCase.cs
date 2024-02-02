using MeuLivroDeReceitas.Application.Services.LogedInUser;
using MeuLivroDeReceitas.Comunication.Request;
using MeuLivroDeReceitas.Domain.Repository;
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
        public UpdatePasswordUseCase(IUpdateOnlyRepository repository, ILoggedInUser loggedInUser)
        {
            _loggedInUser = loggedInUser;
            _repository = repository;
        }

        public async Task Execute(UpdatePasswordRequestJson request)
        {
            var userRetrived = await _loggedInUser.RetrieveUser();
            _repository.Update();
        }
    }
}
