using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankApp_Api.Core.DTO.Customer
{
    internal class UpdateCustomerDTO
    {
        public string? CustomerFullName { get; set; }
        public string? CustomerBplace { get; set; }
        public decimal CustomerRiskLimit { get; set; }

    }
}
