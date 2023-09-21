using AutoMapper;
using MeuLivroDeReceitas.Application.Services.Token;
using MeuLivroDeReceitas.Comunication.Request;
using MeuLivroDeReceitas.Comunication.Response;
using MeuLivroDeReceitas.Domain.Repository;
using MeuLivroDeReceitas.Exceptions;
using MeuLivroDeReceitas.Exceptions.ExceptionsBase;

namespace MeuLivroDeReceitas.Application.UseCases.User.SignUp
{
    public class SignUpUseCase : ISignUpUseCase
    {
        private readonly IUserReadOnlyRepository _readOnlyRepository;
        private readonly IUserWriteOnlyRepository _repository;
        private readonly IMapper _mapper;
        private readonly TokenController _tokenController;

        public SignUpUseCase(IUserWriteOnlyRepository repository, IMapper mapper, TokenController tokenControler, IUserReadOnlyRepository readOnlyRepository)
        {
            _repository = repository;
            _mapper = mapper;
            _tokenController = tokenControler;
            _readOnlyRepository = readOnlyRepository;
        }

        public async Task<ResponseCreateUserJson> Execute(RequestCreateUserJson request)
        {
            await Validate(request);
            var entity = _mapper.Map<Domain.Entities.User>(request);
            //gerando um Hash utilizando o BCrypt
            entity.Password = BCrypt.Net.BCrypt.EnhancedHashPassword(request.Password, 13);
            
            //fazer a comparãção da senha com o hash gerado pelo BCrypt
            Console.WriteLine(BCrypt.Net.BCrypt.EnhancedVerify(request.Password, entity.Password));
            await _repository.AddUser(entity);
            var token = _tokenController.TokenGenerator(entity.Email);

            return new ResponseCreateUserJson
            {
                Token = token,
            };
        }

        public async Task Validate(RequestCreateUserJson request)
        {
            //criando uma instancia do validator
            var validator = new SignUpUserValidator();
            //fazendo a validação passando o request
            var result = validator.Validate(request);

            var emailAlreadyExists = await _readOnlyRepository.EmailAlreadyExists(request.Email);
            if(emailAlreadyExists)
            {
                result.Errors.Add(new FluentValidation.Results.ValidationFailure("email", ResourceErrorMessage.EMAIL_ALREADY_EXISTIS));  
            }

            if (!result.IsValid)
            {
                var errorMessage = result.Errors.Select(error => error.ErrorMessage).ToList();
                throw new ValidationErrorsExceptions(errorMessage);
            }
        }
    }
}
