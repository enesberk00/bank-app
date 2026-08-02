using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankApp_Api.Core.DTO.Customer
{
    internal class CustomerDTO
    {
        public int Id { get; set; }
        public string? CustomerIdentityNumber { get; set; }
        public string? CustomerFullName { get; set; }
        public DateOnly CustomerBdate { get; set; }
        public string? CustomerBplace { get; set; }
        public decimal CustomerRiskLimit { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

    }
}
