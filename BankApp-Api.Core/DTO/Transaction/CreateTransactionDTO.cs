using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankApp_Api.Core.DTO.Transaction
{
    internal class CreateTransactionDTO
    {
        public int CardId { get; set; }
        public int? AccountId { get; set; }
        public short TransactionTypeId { get; set; }
        public decimal TransactionAmount { get; set; }
        public string? TransactionDescription { get; set; }

    }
}
