using AutoMapper;
using MeuLivroDeReceitas.Comunication.Request;
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
            //gerando um Hash utilizando o BCrypt
            entity.Password = BCrypt.Net.BCrypt.EnhancedHashPassword(request.Password, 13);
            
            //fazer a comparãção da senha com o hash gerado pelo BCrypt
            Console.WriteLine(BCrypt.Net.BCrypt.EnhancedVerify(request.Password, entity.Password));
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
