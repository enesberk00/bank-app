using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankAppApi.Core.DTO.Card
{
    public class UpdateCardDTO
    {
        public bool CardStatus { get; set; }
        public decimal? CardLimit { get; set; }

    }
}

