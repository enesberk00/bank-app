using System;
using System.Collections.Generic;

namespace BankAppApi.Repository.Entities;

public partial class Transaction
{
    public int Id { get; set; }

    public int CardId { get; set; }

    public int? AccountId { get; set; }

    public short TransactionTypeId { get; set; }

    public decimal TransactionAmount { get; set; }

    public string? TransactionDescription { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public bool IsDeleted { get; set; }

    public DateTime? DeletedAt { get; set; }

    public virtual Account? Account { get; set; }

    public virtual Card Card { get; set; } = null!;

    public virtual TransactionType TransactionType { get; set; } = null!;
}

