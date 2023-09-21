using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace MeuLivroDeReceitas.Application.Services.Token
{
    public class TokenController
    {
        private const string EmailAlias = "eml";
        private readonly double _lifeTimeTokenMinutes;
        private readonly string _tokenKey;

        public TokenController(double lifeTimeToken, string tokenKey)
        {
            _lifeTimeTokenMinutes = lifeTimeToken;
            _tokenKey = tokenKey;
        }

        public string TokenGenerator(string userEmail)
        {
            var claim = new List<Claim>
            {
                new Claim(EmailAlias, userEmail)
            };

            var tokenHandler = new JwtSecurityTokenHandler();

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                //o email será mostrado no token
                Subject = new ClaimsIdentity(claim),
                //adicionando o tempo de expiração ao token ou seja, de acordo com a data de agora, até os minutos adicionados
                Expires = DateTime.UtcNow.AddMinutes(_lifeTimeTokenMinutes),
                SigningCredentials = new SigningCredentials(SinmetricKey(),
                    SecurityAlgorithms.HmacSha256Signature
                    )
            };

            var securityToken = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(securityToken);
        }

        public void TokenValidation(string token)
        {
            var tokenHandler = new JwtSecurityTokenHandler();

            var validationParams = new TokenValidationParameters
            {
                RequireExpirationTime = true,
                IssuerSigningKey = SinmetricKey(),
                ClockSkew = new TimeSpan(0),
                ValidateIssuer = false,
                ValidateAudience = false
            };
            //validando o token
            tokenHandler.ValidateToken(token, validationParams, out _);
        }

        private SymmetricSecurityKey SinmetricKey()
        {
            var synmetricKey = Convert.FromBase64String(_tokenKey);
            return new SymmetricSecurityKey(synmetricKey);
        }
    }
}
