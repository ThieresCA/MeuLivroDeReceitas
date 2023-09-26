using MeuLivroDeReceitas.Application.UseCases.User.SignUp;
using MeuLivroDeReceitas.Comunication.Request;
using MeuLivroDeReceitas.Comunication.Response;
using Microsoft.AspNetCore.Mvc;

namespace MeuLivroDeReceitas.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {

        [HttpPost]
        //aqui a gente deixa explicito qual será o status e oq receberemos
        [ProducesResponseType(typeof(ResponseCreateUserJson), StatusCodes.Status201Created)]
        public async Task<IActionResult> UserRegister([FromServices] ISignUpUseCase useCase,
                                                      [FromBody] RequestCreateUserJson request)
        {
            var result = await useCase.Execute(request);
            return Created(string.Empty ,result);
        }
    }
}