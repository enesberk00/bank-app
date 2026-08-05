using System;
using System.Collections.Generic;

namespace BankAppApi.Repository.Entities;

public partial class TransactionType
{
    public short Id { get; set; }

    public string TransactionTypeName { get; set; } = null!;

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public bool IsDeleted { get; set; }

    public DateTime? DeletedAt { get; set; }

    public virtual ICollection<Transaction> Transactions { get; set; } = new List<Transaction>();
}

