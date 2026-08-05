using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankAppApi.Core.DTO.Transaction
{
    public class TransactionDTO
    {
        public int Id { get; set; }
        public int CardId { get; set; }
        public int? AccountId { get; set; }
        public short TransactionTypeId { get; set; }
        public decimal TransactionAmount { get; set; }
        public string? TransactionDescription { get; set; }
        public DateTime CreatedAt { get; set; }

    }
}

