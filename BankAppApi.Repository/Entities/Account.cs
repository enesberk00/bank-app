using System;
using System.Collections.Generic;

namespace BankAppApi.Repository.Entities;

public partial class Account
{
    public int Id { get; set; }

    public int CustomerId { get; set; }

    public string AccountNo { get; set; } = null!;

    public string AccountName { get; set; } = null!;

    public string AccountIban { get; set; } = null!;

    public decimal AccountBalance { get; set; }

    public bool AccountStatus { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public bool IsDeleted { get; set; }

    public DateTime? DeletedAt { get; set; }

    public virtual ICollection<Card> Cards { get; set; } = new List<Card>();

    public virtual Customer Customer { get; set; } = null!;

    public virtual ICollection<Transaction> Transactions { get; set; } = new List<Transaction>();
}

