using MeuLivroDeReceitas.Application.Services.Token;
using MeuLivroDeReceitas.Domain.Entities;
using MeuLivroDeReceitas.Domain.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.IdentityModel.Tokens;

namespace MeuLivroDeReceitas.Application.Services.LogedInUser
{
    public class LoggedInUser : ILoggedInUser
    {
        private readonly IHttpContextAccessor _contextAccessor;
        private readonly TokenController _tokenController;
        private readonly IUserReadOnlyRepository _userReadOnlyRepository;
        public LoggedInUser(IHttpContextAccessor httpContextAccessor, IUserReadOnlyRepository userReadOnlyRepository)
        {
            _contextAccessor = httpContextAccessor;
            _userReadOnlyRepository = userReadOnlyRepository;
        }
        public async Task<User> RetrieveUser()
        {
            //usando a Interface de IHttpContextAcessor para conseguir ter acesso a requisição, entrar no cabeçalho
            //e recuperar o Authorization que é o nosso token
            var auth = _contextAccessor.HttpContext.Request.Headers["Authorization"].ToString();

            //["Bearer".Length..] diz que a verificação deve começar a partir da posição 6, já que Bearer possui 6 letras
            //e o Trim é pra remover o espaço das pontas da string
            var token = auth["Bearer".Length..].Trim();

            var userEmail = _tokenController.RecoverEmail(auth);
            
            if (userEmail != null)
            {
                var user = await _userReadOnlyRepository.FindByEmail(userEmail);
                return user;
            }

            throw new NotImplementedException();
        }
    }
}
