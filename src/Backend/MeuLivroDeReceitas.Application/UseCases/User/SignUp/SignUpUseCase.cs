using AutoMapper;
using MeuLivroDeReceitas.Comunication.Request;
using MeuLivroDeReceitas.Domain.Entities;
using MeuLivroDeReceitas.Domain.Repository;
using MeuLivroDeReceitas.Exceptions.ExceptionsBase;

namespace MeuLivroDeReceitas.Application.UseCases.User.SignUp
{
    public class SignUpUseCase : ISignUpUseCase
    {
        private readonly IUserWriteOnlyRepository _repository;
        private readonly IMapper _mapper;

        public SignUpUseCase(IUserWriteOnlyRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task Execute(RequestCreateUserJson request)
        {
            Validate(request);
            var entity = _mapper.Map<Domain.Entities.User>(request);
            entity.Password = "crypt";

            await _repository.AddUser(entity);
        }

        public void Validate(RequestCreateUserJson request)
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
