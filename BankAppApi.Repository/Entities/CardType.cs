using System;
using System.Collections.Generic;

namespace BankApp_Api.Repository.Entities;

public partial class CardType
{
    public short Id { get; set; }

    public string CardTypesName { get; set; } = null!;

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public bool IsDeleted { get; set; }

    public DateTime? DeletedAt { get; set; }

    public virtual ICollection<Card> Cards { get; set; } = new List<Card>();
}
