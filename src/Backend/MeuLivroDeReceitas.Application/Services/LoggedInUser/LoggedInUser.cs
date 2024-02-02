using MeuLivroDeReceitas.Domain.Entities;
using Microsoft.AspNetCore.Http;

namespace MeuLivroDeReceitas.Application.Services.LogedInUser
{
    public class LoggedInUser : ILoggedInUser
    {
        private readonly IHttpContextAccessor _contextAccessor;
        public LoggedInUser(IHttpContextAccessor httpContextAccessor)
        {
            _contextAccessor = httpContextAccessor;
        }
        public async Task<User> RetrieveUser()
        {
            //usando a Interface de IHttpContextAcessor para conseguir ter acesso a requisição, entrar no cabeçalho
            //e recuperar o Authorization que é o nosso token
            var auth = _contextAccessor.HttpContext.Request.Headers["Authorization"].ToString();

            throw new NotImplementedException();
        }
    }
}
