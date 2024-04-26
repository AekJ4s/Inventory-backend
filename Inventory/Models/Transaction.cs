using System;
using System.Collections.Generic;

namespace Inventory.Models;

public partial class Transaction
{
    public int Id { get; set; }

    public int? ProductId { get; set; }

    public int? TransactionTypeId { get; set; }

    public int? Quantity { get; set; }

    public DateTime? CreateDate { get; set; }

    public DateTime? UpdateDate { get; set; }

    public bool? IsDeleted { get; set; }

    public virtual Product? Product { get; set; }

    public virtual TransactionType? TransactionType { get; set; }
}
