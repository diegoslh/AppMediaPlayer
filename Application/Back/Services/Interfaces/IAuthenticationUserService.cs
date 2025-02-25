using Services.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Interfaces
{
    public interface IAuthenticationUserService
    {
        Task<ResponseDto> AuthenticateUser(string username, string password);
        string EncryptSHA256(string password);
    }
}
