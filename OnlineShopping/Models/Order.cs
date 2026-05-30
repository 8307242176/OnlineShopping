using System;
using System.Collections.Generic;

namespace OnlineShopping.Models;

public partial class Order
{
    public int OrdId { get; set; }

    public int? LoginId { get; set; }

    public decimal? TotalAmount { get; set; }

    public string? Status { get; set; } = null!;

    public virtual User? Login { get; set; }

    public virtual ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();
}
