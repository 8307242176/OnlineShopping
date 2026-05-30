using System;
using System.Collections.Generic;

namespace OnlineShopping.Models;

public partial class OrderItem
{
    public int OrderItemId { get; set; }

    public int OrdId { get; set; }

    public int PrdId { get; set; }

    public int Quantity { get; set; }

    public int Price { get; set; }

    public virtual Order Ord { get; set; } = null!;

    public virtual Product Prd { get; set; } = null!;
}
