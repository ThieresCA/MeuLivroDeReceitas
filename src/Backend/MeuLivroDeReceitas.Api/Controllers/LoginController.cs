using MeuLivroDeReceitas.Application.UseCases.Login.LogIn;
using MeuLivroDeReceitas.Comunication.Request;
using MeuLivroDeReceitas.Comunication.Response;
using Microsoft.AspNetCore.Mvc;

namespace MeuLivroDeReceitas.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class LoginController : ControllerBase
    {
        [HttpPost]
        [ProducesResponseType(typeof(ResponseLoginJson), StatusCodes.Status200OK)]
        public async Task<IActionResult> Login(
            [FromServices] ILogInUseCase useCase,
            [FromBody] LoginRequestJson request)
        {
            var resposta = await useCase.Execute(request);
            return Ok(resposta);
        }
    }
}
