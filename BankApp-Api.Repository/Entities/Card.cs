using System;
using System.Collections.Generic;

namespace BankApp_Api.Repository.Entities;

public partial class Card
{
    public int Id { get; set; }

    public int? CustomerId { get; set; }

    public int? AccountId { get; set; }

    public short CardType { get; set; }

    public string CardNo { get; set; } = null!;

    public short CardValidityMonth { get; set; }

    public short CardValidityYear { get; set; }

    public string CardCcv { get; set; } = null!;

    public decimal? CardLimit { get; set; }

    public bool CardStatus { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public bool IsDeleted { get; set; }

    public DateTime? DeletedAt { get; set; }

    public virtual Account? Account { get; set; }

    public virtual CardType CardTypeNavigation { get; set; } = null!;

    public virtual Customer? Customer { get; set; }

    public virtual ICollection<Transaction> Transactions { get; set; } = new List<Transaction>();
}
