using BankApp_Api.Core.DTO.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankApp_Api.Core.Services
{
    internal interface IAuthService
    {
        Task<LoginResponseDTO> LoginAsync(LoginRequestDTO dto);
        Task RegisterAsync(RegisterDTO dto);
    }
}
