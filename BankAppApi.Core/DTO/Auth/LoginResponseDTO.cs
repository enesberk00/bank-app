using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankAppApi.Core.DTO.Auth
{
    public class LoginResponseDTO
    {
        public string? Token { get; set; }
        public string? Username { get; set; }
        public string? Email { get; set; }
        public DateTime Expiration { get; set; }

    }
}

