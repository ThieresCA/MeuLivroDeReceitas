using MeuLivroDeReceitas.Application.Services.Token;
using MeuLivroDeReceitas.Comunication.Response;
using MeuLivroDeReceitas.Domain.Repository;
using MeuLivroDeReceitas.Exceptions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.IdentityModel.Tokens;

namespace MeuLivroDeReceitas.Api.Filters
{
    public class AuthenticatedUserAttribute : AuthorizeAttribute, IAsyncAuthorizationFilter
    {
        private readonly TokenController _tokenController;
        private readonly IUserReadOnlyRepository _userRepository;

        public AuthenticatedUserAttribute(TokenController tokenController, IUserReadOnlyRepository userRepository)
        {
            _tokenController = tokenController;
            _userRepository = userRepository;
        }

        public async Task OnAuthorizationAsync(AuthorizationFilterContext context)
        {
            try
            {
                var token = RequestToken(context);
                var email = _tokenController.RecoverEmail(token);

                var user = await _userRepository.FindByEmail(email);

                if (user == null)
                {
                    throw new Exception();
                }
            }
            catch (SecurityTokenExpiredException)
            {
                ExpiredToken(context);
            }
            catch
            {
                Unauthorized(context);
            }
        }

        private string RequestToken(AuthorizationFilterContext context)
        {
            var authorization = context.HttpContext.Request.Headers["Authorization"].ToString();

            if (string.IsNullOrEmpty(authorization))
            {
                throw new Exception("Unauthorized");
            }

            return authorization["Bearer".Length..].Trim();
        }

        private void ExpiredToken(AuthorizationFilterContext context)
        {
            context.Result = new UnauthorizedObjectResult(new ResponseErrorJson(ResourceErrorMessage.TOKEN_EXPIRED));        
        }

        private void Unauthorized(AuthorizationFilterContext context)
        {
            context.Result = new UnauthorizedObjectResult(new ResponseErrorJson(ResourceErrorMessage.INVALID_TOKEN));
        }
    }
}
