using AutoMapper;
using MeuLivroDeReceitas.Comunication.Request;
using MeuLivroDeReceitas.Domain.Repository;
using MeuLivroDeReceitas.Exceptions.ExceptionsBase;

namespace MeuLivroDeReceitas.Application.UseCases.User.SignUp
{
    public class SignUpUseCase
    {
        private readonly IUserWriteOnlyRepository _repository;
        private readonly IMapper _mapper;

        public SignUpUseCase(IUserWriteOnlyRepository repository)
        {
            _repository = repository;
        }

        public async Task Execute(RequestCreateUser request)
        {
            Validate(request);
            var entity = _mapper.Map<Domain.Entities.User>(request);
            entity.Password = "crypt";

            await _repository.AddUser(entity);

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
