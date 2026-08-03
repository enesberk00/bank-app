using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankApp_Api.Core.DTO.Card
{
    public class CardDTO
    {
        public int Id { get; set; }
        public int? CustomerId { get; set; }
        public int? AccountId { get; set; }
        public short CardType { get; set; }
        public string? CardNo { get; set; }
        public short CardValidityMonth { get; set; }
        public short CardValidityYear { get; set; }
        public decimal? CardLimit { get; set; }
        public bool CardStatus { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

    }
}
