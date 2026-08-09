using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankApp_Api.Core.DTO.Account
{
    public class CreateAccountDTO
    {
        public int CustomerId { get; set; }
        public string? AccountName { get; set; }
        public decimal AccountBalance { get; set; }

    }
}
