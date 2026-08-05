using System;
using System.Collections.Generic;

namespace BankAppApi.Repository.Entities;

public partial class Customer
{
    public int Id { get; set; }

    public string CustomerIdentityNumber { get; set; } = null!;

    public string CustomerFullName { get; set; } = null!;

    public DateOnly CustomerBdate { get; set; }

    public string CustomerBplace { get; set; } = null!;

    public decimal CustomerRiskLimit { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public bool IsDeleted { get; set; }

    public DateTime? DeletedAt { get; set; }

    public virtual ICollection<Account> Accounts { get; set; } = new List<Account>();

    public virtual ICollection<Card> Cards { get; set; } = new List<Card>();
}

