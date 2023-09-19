using MeuLivroDeReceitas.Application.UseCases.User.SignUp;
using MeuLivroDeReceitas.Comunication.Request;
using MeuLivroDeReceitas.Comunication.Response;
using Microsoft.AspNetCore.Mvc;

namespace MeuLivroDeReceitas.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {

        [HttpGet(Name = "GetWeatherForecast")]
        public async Task<ResponseCreateUserJson> Get([FromServices] ISignUpUseCase useCase)
        {
            return await useCase.Execute(new RequestCreateUserJson
            {
                Email = "thzin@gmail.com",
                Name = "Th vida calma",
                Password = "123456",
                Phone = "27 9 8874-2755"
            });
        }
    }
}