using AutoMapper;
using FluentAssertions;
using MeuLivroDeReceitas.Application.Services.AutoMapper;
using MeuLivroDeReceitas.Application.Services.Token;
using MeuLivroDeReceitas.Application.UseCases.User.SignUp;
using MeuLivroDeReceitas.Comunication.Request;
using MeuLivroDeReceitas.Domain.Repository;
using MeuLivroDeReceitas.Domain.Entities;
using Moq;
using Xunit;
using MeuLivroDeReceitas.Exceptions.ExceptionsBase;
using MeuLivroDeReceitas.Exceptions;

namespace UseCase.Test.User.SignUp
{
    public class SignUpUserTest
    {
        private readonly SignUpUseCase _signUpUseCase;
        private readonly Mock<IUserWriteOnlyRepository> _writeOnlyRepository;
        private readonly Mock<IMapper> _mapper;
        private readonly TokenController _tokenControlerRepository;
        private readonly Mock<IUserReadOnlyRepository> _ReadOnlyRepository;

        public SignUpUserTest()
        {
            _writeOnlyRepository = new Mock<IUserWriteOnlyRepository>();
            _mapper = new Mock<IMapper>();
            _tokenControlerRepository = new TokenController(1000, "TmFwZjcsOEE2UU1GXiZldyF9XC59dVMnTz98QH00Rl1DPHI=");
            _ReadOnlyRepository = new Mock<IUserReadOnlyRepository>();

            _signUpUseCase = new SignUpUseCase(_writeOnlyRepository.Object, _mapper.Object, _tokenControlerRepository, _ReadOnlyRepository.Object);
        }

        [Fact]
        public async Task Validate_SignUp_EmailAlreadyExist()
        {
            //Criar request para chamada da funcao
            var request = new RequestCreateUserJson
            {
                Name = "teste",
                Email = "teste@gmail.com",
                Password = "123456",
                Phone = "27 9 9123-4587"
            };

            //definindo que ao passar pela validação para ver se o email exite, essa função deve retornar true
            _ReadOnlyRepository.Setup(s => s.EmailAlreadyExists(request.Email)).ReturnsAsync(true);

            //executando a função assincrona
            Func<Task> result = async () => { await _signUpUseCase.Execute(request); };
            

            //assert
            await result.Should().ThrowAsync<ValidationErrorsExceptions>()
                .Where(erro => erro.ErrorsMessage.Count == 1 &&
                erro.ErrorsMessage.Contains(ResourceErrorMessage.EMAIL_ALREADY_EXISTIS));
        }

        [Fact]
        public async Task Validate_SignUp_Sucess()
        {
            //Criar request para chamada da funcao
            var request = new RequestCreateUserJson
            {
                Name = "teste",
                Email = "teste@gmail.com",
                Password = "123456",
                Phone = "27 9 9123-4587"
            };

            //Seta a interface do mapper para quando chamar o metodo "Map" que espera o
            //MeuLivroDeReceitas.Domain.Entities.User e passa um objeto do tipo RequestCreateUserJson (request)
            //Retornar um novo User
            _mapper.Setup(s => s.Map<MeuLivroDeReceitas.Domain.Entities.User>(request))
                .Returns(new MeuLivroDeReceitas.Domain.Entities.User()
                {
                    Name = "teste",
                    Password = "123456",
                    Email = "teste@gmail.com"
                });

            //executa a classe a ser testada
            var result = await _signUpUseCase.Execute(request);

            //Assert (validacoes do teste unitario)
            //
            _writeOnlyRepository.Verify(s => s.AddUser(It.IsAny<MeuLivroDeReceitas.Domain.Entities.User>()));

            Assert.NotNull(result.Token);
        }

        [Fact]
        public async Task Validate_SignUp_EmptyEmail()
        {
            //Criar request para chamada da funcao
            var request = new RequestCreateUserJson
            {
                Name = "teste",
                Email = "",
                Password = "123456",
                Phone = "27 9 9123-4587"
            };

            //executando a função assincrona
            Func<Task> result = async () => { await _signUpUseCase.Execute(request); };

            //assert
            await result.Should().ThrowAsync<ValidationErrorsExceptions>()
                .Where(erro => erro.ErrorsMessage.Count == 1 &&
                erro.ErrorsMessage.Contains(ResourceErrorMessage.EMPTY_EMAIL));
        }
    }
}
