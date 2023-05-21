using CentralDeUsuarios.Domain.Entities;
using CentralDeUsuarios.Domain.Interfaces.Security;
using CentralDeUsuarios.Infra.Security.Settings;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace CentralDeUsuarios.Infra.Security.Services
{
    /// <summary>
    /// Classe para implementar a geração do TOKEN JWT
    /// </summary>
    public class AuthorizationSecurity : IAuthorizationSecurity
    {
        private readonly JwtSettings _jwtSettings;

        public AuthorizationSecurity(IOptions<JwtSettings> jwtSettings)
        {
            _jwtSettings = jwtSettings.Value;
        }

        public string CreateToken(Usuario usuario)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_jwtSettings.SecretKey);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                //gravar os dados do usuário no token
                Subject = new ClaimsIdentity(new Claim[] {
                    new Claim(ClaimTypes.Name, usuario.Email), //identificação do usuário autenticado
                    new Claim(ClaimTypes.Role, "USER") //perfil do usuário autenticado
                }),

                //definindo a data e hora de expiração
                Expires = DateTime.UtcNow.AddHours(_jwtSettings.ExpirationInHours),

                //criptografar a chave antifalsificação no token
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key),
                    SecurityAlgorithms.HmacSha256Signature)
            };

            //retornando o token
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
