using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Models.Data;
using Repository.Interfaces;
using Services.DTOs;
using Services.Interfaces;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class AuthenticationUserService(IUserRepository userRepository, IConfiguration configuration) : IAuthenticationUserService
    {
        public async Task<ResponseDto> AuthenticateUser(string username, string password)
        {
            try
            {
                var credentials = await VerificateCredentials(username, password);
                if (!credentials.Success) return credentials;

                var token = GenerarJWT(username);
                credentials.Jwt = token;
                credentials.Message = "Sesión iniciada!";

                return credentials;
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Error al autenticar usuario: " + ex);
            }
        }

        public string EncryptSHA256(string password)
        {
            using (SHA256 sha256Hash = SHA256.Create())
            {
                // Computar el hash
                byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(password));

                // Convertir el array de bytes a string
                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < bytes.Length; i++)
                {
                    builder.Append(bytes[i].ToString("x2"));
                }

                return builder.ToString();
            }
        }


        #region Private Methods
        private async Task<ResponseDto> VerificateCredentials(string username, string password)
        {
            try
            {
                var responseDb = userRepository.GetUserByUsername(username);
                var encryptedPassword = EncryptSHA256(password);

                var userInfo = await responseDb;
                if (userInfo == null)
                {
                    return new ResponseDto { Success = false, Message = "Usuario incorrecto!" };
                }
                else if (userInfo.UsEstado == false)
                {
                    return new ResponseDto { Success = false, Message = "Usuario inactivo!" };
                }
                else if (userInfo.UsContrasena != encryptedPassword)
                {
                    return new ResponseDto { Success = false, Message = "Clave incorrecta!" };
                }


                return new ResponseDto { Success = true, Message = "Credenciales válidas!" };
            }
            catch (Exception ex)
            {
                //throw new ApplicationException("Error al verificar credenciales: " + ex);
                return new ResponseDto { Success = false, Message = $"Error al verificar credenciales: {ex.Message}" };
            }
        }

        private string GenerarJWT(string usuario)
        {
            //crear la informacion del usuario para token
            var userClaims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, usuario),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:key"]!));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature);

            //crear detalle del token
            var jwtConfig = new JwtSecurityToken(
                claims: userClaims,
                expires: DateTime.UtcNow.AddHours(5),
                signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(jwtConfig);
        }

        #endregion
    }
}
