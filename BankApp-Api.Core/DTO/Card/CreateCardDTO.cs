using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankApp_Api.Core.DTO.Card
{
    internal class CreateCardDTO
    {
        ublic int CustomerId { get; set; }
        public int AccountId { get; set; }
        public short CardType { get; set; }
        public decimal? CardLimit { get; set; }
    }
}
