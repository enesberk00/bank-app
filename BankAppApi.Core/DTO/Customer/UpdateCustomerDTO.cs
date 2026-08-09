using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankAppApi.Core.DTO.Customer
{
    public class UpdateCustomerDTO
    {
        public string? CustomerFullName { get; set; }
        public string? CustomerBplace { get; set; }
        public decimal CustomerRiskLimit { get; set; }

    }
}

