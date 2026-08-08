using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankApp_Api.Core.DTO.Transaction
{
    public class TransactionTypeDTO
    {
        public short Id {  get; set; }

        public string TransactionTypeName { get; set; }
    }
}
