using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankApp_Api.Core.DTO.Account
{
    public class AccountDTO
    {
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public string? AccountNo { get; set; }
        public string? AccountName { get; set; }
        public string? AccountIban { get; set; }
        public decimal AccountBalance { get; set; }
        public bool AccountStatus { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

    }
}
