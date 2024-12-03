using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.IdentityModel.Tokens;

namespace api_bookStore.App.Services.Jwt
{
    /// <summary>
    /// Serviço para geração de tokens JWT.
    /// </summary>
    /// 
    /// <param name="configuration">Configurações da aplicação, incluindo a chave secreta para assinatura do token.</param>
    public class JwtTokenService(IConfiguration configuration) : IJwtTokenService
    {
        private readonly IConfiguration _configuration = configuration;

        /// <summary>
        /// Gera um token JWT com base nas credenciais do usuário e configuração fornecida.
        /// </summary>
        /// <param name="userId">ID do usuário que será incluído no token como claim.</param>
        /// <param name="role">Função ou papel do usuário (como "Admin", "User", etc.) que será incluída no token como claim.</param>
        /// <returns>Um token JWT assinado.</returns>
        /// <exception cref="Exception">Lançado quando a chave secreta não é encontrada nas configurações.</exception>
        public string GenerateJwtToken(string userId, string role)
        {

            string secretKey = _configuration["Jwt:Key"] ?? throw new Exception("A chave secreta do token não foi encontrada. Verificar variável de ambiente");

            byte[] key = Encoding.ASCII.GetBytes(secretKey);

            SecurityTokenDescriptor tokenConfig = new()
            {
                Subject = new ClaimsIdentity(
                [
                    new Claim("userId", userId),
                    new Claim(ClaimTypes.Role, role)
                ]),
                Expires = DateTime.UtcNow.AddHours(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            // Criação e assinatura do token JWT
            JwtSecurityTokenHandler tokenHanlder = new();
            SecurityToken token = tokenHanlder.CreateToken(tokenConfig);
            string tokenString = tokenHanlder.WriteToken(token);

            return tokenString;
        }
    }
}